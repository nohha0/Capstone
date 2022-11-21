using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class ThirdMiddleBoss : Enemy
{
    //������ ��ų ���������� ��
    List<string> SetSkill = new List<string> { "Knife", "Forward", "Summon", "Telepo" };
    public int Knife_Pattern;     //������ ����, ���� ����

    //���� ��ȯ������
    [SerializeField] protected GameObject Nomal_1;
    [SerializeField] protected GameObject Nomal_2;
    [SerializeField] protected GameObject Fly_1;
    [SerializeField] protected GameObject Fly_2;
    [SerializeField] protected GameObject Knife;
    [SerializeField] protected GameObject MagicCircle;

    [SerializeField] protected Transform BossPos;
    [SerializeField] protected Transform PlayerPos;
    [SerializeField] protected Transform BasePos;

    protected int CurrentPos;         //�ߺ����� ����ġ�� �˷���
    protected Transform SummonPos;    //���� ��ȯ�� �������� �ϴ� ����
    public SpriteRenderer rend;    //�ߺ��� 

    //Skill : 0 = Į �����⽺ų, 1 = ����, 2 = ��ȯ, 3 = ����        

    public Rigidbody2D riged;

    protected float ForWardTime = 0;      //���� �����ϴ� �ð�
    protected int BeforeSkill;            //�ߺ��� �Ǹ� �ȵǴ� ��ų�� �ֱ� ������ �װ� �Ǵ��ϴ� ���� 
    protected int SetSkillNum;
    protected float ForwardSpeed = 180;    //���� ���ǵ�  
    protected float Difficulty = 6f;           //��ȯ �ӵ�, ���̵�
    protected bool cooltime = false;
    protected bool telepo = false;
    protected bool ForAdd = false;
    protected int SetMoster;              //��ȯ ���Ÿ�� ����Ÿ�� ���� ����
    protected Vector3 pos;                //�ʱ�ȭ ����

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

        Invoke("Teleport", 2); //�߾ӿ��ִ� ������ ������ ��ġ ���
        Invoke("OnStart", 2);
        InvokeRepeating("OnActive", 5, 3);
        //SetSkillNum = Random.Range(0, 4);   //ó�� �� ��ų ����
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
        Debug.Log("��ȯ");
        Summoncurtime = Summoncooltime;
        int SetMoster;

        if (CurrentPos != 2)
        {
            SetMoster = Random.Range(0, 3);
            if (SetMoster == 0)
            {
                //�⺻ 4
                Debug.Log("�⺻ ��ȯ");
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
                //���� 4
                Debug.Log("���� ��ȯ");
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
            if (SetMoster == 2)   //�ߺ��� ��ġ�� �߾��̶��
            {
                //�⺻3, ���� 3 ��ȯ
                //gameObject.SetActive(false);

                transform.position = new Vector2(BasePos.position.x, BasePos.position.y + 50);
                Debug.Log("�̳� ��ȯ");


                //Invoke("OnSetActive", 1f);
                Invoke("summon3", 2);
                //Invoke("Teleport", 7f);  //1�ʵ� �ٽ� ����
            }
        }
        //cooltime = false;
        //Invoke("cooltrue", Difficulty);
        //BeforeSkill = 2; //��ȯ����Ϸ�
        //SetSkillNum = Random.Range(0, 4);  //���ο� ��ų

    }
    //-------------------------------------------------------
    public void ForwardSKill()
    {

        if (ForWardTime < 2f)
        {
            if (rend.flipX) //������ ����
            {
                transform.Translate(transform.right * ForwardSpeed * Time.deltaTime);
            }
            if (!rend.flipX) //���� ����
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
            riged.velocity = Vector2.zero;   //���� ����
            ForWardTime = 0;  //�ٽ� 0           
            Teleport();
            DashOn = false;
            timeUntilChangeState = 0;
        }
        ForWardTime += Time.deltaTime;  //�ð�����
    }
    //-----------------------------------------------------------
    void Teleport()
    {
        //gameObject.SetActive(false);
        Debug.Log("����");
        int a;
        a = CurrentPos;
        CurrentPos = Random.Range(0, 2);   //���� ��ġ ���ϴ� �Լ�         
        if (CurrentPos == 0)   //��������
        {

            if (a == 0)
            {
                //����������
                transform.position = new Vector2(BasePos.position.x + 80, BasePos.position.y);
                rend.flipX = false;
                CurrentPos = 1;
            }
            else
            {
                //��������
                transform.position = new Vector2(BasePos.position.x - 80, BasePos.position.y);
                rend.flipX = true;
            }
            Invoke("OnActive", 1f);
        }
        else if (CurrentPos == 1)  //����������
        {
            if (a == 1)
            {
                //��������
                transform.position = new Vector2(BasePos.position.x - 80, BasePos.position.y);
                rend.flipX = true;
                CurrentPos = 0;
            }
            else
            {
                //����������
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
        /*if (CurrentPos == 3)  //�߾ӿ� ��ġ
        {
            int x = Random.Range(-55, 56);
            pos = new Vector3(BasePos.position.x + x, BossPos.position.y + 30, 1);

        }*/
        int SetPosInt = Random.Range(0, 2);
        if (SetPosInt == 0)
        {
            //������ ������ ������
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