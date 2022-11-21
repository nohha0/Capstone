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
    [SerializeField] protected GameObject Nomal_1;
    [SerializeField] protected GameObject Nomal_2;
    [SerializeField] protected GameObject Fly_1;
    [SerializeField] protected GameObject Fly_2;
    [SerializeField] protected GameObject Knife;
    [SerializeField] protected GameObject MagicCircle;

    [SerializeField] protected Transform BossPos;
    [SerializeField] protected Transform PlayerPos;
    [SerializeField] protected Transform BasePos;

    protected int CurrentPos;         //중보스에 현위치를 알려줌
    protected Transform SummonPos;    //몬스터 소환시 기점으로 하는 포스
    public SpriteRenderer rend;    //중보스 

    //Skill : 0 = 칼 던지기스킬, 1 = 돌진, 2 = 소환, 3 = 텔포        

    public Rigidbody2D riged;

    protected float ForWardTime = 0;      //돌진 진행하는 시간
    protected int BeforeSkill;            //중복이 되면 안되는 스킬이 있기 때문에 그걸 판단하는 정수 
    protected int SetSkillNum;
    protected float ForwardSpeed = 180;    //돌진 스피드  
    protected float Difficulty = 6f;           //전환 속도, 난이도
    protected bool cooltime = false;
    protected bool telepo = false;
    protected bool ForAdd = false;
    protected int SetMoster;              //소환 노멀타입 비행타입 결정 정수
    protected Vector3 pos;                //초기화 변수

    protected float Summoncurtime = 0;
    protected float Summoncooltime = 30;

    protected float retelepotime;




    //------------------------------------
    public bool DashOn = false;
    public bool SummonOn = false;
    public bool KnifeOn = false;
    bool start = false;

    int Two_Knife = 0;

    //private Dash script;


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
        //SetSkillNum = Random.Range(0, 4);   //처음 할 스킬 고르기
        //SetSkillNum = 0;
        //InvokeRepeating("Teleport", 0, 4);
        //InvokeRepeating("OnDash", 5, 10);

        //---------------------------------------------------------------
        //script = GameObject.Find("Player").GetComponent<Dash>();


    }

    override protected void Update()
    {
        base.Update();
        ChangeState();

        if(start)
        {
            if (DashOn) ForwardSKill();
            if (KnifeOn)
            {
                KnifeOn = false;
                KnifeSkill();
                Invoke("KnifeSkill", 6);
                Invoke("Teleport", 5);

            }
            if (SummonOn)
            {
                SummonSkill();
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


    void KnifeSkill()
    {
        Instantiate(Knife, BossPos, transform);
    }

    void SummonSkill()
    {
        Debug.Log("소환");
        Summoncurtime = Summoncooltime;
        int SetMoster;

        if (CurrentPos != 2)
        {
            SetMoster = Random.Range(0, 3);
            if (SetMoster == 0)
            {
                //기본 4
                Debug.Log("기본 소환");
                for (int i = 0; i < 3; i++)
                {

                    SummonSetPos();
                    Instantiate(MagicCircle, pos, transform.rotation);
                    Instantiate(Nomal_1, pos, transform.rotation);

                }
                for (int i = 0; i < 1; i++)
                {

                    SummonSetPos();
                    Instantiate(MagicCircle, pos, transform.rotation);
                    Instantiate(Nomal_2, pos, transform.rotation);

                }
            }
            if (SetMoster == 1)
            {
                //비행 4
                Debug.Log("비행 소환");
                for (int i = 0; i < 3; i++)
                {

                    SummonSetPos();
                    Instantiate(MagicCircle, pos, transform.rotation);
                    Instantiate(Fly_1, pos, transform.rotation);

                }
                for (int i = 0; i < 1; i++)
                {

                    SummonSetPos();
                    Instantiate(MagicCircle, pos, transform.rotation);
                    Instantiate(Fly_2, pos, transform.rotation);

                }
            }
            if (SetMoster == 2)   //중보스 위치가 중앙이라면
            {
                //기본3, 비행 3 소환
                //gameObject.SetActive(false);

                transform.position = new Vector2(BasePos.position.x, BasePos.position.y + 50);
                Debug.Log("겁나 소환");


                //Invoke("OnSetActive", 1f);
                Invoke("summon3", 2);
                //Invoke("Teleport", 7f);  //1초뒤 다시 텔포
            }
        }
        //cooltime = false;
        //Invoke("cooltrue", Difficulty);
        //BeforeSkill = 2; //소환수행완료
        //SetSkillNum = Random.Range(0, 4);  //새로운 스킬

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

            if (rend.flipX)
            {
                transform.Translate(transform.right * ForwardSpeed * Time.deltaTime);
            }
            if (!rend.flipX)
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
        //gameObject.SetActive(false);
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
            onRest = true;
            timeUntilChangeState = 1f;
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
        /*if (CurrentPos == 3)  //중앙에 위치
        {
            int x = Random.Range(-55, 56);
            pos = new Vector3(BasePos.position.x + x, BossPos.position.y + 30, 1);

        }*/
        int SetPosInt = Random.Range(0, 2);
        if (SetPosInt == 0)
        {
            //보스를 기점한 포스값
            int x = Random.Range(-40, 41);
            int y = Random.Range(0, 11);
            pos = new Vector3(BossPos.position.x + x, BossPos.position.y + y, 1);
        }
        if (SetPosInt == 1)
        {
            int x = Random.Range(-40, 41);
            int y = Random.Range(0, 11);
            pos = new Vector3(PlayerPos.position.x + x, PlayerPos.position.y + y);
        }
    }

    void summon3()
    {
        for (int i = 0; i < 2; i++)
        {
            SummonSetPos();
            Instantiate(MagicCircle, pos, transform.rotation);
            Instantiate(Nomal_2, pos, transform.rotation);

        }
        for (int i = 0; i < 2; i++)
        {

            SummonSetPos();
            Instantiate(MagicCircle, pos, transform.rotation);
            Instantiate(Fly_2, pos, transform.rotation);

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
}