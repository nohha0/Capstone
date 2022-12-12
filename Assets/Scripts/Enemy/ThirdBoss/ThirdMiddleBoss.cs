using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class ThirdMiddleBoss : Enemy
{
    //시전할 스킬 랜덤값으로 고름
    List<string> SetSkill = new List<string> { "Knife", "Forward", "Summon", "Telepo" };
    public int Knife_Pattern;     //나이프 패턴, 접근 가능

    //몬스터 소환프리팹
    [SerializeField] protected GameObject Fly_1;
    [SerializeField] protected GameObject Knife;
    [SerializeField] protected GameObject MagicCircle;

    [SerializeField] protected Transform BossPos;
    [SerializeField] protected Transform PlayerPos;
    [SerializeField] protected Transform BasePos;

    int CurrentPos;         //중보스에 현위치를 알려줌
    Transform SummonPos;    //몬스터 소환시 기점으로 하는 포스
    public SpriteRenderer rend;    //중보스 

    //Skill : 0 = 칼 던지기스킬, 1 = 돌진, 2 = 소환, 3 = 텔포        

    public Rigidbody2D riged;

    float ForWardTime = 0;      //돌진 진행하는 시간
    int BeforeSkill;            //중복이 되면 안되는 스킬이 있기 때문에 그걸 판단하는 정수 
    int SetSkillNum;
    float ForwardSpeed = 180;    //돌진 스피드  
    protected bool cooltime = false;
    protected Vector3 pos;                //초기화 변수

    protected float Summoncurtime = 0;
    protected float Summoncooltime = 30;

    //protected float retelepotime;

    Vector3 m_offset;

    //------------------------------------
    bool DashOn = false;
    bool SummonOn = false;
    bool KnifeOn = false;
    bool start = false;


    float timeUntilChangeState;
    bool onRest = false;

    override protected void Start()
    {
        base.Start();
        rend = GetComponent<SpriteRenderer>();
        riged = GetComponent<Rigidbody2D>();

        Invoke("Teleport", 2); //중앙에있던 보스를 텔포로 위치 잡기
        Invoke("OnStart", 2);
        InvokeRepeating("OnActive", 5, 3);



    }

    override protected void Update()
    {
        base.Update();
        ChangeState();

        if ((targetGameObject.transform.position - transform.position).magnitude <= mag)
        {
            if (start)
            {
                if (DashOn) ForwardSKill();
                if (KnifeOn)
                {
                    KnifeOn = false;
                    KnifeSkill();
                    Invoke("KnifeSkill", 6);
                    Invoke("Teleport", 8);

                }
                if (SummonOn)
                {
                    //SummonSkill();
                    SummonOn = false;
                    if (timeUntilChangeState <= 2)
                    {
                        Teleport();
                    }
                }
                Summoncurtime -= Time.deltaTime;
                timeUntilChangeState -= Time.deltaTime;
            }

        }

        //---------------------------------
        if (HP <= 0)
        {
            Destroy(transform.GetChild(0).gameObject);
            spriteRend.color = new Color(0.8f, 0.8f, 0.8f);
            //애니메이션 끝나면서 경험치 주셈
            //애니함수 호출해야하는데 지금은 그냥 다이 스킬 획득 호출
            DieAni();
        }
    }



    void KnifeSkill()
    {
        Debug.Log("나이프");
        Instantiate(Knife,transform);
    }

    void SummonSkill()
    {
        Debug.Log("기본 소환");
        for (int i = 0; i < 2; i++)
        {

            SummonSetPos();
            Instantiate(MagicCircle, pos, transform.rotation);
            Instantiate(Fly_1, pos, transform.rotation);

        }

    }
    //-------------------------------------------------------
    public void ForwardSKill()
    {

        if (ForWardTime < 2f)
        {
            if (rend.flipX) //오른쪽 돌진
            {
                transform.Translate(transform.right * ForwardSpeed * Time.deltaTime);
            }
            if (!rend.flipX) //왼쪽 돌진
            {
                transform.Translate(transform.right * -1 * ForwardSpeed * Time.deltaTime);
            }
        }
        else
        {
            riged.velocity = Vector2.zero;   //동작 정지
            ForWardTime = 0;  //다시 0           
            Teleport();
            DashOn = false;
            timeUntilChangeState = 0;
        }
        ForWardTime += Time.deltaTime;  //시간증가
    }
    //-----------------------------------------------------------
    void Teleport()
    {
        gameObject.SetActive(false);
        Debug.Log("텔포");
        int a;
        a = CurrentPos;
        CurrentPos = Random.Range(0, 2);   //텔포 위치 정하는 함수         
        if (CurrentPos == 0)   //왼쪽텔포
        {

            if (a == 0)
            {
                //오른쪽텔포
                transform.position = new Vector2(BasePos.position.x + 80, BasePos.position.y);
                rend.flipX = false;
                CurrentPos = 1;
            }
            else
            {
                //왼쪽텔포
                transform.position = new Vector2(BasePos.position.x - 80, BasePos.position.y);
                rend.flipX = true;
            }
            Invoke("OnActive", 1f);
        }
        else if (CurrentPos == 1)  //오른쪽텔포
        {
            if (a == 1)
            {
                //왼쪽텔포
                transform.position = new Vector2(BasePos.position.x - 80, BasePos.position.y);
                rend.flipX = true;
                CurrentPos = 0;
            }
            else
            {
                //오른쪽텔포
                transform.position = new Vector2(BasePos.position.x + 80, BasePos.position.y);
                rend.flipX = false;
            }
            Invoke("OnActive", 1f);
        }
    }
    //-----------------------------------------------------------------------

    void ChangeState()
    {
        if (!onRest && timeUntilChangeState <= 0)
        {
            timeUntilChangeState = 3f;
            onRest = true;
            DashOn = false;
            SummonOn = false;
            KnifeOn = false;
}
        if (onRest && timeUntilChangeState <= 0)
        {
            onRest = false;
            int rand = Random.Range(0, 3);

            if (rand == 0)
            {
                KnifeOn = true;
                timeUntilChangeState = 13f;
            }
            else if (rand == 1)
            {
                DashOn = true;
                timeUntilChangeState = 4f;
            }
            else if (rand == 2)
            {
                if(Summoncurtime <=0)
                {
                    SummonOn = true;
                    timeUntilChangeState = 8;
                }
            }

        }
    }

    //--------------------------------------------------------------
    void SummonSetPos()
    {
        int SetPosInt = Random.Range(0, 2);
        if (SetPosInt == 0)
        {
            //보스를 기점한 포스값
            int x = Random.Range(-40, 41);
            int y = Random.Range(10, 15);
            pos = new Vector3(BossPos.position.x + x, BossPos.position.y + y, 1);
        }
        if (SetPosInt == 1)
        {
            int x = Random.Range(-40, 41);
            int y = Random.Range(10, 15);
            pos = new Vector3(PlayerPos.position.x + x, PlayerPos.position.y + y);
        }
    }

    void OnActive()
    {
        gameObject.SetActive(true);
    }

    void OnDash()
    {
        DashOn = true;
    }
    void OnStart()
    {
        start = true;
    }

    void Shake()
    {
        Debug.Log("shake");
        Vector3 root = transform.position;
        for (float cool = 10f;cool >=0;)
        {
            float randx = Random.Range(-1f, 1f);
            float randy = Random.Range(-1f, 1f);

            m_offset = new Vector2(transform.position.x + randx, transform.position.y + randy);
            transform.position = m_offset;

            cool -= Time.deltaTime;
        }
        transform.position = root;

    }

    void DieAni()
    {
        //애니가 끝나고, 경험치 획득후(파티클 획득)
        GameObject.Find("Canvas").transform.Find("튜토리얼").transform.Find("스킬").transform.Find("설명 텍스트").gameObject.SetActive(true);
        Invoke("delaytutorial", 1f);
        SaveManager.Instance._playerData.killedBoss1 = true;  //이거 키면 게임 오브젝트 삭제
    }

    void delaytutorial()
    {
        GameObject.Find("Canvas").transform.Find("튜토리얼").transform.Find("스킬").transform.Find("스킬2").gameObject.SetActive(true);
    }
}