using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLight : FlyingMonster
{
    public GameObject Lightwave;
    float cooltime = 5;
    float curtime;

    override protected void Start()
    {
        base.Start();
        curtime = 2;
    }

    override protected void Update()
    {
        if (HP <= 0) DIE();

        UpdateTarget();

        if(curtime <= 0&&SetOn)
        {
            
            Instantiate(Lightwave, pos.position, transform.rotation);
            curtime = cooltime;
        }
        curtime -= Time.deltaTime;

    }
}
