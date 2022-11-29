using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    CharacterStats stats;
    Animator animator;

    public float walkSpeed;
    public float jumpForce;
    public float dashSpeed;
    public float dashUpForce;
    int direction;

    public int jumpCount = 0;
    public bool isLongJump = false;
    public bool hasAttacked = false;   //피격 중복 금지
    public bool dashOn = false;
    public bool onPuzzle = false;
    public bool movable = true;


    //어택 스크립트
    private Attack script;
    CharacterStats HP;

    //마지막 바닥 기억
    GameObject obj;
    public bool CameraController = false;


    public GameObject puzzle1;
    public GameObject puzzle2;
    public GameObject puzzle3;


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        stats = GetComponent<CharacterStats>();
        animator = GetComponent<Animator>();

        script = GameObject.Find("Player").GetComponent<Attack>();  //공격 스크립트 접근
        HP = GameObject.Find("Player").GetComponent<CharacterStats>();
        //Shake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
    }

    void Update()
    {
        animator.SetBool("run", false);

        //캐릭터 이동/점프
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2 && movable)
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

        if (Input.GetKeyDown(KeyCode.LeftShift) && !dashOn && movable)
        {
            dashOn = true;
            Dash();
            Invoke("DashOn", 1);
        }

        if (Input.GetKeyDown(KeyCode.P) && onPuzzle)
        {
            onPuzzle = false;
            movable = true;
            if (puzzle1.activeSelf) puzzle1.SetActive(false);
            if (puzzle2.activeSelf) puzzle2.SetActive(false);
            if (puzzle3.activeSelf) puzzle3.SetActive(false);
            Debug.Log("퍼즐 비활성화!");
        }

        //CheckDie();
    }

    //Rigidbody(물리연산)를 이용할 때는 FixedUpdate에 작성
    private void FixedUpdate()
    {
        if (isLongJump) //rigid.velocity.y 조건 잠깐 빼놓음
            rigid.gravityScale = 15.0f;
        else
            rigid.gravityScale = 20.0f;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform")|| other.gameObject.CompareTag("Platform2"))
        {
            this.jumpCount = 0;
            animator.SetBool("jump", false);

            //----------------------------------------------
            obj = other.gameObject;

        }
        if (other.gameObject.CompareTag("thorn") && !hasAttacked)  //가시충돌
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
            if (HP.currentHP <= 0)
            {
                animator.SetTrigger("Die");
                Destroy(gameObject, 1);
            }
            else
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
                Debug.Log("퍼즐 활성화!");
            }
        }
    }

    void OnPuzzle()
    {
        onPuzzle = true;
    }

    //점프
    public void Jump()
    {
        rigid.velocity = new Vector2(rigid.velocity.x, 0f);
        rigid.AddForce(new Vector2(0.0f, jumpForce));
        jumpCount++;
    }

    public void Dash()
    {
        //Debug.Log("대쉬");

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

    //가시 충돌시
    void TrnsForm()
    {
        Transform pos = obj.GetComponent<Transform>();
        float Wid = obj.GetComponent<RectTransform>().rect.width;
        Wid = (Wid / 5);

        Debug.Log(Wid);

        transform.position = new Vector3(pos.position.x + Wid, pos.position.y + 20, 1);

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
        }
    }

    void movabletrue()
    {
        movable = true;
    }

}
