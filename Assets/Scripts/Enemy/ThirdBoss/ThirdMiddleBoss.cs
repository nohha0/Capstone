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
    [SerializeField] GameObject Nomal_1;
    [SerializeField] GameObject Nomal_2;
    [SerializeField] GameObject Fly_1;
    [SerializeField] GameObject Fly_2;
    [SerializeField] GameObject Knife;
    [SerializeField] GameObject MagicCircle;

    [SerializeField] Transform BossPos;
    [SerializeField] Transform PlayerPos;
    [SerializeField] Transform BasePos;

    int CurrentPos;         //중보스에 현위치를 알려줌
    Transform SummonPos;    //몬스터 소환시 기점으로 하는 포스
    SpriteRenderer rend;    //중보스 

    //Skill : 0 = 칼 던지기스킬, 1 = 돌진, 2 = 소환, 3 = 텔포        
   
    Rigidbody2D riged;

    float ForWardTime = 0;      //돌진 진행하는 시간
    int BeforeSkill;            //중복이 되면 안되는 스킬이 있기 때문에 그걸 판단하는 정수 
    int SetSkillNum;
    float ForwardSpeed = 180;    //돌진 스피드  
    float Difficulty = 6f;           //전환 속도, 난이도
    bool cooltime = false;
    bool telepo = false;
    Vector3 pos;                //초기화 변수

    float Summoncurtime;
    float Summoncooltime = 20;

    override protected void Start()
    {
        base.Start();
        rend = GetComponent<SpriteRenderer>();
        riged = GetComponent<Rigidbody2D>();

        Invoke("ReTelepo", 2); //중앙에있던 보스를 텔포로 위치 잡기
        //SetSkillNum = Random.Range(0, 4);   //처음 할 스킬 고르기
        Invoke("cooltrue", Difficulty);
        SetSkillNum = 0;
    }

    override protected void Update()
    {
        base.Update();


        if (telepo)  //돌진 위치 재정립
        {
            telepo = false;
            ReTelepo();
            SetSkillNum = Random.Range(0, 4);
        }
        if (cooltime)
        {
            switch (SetSkill[SetSkillNum])
            {
                case "Knife":       //나이프
                    if (BeforeSkill == 0)
                    {
                        SetSkillNum = Random.Range(0, 4);
                        break;
                    }
                    KnifeSkill();
                    break;
                case "Forward":     //돌진
                    if (BeforeSkill == 1)
                    {
                        SetSkillNum = Random.Range(0, 4);
                        break;
                    }
                    ForwardSKill();
                    break;
                case "Summon":      //소환
                    if (BeforeSkill == 2)
                    {
                        SetSkillNum = Random.Range(0, 4);
                        break;
                    }
                    SummonSkill();
                    break;
                case "Telepo":      //텔포
                    if (BeforeSkill == 3)
                    {
                        SetSkillNum = Random.Range(0, 4);
                        break;
                    }
                    Teleport();
                    break;
            }
        }
        Summoncurtime -= Time.deltaTime;   //소환스킬 사용후 30초 지나 0이되면 다시 소환

    }

    //스킬


    //--------------------------------------------------------------------


    //몬스터 소환스킬
    void SummonSkill()
    {
        Debug.Log("소환");
        if (CurrentPos == 2)   //중보스 위치가 중앙이라면
        {
            //기본3, 비행 3 소환


            Debug.Log("겁나 소환");
            for (int i = 0; i < 3; i++)
            {
                SummonSetPos();
                Instantiate(MagicCircle, pos, transform.rotation);
                Instantiate(Nomal_2, pos, transform.rotation);

            }
            for (int i = 0; i < 3; i++)
            {

                SummonSetPos();
                Instantiate(MagicCircle, pos, transform.rotation);
                Instantiate(Fly_2, pos, transform.rotation);

            }
        }
        if (Summoncurtime <= 0)
        {
            Summoncurtime = Summoncooltime;
            int SetMoster;

            if (CurrentPos != 2)
            {
                SetMoster = Random.Range(0, 2);
                if (SetMoster == 0)
                {
                    //기본 4
                    Debug.Log("기본 소환");
                    for (int i = 0; i < 2; i++)
                    {

                        SummonSetPos();
                        Instantiate(MagicCircle, pos, transform.rotation);
                        Instantiate(Nomal_1, pos, transform.rotation);

                    }
                    for (int i = 0; i < 2; i++)
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
                    for (int i = 0; i < 2; i++)
                    {

                        SummonSetPos();
                        Instantiate(Fly_1, pos, transform.rotation);

                    }
                    for (int i = 0; i < 2; i++)
                    {

                        SummonSetPos();
                        Instantiate(Fly_2, pos, transform.rotation);

                    }
                }
            }

        }
        cooltime = false;
        Invoke("cooltrue", Difficulty);
        BeforeSkill = 2; //소환수행완료
        SetSkillNum = Random.Range(0, 4);  //새로운 스킬

    }

    //보스 돌진 스킬
    void ForwardSKill()
    {

        if (ForWardTime < 2f)
        {
            if (rend.flipX) //오른쪽 돌진
            {
                transform.Translate(transform.right * speed * Time.deltaTime);
            }
            if (!rend.flipX) //왼쪽 돌진
            {
                transform.Translate(transform.right * -1 * speed * Time.deltaTime);
            }

            if(rend.flipX)
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
            telepo = true;
        }
        ForWardTime += Time.deltaTime;  //시간증가
    }

    void KnifeSkill()
    {
        // 랜덤함수  | b = 2
        // 소환 대각 위에서 대각 아래로

        //대각 아래에서 대각 위로

        //정면으로

        Debug.Log("나이프");
        Knife_Pattern = Random.Range(1, 4);
        switch(Knife_Pattern)
        {
            case 1:
                Instantiate(Knife, BossPos);  //정면
                break;
            case 2:
                Instantiate(Knife, BossPos);  //대각 위
                break;
            case 3:
                Vector3 down = new Vector3(BossPos.position.x, BossPos.position.y, 1);
                Instantiate(Knife, down,transform.rotation);  //대각아래
                break;
        }
        cooltime = false;
        Invoke("cooltrue", Difficulty);
        BeforeSkill = 0; //나이프수행완료
        SetSkillNum = Random.Range(0, 4);
    }

    void Teleport()
    {
        Debug.Log("텔포");
        SetSkillNum = 3;  //텔포로 지정
        if (BeforeSkill != 3)
        {
            int a;
            a = CurrentPos;
            CurrentPos = Random.Range(0, 3);   //텔포 위치 정하는 함수
            if (CurrentPos == 2)   //중앙에 텔포가 된다면
            {
                //중앙으로 이동하는 메소드 작성
                transform.position = new Vector2(BasePos.position.x, BasePos.position.y + 50);
                Invoke("SummonSkill", 2);

                //다시 중앙외에 텔포
                Invoke("ReTelepo", 2.5f);  //1초뒤 다시 텔포
            }
            else if (CurrentPos == 0)
            {

                if (a == 0)
                {
                    //오른쪽텔포
                    transform.position = new Vector2(BasePos.position.x + 80, BasePos.position.y);
                    rend.flipX = false;
                }
                else
                {
                    //왼쪽텔포
                    transform.position = new Vector2(BasePos.position.x - 80, BasePos.position.y);
                    rend.flipX = true;
                }
            }
            else
            {
                if (a == 1)
                {
                    //왼쪽텔포
                    transform.position = new Vector2(BasePos.position.x - 80, BasePos.position.y);
                    rend.flipX = true;
                }
                else
                {
                    //오른쪽텔포
                    transform.position = new Vector2(BasePos.position.x + 80, BasePos.position.y);
                    rend.flipX = false;
                }
            }

        }
        cooltime = false;
        Invoke("cooltrue", Difficulty);
        BeforeSkill = 3; //텔포수행완료
        SetSkillNum = Random.Range(0, 4);
    }
    //-----------------------------------------------------------------------


    //보조함수


    //소환 위치 정하는 함수
    void SummonSetPos()
    {
        if (CurrentPos == 3)  //중앙에 위치
        {
            int x = Random.Range(-50, 51);
            pos = new Vector3(BasePos.position.x + x, BossPos.position.y, 1);
        }
        else
        {
            int SetPosInt = Random.Range(0, 2);
            if (SetPosInt == 0)
            {
                //보스를 기점한 포스값
                int x = Random.Range(-20, 21);
                int y = Random.Range(-10, 11);
                pos = new Vector3(BossPos.position.x + x, BossPos.position.y + y, 1);
            }
            if (SetPosInt == 1)
            {
                int x = Random.Range(-40, 41);
                int y = Random.Range(-10, 11);
                pos = new Vector3(PlayerPos.position.x + x, PlayerPos.position.y + y);
            }
        }
    }

    void ReTelepo()
    {
        CurrentPos = Random.Range(0, 2);
        if (CurrentPos == 0)
        {
            transform.position = new Vector2(BasePos.position.x - 80, BasePos.position.y);
            rend.flipX = true;
        }
        else
        {
            transform.position = new Vector2(BasePos.position.x + 80, BasePos.position.y);
            rend.flipX = false;
        }
    }

    void cooltrue()
    {
        cooltime = true;
    }

}