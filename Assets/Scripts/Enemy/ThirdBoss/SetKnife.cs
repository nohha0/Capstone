using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetKnife : ThirdMiddleBoss
{
    override protected void Start()
    {
        
    }

    // Update is called once per frame
    override protected void Update()
    {
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

}
