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
    float time = 0;

    override protected void Start()
    {
        base.Start();

        curtime = 2;
        gameObject.SetActive(false);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        Invoke("OnSetActive", 1.5f);
    }

    // Update is called once per frame
    override protected void Update()
    {

        if (gameObject.activeSelf)
        {
            if (time <= 3f)
            {
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, time / 3);
            }
            time += Time.deltaTime;
        }


        //-----------------------------------------------


        if (HP <= 0) DIE();

        UpdateTarget();

        if(curtime <=0)
        {
            if(setLook)
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
    }
    void OnSetActive()
    {
        gameObject.SetActive(true);
    }
}
