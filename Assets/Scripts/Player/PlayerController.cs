using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    CharacterStats stats;
    Animator animator;
    data SavePos;
    GameController KeyController;

    public GameManager manager;
    GameObject scanObject;
    public GameObject Heal;
    public GameObject gameOverImage;

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
    public bool movable = false;
    public bool IsJump = true;
    bool IsDash = false;
    float time = 0.1f;
    float StartCheckTime = 3f;

    //어택 스크립트
    private Attack script;
    CharacterStats HP;

    //마지막 바닥 기억
    GameObject obj;
    public bool CameraController = false;


    public GameObject puzzle1;
    public GameObject puzzle2;
    public GameObject puzzle3;

    float healtime = 0.7f;
    int DieMoveStage;
    bool NotLoop = false;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        stats = GetComponent<CharacterStats>();
        animator = GetComponent<Animator>();
        SavePos = GameObject.Find("Main Camera").GetComponent<data>();

        script = GameObject.Find("Player").GetComponent<Attack>();  //공격 스크립트 접근
        HP = GameObject.Find("Player").GetComponent<CharacterStats>();
        KeyController = GameObject.Find("GameManager").GetComponent<GameController>();
        //Shake = GameObject.Find("Main Camera").GetComponent<CameraShake>();

        Invoke("movabletrue", 2f);
    }

    void Update()
    {
        animator.SetBool("run", false);

        //캐릭터 이동/점프
        if (Input.GetKeyDown(KeyCode.LeftControl) && jumpCount < 2 && movable&& IsJump)
        {
            Jump();
            animator.SetBool("jump", true);
        }

        if (Input.GetKey(KeyCode.LeftControl))
            isLongJump = true;

        if (Input.GetKeyUp(KeyCode.LeftControl))
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

        if (Input.GetKeyDown(KeyCode.X) && !dashOn && movable && SaveManager.Instance._playerData.killedBoss1)
        {
            dashOn = true;
            time = 0.1f;
            IsDash = true;
            Invoke("DashOn", 1);

            if (direction == 1) animator.SetTrigger("LeftDash");
            if (direction == 2) animator.SetTrigger("RightDash");
        }

        if (Input.GetKeyDown(KeyCode.Space) && onPuzzle)
        {
            onPuzzle = false;
            movable = true;
            if (puzzle1.activeSelf) puzzle1.SetActive(false);
            if (puzzle2.activeSelf) puzzle2.SetActive(false);
            if (puzzle3.activeSelf) puzzle3.SetActive(false);
            Debug.Log("퍼즐 비활성화!");
        }

        if(IsDash)
        {
            //rigid.gravityScale = 0f;
            if (time >=0)
            {
                //Destroy(gameObject.GetComponent<Rigidbody>());
                if (direction == 1)
                {
                    rigid.gravityScale = 0f;
                    rigid.velocity = Vector3.zero;
                    transform.Translate(Vector3.right * -1 * 280 * Time.deltaTime);
                }

                if (direction == 2)
                {
                    rigid.gravityScale = 0f;
                    rigid.velocity = Vector3.zero;
                    transform.Translate(Vector3.right * 280 * Time.deltaTime);
                }
            }
            else
            {
                //gameObject.AddComponent<Rigidbody>();
                IsDash = false;
                rigid.velocity = Vector2.zero;
                rigid.gravityScale = 20.0f;
            }
            time -= Time.deltaTime;
        }

        if(StartCheckTime >= 0)
        {
            StartCheckTime -= Time.deltaTime;
        }else
        {
            CheckDie();
        }
    }

    //Rigidbody(물리연산)를 이용할 때는 FixedUpdate에 작성
    private void FixedUpdate()
    {
        if (isLongJump) //rigid.velocity.y 조건 잠깐 빼놓음
            rigid.gravityScale = 15.0f;
        else if (!isLongJump && !IsDash)
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
            /*
            if (HP.currentHP <= 0)
            {
                movable = false;
                Invoke("movabletrue", 4);
                animator.SetTrigger("Die");
                Destroy(gameObject, 2);
            }
            */
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
            if (Input.GetKeyDown(KeyCode.Space) && !onPuzzle)
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

        if (other.tag == "SavePoint")
        {

            if(!KeyController.FirstSaveCol)
            {
                KeyController.FirstSaveCol = true;
                GameObject.Find("Canvas").transform.Find("튜토리얼").transform.Find("세이브 가능 튜토리얼").gameObject.SetActive(true);
            }
            if (Input.GetKey(KeyCode.Space) && healtime <= 0)
            {
                healtime = 0.7f;
                Debug.Log("세이브");
                stats.currentHP = stats.maxHP;
                Instantiate(Heal, transform.position, transform.rotation);

                SavePos.Respawn = other.gameObject.transform;
                DieMoveStage = other.GetComponent<SaveStage>().SetStage;

                //JSON 파일 업데이트
                SaveManager.Instance._playerData.haveSaveFile = true;
                SaveManager.Instance.SavePlayerDataToJson();
            }
            healtime -= Time.deltaTime;
        }
        if (other.gameObject.CompareTag("Start"))
        {
            if (!KeyController.FirstStart)
            {
                movable = false;
                
            }
            if (!KeyController.FirstStart)
            {
                KeyController.FirstStart = true;
                Invoke("delay", 2.5f);
            }
            if (NotLoop&&!GameObject.Find("Canvas").transform.Find("튜토리얼").transform.Find("키조작").gameObject.activeSelf)
            {
                movable = true;
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
            movable = false;

            animator.SetTrigger("Die");

            Invoke("OnGameOverImage", 2f);

            Invoke("LoadSceneTitle", 5f);
        }
    }


    void OnGameOverImage()
    {
        gameOverImage.SetActive(true);
    }

    void LoadSceneTitle()
    {
        SceneManager.LoadScene("Title");
    }

    void movabletrue()
    {
        movable = true;
    }

    void respown()
    {
        SavePos.Stage = DieMoveStage;
        transform.position = SavePos.Respawn.position;
        stats.currentHP = stats.maxHP;
        Instantiate(Heal, transform.position, transform.rotation);
    }

    void delay()
    {
        NotLoop = true;
        GameObject.Find("Canvas").transform.Find("튜토리얼").transform.Find("키조작").gameObject.SetActive(true);
    }
}
