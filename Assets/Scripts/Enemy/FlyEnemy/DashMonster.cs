using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMonster : FlyingMonster
{
    Vector2 forward;
    bool setLook = true;
    float cooltime = 6;
    float curtime;
    float Dashspeed = 110;
    float time = 0;
    bool ani = true;

    override protected void Start()
    {
        base.Start();

        curtime = 6;
        //gameObject.SetActive(false);
        //GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        //Invoke("OnSetActive", 1f);
    }

    // Update is called once per frame
    override protected void Update()
    {



        //-----------------------------------------------


        if (HP <= 0) DIE();

        UpdateTarget();

        if(curtime<=0.5f)
        {
            animator.SetTrigger("DashAttack");
        }
        if(curtime <=0)
        {
            
            if (setLook)
            {
                forward = (targetGameObject.transform.position - transform.position).normalized; //방향 설정
                setLook = false;
            }
            if ((targetGameObject.transform.position - transform.position).magnitude <= mag)
                transform.Translate(forward * Dashspeed * Time.deltaTime);
            Invoke("SETCUR", 1.5f);

        }
        curtime -= Time.deltaTime;
    }

    void SETCUR()
    {
        curtime = cooltime;
        setLook = true;
        ani = true;
    }
    void OnSetActive()
    {
        gameObject.SetActive(true);
    }
}
