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

        Invoke("Teleport", 2); //�߾ӿ��ִ� ������ ������ ��ġ ���
        //SetSkillNum = Random.Range(0, 4);   //ó�� �� ��ų ����
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
            Debug.Log("����");
            riged.velocity = Vector2.zero;   //���� ����
            ForWardTime = 0;  //�ٽ� 0           
            Teleport();
            DashOn = false;
        }
        ForWardTime += Time.deltaTime;  //�ð�����
    }

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


    void OnActive()
    {
        gameObject.SetActive(true);
    }

    void OnDash()
    {
        DashOn = true;
    }
}