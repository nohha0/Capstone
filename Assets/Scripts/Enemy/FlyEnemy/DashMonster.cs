using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMonster : FlyingMonster
{
    Vector2 forward;
    bool setLook = true;
    float cooltime = 4;
    float curtime;
    float Dashspeed = 110;

    override protected void Start()
    {
        base.Start();

        curtime = 2;
    }

    // Update is called once per frame
    override protected void Update()
    {
        if (HP <= 0) DIE();

        UpdateTarget();

        if(curtime <=0)
        {
            if(setLook)
            {
                forward = (targetGameObject.transform.position - transform.position).normalized; //방향 설정
                setLook = false;
            }
            transform.Translate(forward * Dashspeed * Time.deltaTime);
            Invoke("SETCUR", 1.5f);

        }
        curtime -= Time.deltaTime;
    }

    void SETCUR()
    {
        curtime = cooltime;
        setLook = true;
    }
}
