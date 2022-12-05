using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    CharacterStats stats;
    Animator animator;
    data SavePos;

    public Animator penelani;
    public GameManager manager;
    GameObject scanObject;
    public GameObject Heal;

    public float walkSpeed;
    public float jumpForce;
    public float dashSpeed;
    public float dashUpForce;
    int direction;

    public int jumpCount = 0;
    public bool isLongJump = false;
    public bool hasAttacked = false;   //�ǰ� �ߺ� ����
    public bool dashOn = false;
    public bool onPuzzle = false;
    public bool movable = true;
    public bool IsJump = true;
    bool IsDash = false;
    float time = 0.1f;

    //���� ��ũ��Ʈ
    private Attack script;
    CharacterStats HP;

    //������ �ٴ� ���
    GameObject obj;
    public bool CameraController = false;


    public GameObject puzzle1;
    public GameObject puzzle2;
    public GameObject puzzle3;

    float healtime = 0.4f;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        stats = GetComponent<CharacterStats>();
        animator = GetComponent<Animator>();
        penelani = GameObject.Find("���").GetComponent<Animator>();
        SavePos = GameObject.Find("Main Camera").GetComponent<data>();

        script = GameObject.Find("Player").GetComponent<Attack>();  //���� ��ũ��Ʈ ����
        HP = GameObject.Find("Player").GetComponent<CharacterStats>();
        //Shake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
    }

    void Update()
    {
        animator.SetBool("run", false);

        //ĳ���� �̵�/����
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2 && movable&& IsJump)
        {
            Jump();
            animator.SetBool("jump", true);
        }

        if (Input.GetKey(KeyCode.Space))
            isLongJump = true;

        if (Input.GetKeyUp(KeyCode.Space))
            isLongJump = false;

        if (Input.GetKey(KeyCode.LeftArrow)&&script.AttackLeftOn && movable)
        {
            direction = 1;
            spriteRenderer.flipX = false;
            Vector2 vec = transform.position;
            vec += new Vector2(-walkSpeed * Time.deltaTime, 0.0f);
            transform.position = vec;
            animator.SetBool("run", true);
        }

        if (Input.GetKey(KeyCode.RightArrow) && script.AttackRightOn && movable)
        {
            direction = 2;
            spriteRenderer.flipX = true;
            Vector2 vec = transform.position;
            vec += new Vector2(walkSpeed * Time.deltaTime, 0.0f);
            transform.position = vec;
            animator.SetBool("run", true);
        }

        if (Input.GetKeyDown(KeyCode.X) && !dashOn && movable)
        {
            dashOn = true;
            time = 0.1f;
            IsDash = true;
            Invoke("DashOn", 1);

            if (direction == 1) animator.SetTrigger("LeftDash");
            if (direction == 2) animator.SetTrigger("RightDash");
        }

        if (Input.GetKeyDown(KeyCode.P) && onPuzzle)
        {
            onPuzzle = false;
            movable = true;
            if (puzzle1.activeSelf) puzzle1.SetActive(false);
            if (puzzle2.activeSelf) puzzle2.SetActive(false);
            if (puzzle3.activeSelf) puzzle3.SetActive(false);
            Debug.Log("���� ��Ȱ��ȭ!");
        }
        if(IsDash)
        {
            //rigid.gravityScale = 0f;
            if (time >=0)
            {
                Destroy(gameObject.GetComponent<Rigidbody>());
                if (direction == 1)
                {
                    rigid.gravityScale = 0f;
                    transform.Translate(Vector3.right * -1 * 280 * Time.deltaTime);
                }

                if (direction == 2)
                {
                    rigid.gravityScale = 0f;
                    transform.Translate(Vector3.right * 280 * Time.deltaTime);
                }
            }
            else
            {
                gameObject.AddComponent<Rigidbody>();
                IsDash = false;
                rigid.velocity = Vector2.zero;
                rigid.gravityScale = 20.0f;
            }
            time -= Time.deltaTime;
        }

        CheckDie();

        if(Input.GetKeyDown(KeyCode.T)) manager.Action();

    }

    //Rigidbody(��������)�� �̿��� ���� FixedUpdate�� �ۼ�
    private void FixedUpdate()
    {
        if (isLongJump) //rigid.velocity.y ���� ��� ������
            rigid.gravityScale = 15.0f;
        else if(!isLongJump&&!IsDash)
            rigid.gravityScale = 20.0f;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform")||other.gameObject.CompareTag("platform2")
            || other.gameObject.CompareTag("CameraSetPlatform"))
        {
            if (other.contacts[0].normal.y >= 0.8f){
                this.jumpCount = 0;
                animator.SetBool("jump", false);
            }
            if (other.gameObject.CompareTag("Platform")){
                obj = other.gameObject;
            }
        }
        if (other.gameObject.CompareTag("thorn") && !hasAttacked)  //�����浹
        {
            stats.TakeDamage();
            hasAttacked = true;
            Invoke("attackOn", 3);
            //
            rigid.velocity = Vector2.zero;
            script.AttackRightOn = false;
            script.AttackLeftOn = false;
            //
            CameraController = true;
            animator.SetTrigger("Damage");
            //
            if (HP.currentHP > 0)
            {
                Invoke("TrnsForm", 0.7f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && !hasAttacked)
        {

            stats.TakeDamage();
            hasAttacked = true;
            Invoke("attackOn", 3);
            //
            CameraController = true;
            animator.SetTrigger("Damage");
            //
            if (HP.currentHP <= 0)
            {
                movable = false;
                Invoke("movabletrue", 4);
                animator.SetTrigger("Die");
                Destroy(gameObject, 2);
            }
        }
        if (other.gameObject.CompareTag("thorn"))
        {
            movable = false;
            Invoke("movabletrue", 1);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PuzzleSpot"))
        {
            if (Input.GetKeyDown(KeyCode.P) && !onPuzzle)
            {
                Invoke("OnPuzzle", 0.5f);
                movable = false;
                switch (other.gameObject.name)
                {
                    case "Puzzle1Spot":
                        puzzle1.SetActive(true);
                        break;
                    case "Puzzle2Spot":
                        puzzle2.SetActive(true);
                        break;
                    case "Puzzle3Spot":
                        puzzle3.SetActive(true);
                        break;
                }
                Debug.Log("���� Ȱ��ȭ!");
            }
        }

        if (other.tag == "SavePoint")
        {
            if (Input.GetKey(KeyCode.Space) && healtime <= 0)
            {
                healtime = 0.4f;
                Debug.Log("���̺�");
                stats.currentHP = stats.maxHP;
                Instantiate(Heal, transform.position, transform.rotation);
                SavePos.Respawn = other.gameObject.transform;
            }
            healtime -= Time.deltaTime;
        }

    }

    void OnPuzzle()
    {
        onPuzzle = true;
    }

    //����
    public void Jump()
    {
        rigid.velocity = new Vector2(rigid.velocity.x, 0f);
        rigid.AddForce(new Vector2(0.0f, jumpForce));
        jumpCount++;
    }

    public void Dash()
    {
        //Debug.Log("�뽬");

        rigid.velocity = Vector2.zero;
        
        if (direction == 1)
        {
            rigid.AddForce(new Vector2(-dashSpeed, dashUpForce));
        }

        if (direction == 2)
        {
            rigid.AddForce(new Vector2(dashSpeed, dashUpForce));
        }
    }

    //���� �浹��
    void TrnsForm()
    {
        Transform pos = obj.GetComponent<Transform>();
        float Wid = obj.GetComponent<RectTransform>().rect.width;
        Wid = (Wid / 6);

        Debug.Log(Wid);

        transform.position = new Vector3(pos.position.x + Wid, pos.position.y + 23, 1);

        script.AttackRightOn = true;
        script.AttackLeftOn = true;

    }

    public void attackOn()
    {
        hasAttacked = false;
    }

    public void DashOn()
    {
        dashOn = false;
    }    

    void CheckDie()
    {
        if(HP.currentHP<=0)
        {
            animator.SetTrigger("Die");
            Destroy(gameObject,2);

            //ȭ�� ��ξ���
            Invoke("respown", 3);
        }
    }

    void movabletrue()
    {
        movable = true;
    }

    void respown()
    {
        transform.position = SavePos.Respawn.position;
        stats.currentHP = stats.maxHP;
        Instantiate(Heal, transform.position, transform.rotation);
        //ȸ�� �ִϸ��̼�
    }
}