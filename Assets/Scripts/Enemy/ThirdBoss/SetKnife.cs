using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetKnife : ThirdMiddleBoss
{
    bool KnifeOn = true;
    override protected void Start()
    {
        
    }

    // Update is called once per frame
    override protected void Update()
    {
        if (KnifeOn)
        {
            KnifeOn = false;
            KnifeSkill();
            Invoke("KnifeSkill", 6);
            //Invoke("Teleport", 5);

        }

    }

    void KnifeSkill()
    {
        Debug.Log("나이프");


        Instantiate(Knife, BossPos, transform);  //대각 위


        cooltime = false;
        Invoke("cooltrue", Difficulty);
        BeforeSkill = 0; //나이프수행완료
        SetSkillNum = Random.Range(0, 4);
    }

}
