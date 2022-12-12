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
    [SerializeField] protected GameObject Fly_1;
    [SerializeField] protected GameObject Knife;
    [SerializeField] protected GameObject MagicCircle;

    [SerializeField] protected Transform BossPos;
    [SerializeField] protected Transform PlayerPos;
    [SerializeField] protected Transform BasePos;

    int CurrentPos;         //�ߺ����� ����ġ�� �˷���
    Transform SummonPos;    //���� ��ȯ�� �������� �ϴ� ����
    public SpriteRenderer rend;    //�ߺ��� 

    //Skill : 0 = Į �����⽺ų, 1 = ����, 2 = ��ȯ, 3 = ����        

    public Rigidbody2D riged;

    float ForWardTime = 0;      //���� �����ϴ� �ð�
    int BeforeSkill;            //�ߺ��� �Ǹ� �ȵǴ� ��ų�� �ֱ� ������ �װ� �Ǵ��ϴ� ���� 
    int SetSkillNum;
    float ForwardSpeed = 180;    //���� ���ǵ�  
    protected bool cooltime = false;
    protected Vector3 pos;                //�ʱ�ȭ ����

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

        Invoke("Teleport", 2); //�߾ӿ��ִ� ������ ������ ��ġ ���
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
            //�ִϸ��̼� �����鼭 ����ġ �ּ�
            //�ִ��Լ� ȣ���ؾ��ϴµ� ������ �׳� ���� ��ų ȹ�� ȣ��
            DieAni();
        }
    }



    void KnifeSkill()
    {
        Debug.Log("������");
        Instantiate(Knife,transform);
    }

    void SummonSkill()
    {
        Debug.Log("�⺻ ��ȯ");
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
            if (rend.flipX) //������ ����
            {
                transform.Translate(transform.right * ForwardSpeed * Time.deltaTime);
            }
            if (!rend.flipX) //���� ����
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
        gameObject.SetActive(false);
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
            //������ ������ ������
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
        //�ִϰ� ������, ����ġ ȹ����(��ƼŬ ȹ��)
        GameObject.Find("Canvas").transform.Find("Ʃ�丮��").transform.Find("��ų").transform.Find("���� �ؽ�Ʈ").gameObject.SetActive(true);
        Invoke("delaytutorial", 1f);
        SaveManager.Instance._playerData.killedBoss1 = true;  //�̰� Ű�� ���� ������Ʈ ����
    }

    void delaytutorial()
    {
        GameObject.Find("Canvas").transform.Find("Ʃ�丮��").transform.Find("��ų").transform.Find("��ų2").gameObject.SetActive(true);
    }
}