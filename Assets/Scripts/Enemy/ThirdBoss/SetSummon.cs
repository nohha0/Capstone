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
        Debug.Log("소환");
        if (Summoncurtime <= 0)
        {
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
                    gameObject.SetActive(false);

                    transform.position = new Vector2(BasePos.position.x, BasePos.position.y + 50);
                    Debug.Log("겁나 소환");


                    Invoke("OnSetActive", 1f);
                    Invoke("summon3", 2);
                    Invoke("Teleport", 7f);  //1초뒤 다시 텔포
                }
            }

        }
        cooltime = false;
        Invoke("cooltrue", Difficulty);
        BeforeSkill = 2; //소환수행완료
        SetSkillNum = Random.Range(0, 4);  //새로운 스킬

    }

    void SummonSetPos()
    {
        if (CurrentPos == 3)  //중앙에 위치
        {
            int x = Random.Range(-55, 56);
            pos = new Vector3(BasePos.position.x + x, BossPos.position.y + 30, 1);

        }
        else
        {
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
