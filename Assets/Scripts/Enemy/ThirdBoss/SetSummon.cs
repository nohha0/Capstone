using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSummon : ThirdMiddleBoss
{
    // Start is called before the first frame update
    override protected void Start()
    {
        
    }

    // Update is called once per frame
    override protected void Update()
    {
        
    }

    void SummonSkill()
    {
        Debug.Log("��ȯ");
        if (Summoncurtime <= 0)
        {
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
                    gameObject.SetActive(false);

                    transform.position = new Vector2(BasePos.position.x, BasePos.position.y + 50);
                    Debug.Log("�̳� ��ȯ");


                    Invoke("OnSetActive", 1f);
                    Invoke("summon3", 2);
                    Invoke("Teleport", 7f);  //1�ʵ� �ٽ� ����
                }
            }

        }
        cooltime = false;
        Invoke("cooltrue", Difficulty);
        BeforeSkill = 2; //��ȯ����Ϸ�
        SetSkillNum = Random.Range(0, 4);  //���ο� ��ų

    }

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
}
