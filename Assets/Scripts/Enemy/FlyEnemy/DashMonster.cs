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
    public bool ani = false;

    override protected void Start()
    {
        base.Start();

        curtime = 6;
        //gameObject.SetActive(false);
        //GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        //Invoke("OnSetActive", 1f);
        DieStage = 6;
    }

    // Update is called once per frame
    override protected void Update()
    {


        if (HP <= 0)
        {
            rigid.gravityScale = 300f;
            animator.SetTrigger("다이");
            spriteRend.color = new Color(0.8f, 0.8f, 0.8f);
            Invoke("DIE", 1.7f);
        }
        //-----------------------------------------------



        UpdateTarget();

        if(curtime <=0)
        {
            if(ani)
            {

                if (setLook)
                {
                    forward = (targetGameObject.transform.position - transform.position).normalized; //방향 설정
                    setLook = false;
                    FlipOn = false;
                }
                if ((targetGameObject.transform.position - transform.position).magnitude <= mag)
                    transform.Translate(forward * Dashspeed * Time.deltaTime);
                Invoke("SETCUR", 1.5f);
            }

        }
        curtime -= Time.deltaTime;


        if (DiecurStage.Stage != DieStage && DiecurStage.Stage != 5)
        {

            Destroy(gameObject);
        }
    }

    void SETCUR()
    {
        FlipOn = true;
        curtime = cooltime;
        setLook = true;
        ani = true;
    }
    void OnSetActive()
    {
        gameObject.SetActive(true);
    }
    public override void DIE()
    {
        base.DIE();
    }
}
