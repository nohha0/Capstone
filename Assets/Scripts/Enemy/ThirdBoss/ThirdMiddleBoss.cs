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
    [SerializeField] protected GameObject tell;

    [SerializeField] protected Transform BossPos;
    [SerializeField] protected Transform PlayerPos;
    [SerializeField] protected Transform BasePos;
    //[SerializeField] protected ParticleSystem ps;

    int CurrentPos =1;         //�ߺ����� ����ġ�� �˷���
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

    protected float Summoncurtime = 20;
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


    bool First = true;

    float Sumontime = 20;
    bool aniF = true;

    

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

        if(!gameObject.activeSelf)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 0.92f, 0.016f, 1);
        }
        if(gameObject.activeSelf&& First)
        {
            First = false;
            Invoke(" Boss3Color", 0.2f);

        }



        if ((targetGameObject.transform.position - transform.position).magnitude <= mag&&BossPlay && HP>300)
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
                    SummonSkill();
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

    }


    private void FixedUpdate()
    {
        if (HP <= 300)
        {
            animator.SetTrigger("����");
            Die.transform.position = transform.position;
            Die.Play();
            Invoke("HP300", 2f);
            
        }
    }


    void KnifeSkill()
    {
        Debug.Log("������");
        Instantiate(Knife,new Vector2(BasePos.position.x,BasePos.position.y+45),transform.rotation);
    }

    void SummonSkill()
    {
        SummonOn = false;
        Debug.Log("�⺻ ��ȯ");
        SummonSetPos();
        Instantiate(MagicCircle, pos, transform.rotation);
        Instantiate(Fly_1, pos, transform.rotation);
        SummonSetPos();
        Instantiate(Fly_1, pos, transform.rotation);
        Instantiate(MagicCircle, pos, transform.rotation);


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
            DashOn = false;
            riged.velocity = Vector2.zero;   //���� ����
            ForWardTime = 0;  //�ٽ� 0           
            aniF = true;
            Teleport();
            timeUntilChangeState = 0;
        }
        ForWardTime += Time.deltaTime;  //�ð�����
    }
    //-----------------------------------------------------------
    void Teleport()
    {
        //��ƼŬ
        Debug.Log("����");
        int a;
        a = CurrentPos;

        if (a == 0)
        {
            //����������
            //ps.Play();
            transform.position = new Vector2(BasePos.position.x + 90, BasePos.position.y);
            rend.flipX = false;
            CurrentPos = 1;
        }
        else
        {
            //��������
            //ps.Play();
            transform.position = new Vector2(BasePos.position.x - 90, BasePos.position.y);
            rend.flipX = true;
            CurrentPos = 0;
        }
    }
    //-----------------------------------------------------------------------

    void ChangeState()
    {
        if (!onRest && timeUntilChangeState <= 0)
        {
            timeUntilChangeState = 1.5f;
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
                timeUntilChangeState = 10f;
            }
            else if (rand == 1)
            {
                //DashOn = true;
                Invoke("DaOn", 0.8f);
                animator.SetTrigger("���");
                timeUntilChangeState = 4f;
            }
            else if (rand == 2)
            {
                if(Summoncurtime <=0)
                {
                    Summoncurtime = 20;
                    SummonOn = true;
                    timeUntilChangeState = 5;
                }
            }

        }
    }

    //--------------------------------------------------------------
    void SummonSetPos()
    {
        int x = Random.Range(-60, 61);
        int y = Random.Range(15, 22);
        if (x > -20 && x < 20)
        {
            x = Random.Range(-60, 61);
        }

        pos = new Vector3(PlayerPos.position.x + x, PlayerPos.position.y + y);
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

    void Boss3Color()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    void DaOn()
    {
        DashOn = true;
    }
    void HP300()
    {
        HP = 0;
        DieAni();
    }


}