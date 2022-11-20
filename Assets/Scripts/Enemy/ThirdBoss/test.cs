using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class test : Enemy
{
    //������ ��ų ���������� ��
    List<string> SetSkill = new List<string> { "Knife", "Forward", "Summon", "Telepo" };
    public int Knife_Pattern;     //������ ����, ���� ����

    //���� ��ȯ������
    [SerializeField] GameObject Nomal_1;
    [SerializeField] GameObject Nomal_2;
    [SerializeField] GameObject Fly_1;
    [SerializeField] GameObject Fly_2;
    [SerializeField] GameObject Knife;
    [SerializeField] GameObject MagicCircle;

    [SerializeField] Transform BossPos;
    [SerializeField] Transform PlayerPos;
    [SerializeField] Transform BasePos;

    int CurrentPos;         //�ߺ����� ����ġ�� �˷���
    Transform SummonPos;    //���� ��ȯ�� �������� �ϴ� ����
    SpriteRenderer rend;    //�ߺ��� 

    //Skill : 0 = Į �����⽺ų, 1 = ����, 2 = ��ȯ, 3 = ����        

    Rigidbody2D riged;

    float ForWardTime = 0;      //���� �����ϴ� �ð�
    int BeforeSkill;            //�ߺ��� �Ǹ� �ȵǴ� ��ų�� �ֱ� ������ �װ� �Ǵ��ϴ� ���� 
    int SetSkillNum;
    float ForwardSpeed = 180;    //���� ���ǵ�  
    float Difficulty = 6f;           //��ȯ �ӵ�, ���̵�
    bool cooltime = false;
    bool telepo = false;
    int SetMoster;              //��ȯ ���Ÿ�� ����Ÿ�� ���� ����
    Vector3 pos;                //�ʱ�ȭ ����

    float Summoncurtime;
    float Summoncooltime = 30;

    float retelepotime;

    override protected void Start()
    {
        base.Start();
        rend = GetComponent<SpriteRenderer>();
        riged = GetComponent<Rigidbody2D>();

        Invoke("ReTelepo", 2); //�߾ӿ��ִ� ������ ������ ��ġ ���
        //SetSkillNum = Random.Range(0, 4);   //ó�� �� ��ų ����
        Invoke("cooltrue", Difficulty);
        SetSkillNum = 0;
    }

    override protected void Update()
    {
        base.Update();


        if (telepo)  //���� ��ġ ������
        {
            telepo = false;
            ReTelepo();
            retelepotime = 20;

            SetSkillNum = Random.Range(0, 4);
        }
        if (cooltime)
        {
            switch (SetSkill[SetSkillNum])
            {
                case "Knife":       //������
                    if (BeforeSkill == 0)
                    {
                        SetSkillNum = Random.Range(0, 4);
                        break;
                    }
                    KnifeSkill();
                    break;
                case "Forward":     //����
                    if (BeforeSkill == 1)
                    {
                        SetSkillNum = Random.Range(0, 4);
                        break;
                    }
                    ForwardSKill();
                    break;
                case "Summon":      //��ȯ
                    if (BeforeSkill == 2)
                    {
                        SetSkillNum = Random.Range(0, 4);
                        break;
                    }
                    SummonSkill();
                    break;
                case "Telepo":      //����
                    if (BeforeSkill == 3 && retelepotime > 0)
                    {
                        SetSkillNum = Random.Range(0, 4);
                        break;
                    }
                    Teleport();
                    break;
            }
        }
        Summoncurtime -= Time.deltaTime;   //��ȯ��ų ����� 30�� ���� 0�̵Ǹ� �ٽ� ��ȯ
        retelepotime -= Time.deltaTime;
    }

    //��ų


    //--------------------------------------------------------------------


    //���� ��ȯ��ų
    void SummonSkill()
    {
        Debug.Log("��ȯ");
        if (CurrentPos == 2)   //�ߺ��� ��ġ�� �߾��̶��
        {
            //�⺻3, ���� 3 ��ȯ


            Debug.Log("�̳� ��ȯ");
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
        if (Summoncurtime <= 0)
        {
            Summoncurtime = Summoncooltime;
            int SetMoster;

            if (CurrentPos != 2)
            {
                SetMoster = Random.Range(0, 2);
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
            }

        }
        cooltime = false;
        Invoke("cooltrue", Difficulty);
        BeforeSkill = 2; //��ȯ����Ϸ�
        SetSkillNum = Random.Range(0, 4);  //���ο� ��ų

    }

    //���� ���� ��ų
    void ForwardSKill()
    {

        if (ForWardTime < 3f && ForWardTime > 0.5f)
        {
            if (rend.flipX) //������ ����
            {
                transform.Translate(transform.right * speed * Time.deltaTime);
            }
            if (!rend.flipX) //���� ����
            {
                transform.Translate(transform.right * -1 * speed * Time.deltaTime);
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
            telepo = true;
        }
        ForWardTime += Time.deltaTime;  //�ð�����
    }

    void KnifeSkill()
    {
        Debug.Log("������");


        Instantiate(Knife, BossPos, transform);  //�밢 ��


        cooltime = false;
        Invoke("cooltrue", Difficulty);
        BeforeSkill = 0; //����������Ϸ�
        SetSkillNum = Random.Range(0, 4);
    }

    void Teleport()
    {
        gameObject.SetActive(false);
        Debug.Log("����");
        SetSkillNum = 3;  //������ ����
        if (BeforeSkill != 3)
        {
            int a;
            a = CurrentPos;
            CurrentPos = Random.Range(0, 3);   //���� ��ġ ���ϴ� �Լ�
            if (Summoncurtime <= 0)
            {
                if (CurrentPos == 2)   //�߾ӿ� ������ �ȴٸ�
                {
                    //�߾����� �̵��ϴ� �޼ҵ� �ۼ�
                    transform.position = new Vector2(BasePos.position.x, BasePos.position.y + 50);
                    Invoke("OnSetActive", 1f);
                    Invoke("SummonSkill", 2);

                    //�ٽ� �߾ӿܿ� ����
                    Invoke("ReTelepo", 7f);  //1�ʵ� �ٽ� ����
                }
            }
            if (CurrentPos == 0)
            {

                if (a == 0)
                {
                    //����������
                    transform.position = new Vector2(BasePos.position.x + 80, BasePos.position.y);
                    rend.flipX = false;
                }
                else
                {
                    //��������
                    transform.position = new Vector2(BasePos.position.x - 80, BasePos.position.y);
                    rend.flipX = true;
                }
                Invoke("OnSetActive", 1f);
            }
            else if (CurrentPos == 1)
            {
                if (a == 1)
                {
                    //��������
                    transform.position = new Vector2(BasePos.position.x - 80, BasePos.position.y);
                    rend.flipX = true;
                }
                else
                {
                    //����������
                    transform.position = new Vector2(BasePos.position.x + 80, BasePos.position.y);
                    rend.flipX = false;
                }
                Invoke("OnSetActive", 1f);
            }

        }
        cooltime = false;
        Invoke("cooltrue", Difficulty);
        BeforeSkill = 3; //��������Ϸ�
        SetSkillNum = Random.Range(0, 4);
    }
    //-----------------------------------------------------------------------


    //�����Լ�


    //��ȯ ��ġ ���ϴ� �Լ�
    void SummonSetPos()
    {
        if (CurrentPos == 3)  //�߾ӿ� ��ġ
        {
            int x = Random.Range(-55, 56);
            pos = new Vector3(BasePos.position.x + x, BossPos.position.y + 30, 1);

        }
        else
        {
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
    }

    void ReTelepo()
    {
        gameObject.SetActive(false);
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
        Invoke("OnSetActive", 1f);
    }


    void cooltrue()
    {
        cooltime = true;
    }
    void OnSetActive()
    {
        gameObject.SetActive(true);
    }

}