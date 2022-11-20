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

    protected float Summoncurtime;
    protected float Summoncooltime = 30;

    protected float retelepotime;




    //------------------------------------
    public bool DashOn = false;
    public bool SummonOn = false;
    public bool KnifeOn = false;

    //private Dash script;

    override protected void Start()
    {
        base.Start();
        rend = GetComponent<SpriteRenderer>();
        riged = GetComponent<Rigidbody2D>();

        Invoke("Teleport", 2); //중앙에있던 보스를 텔포로 위치 잡기
        //SetSkillNum = Random.Range(0, 4);   //처음 할 스킬 고르기
        SetSkillNum = 0;
        //InvokeRepeating("Teleport", 0, 4);
        InvokeRepeating("OnDash", 5, 10);

        //---------------------------------------------------------------
        //script = GameObject.Find("Player").GetComponent<Dash>();


    }

    override protected void Update()
    {
        base.Update();

        if(DashOn)
        {
            ForwardSKill();
        }

    }



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
            Debug.Log("돌진");
            riged.velocity = Vector2.zero;   //동작 정지
            ForWardTime = 0;  //다시 0           
            Teleport();
            DashOn = false;
        }
        ForWardTime += Time.deltaTime;  //시간증가
    }

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


    void OnActive()
    {
        gameObject.SetActive(true);
    }

    void OnDash()
    {
        DashOn = true;
    }
}