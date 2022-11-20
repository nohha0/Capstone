using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLight : FlyingMonster
{
    public GameObject Lightwave;
    float cooltime = 5;
    float curtime;
    float time = 0;

    override protected void Start()
    {
        base.Start();
        curtime = 2;
        gameObject.SetActive(false);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        Invoke("OnSetActive", 1.5f);
    }

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

        if(curtime <= 0&&SetOn)
        {
            
            Instantiate(Lightwave, pos.position, transform.rotation);
            curtime = cooltime;
        }
        curtime -= Time.deltaTime;

    }
    void OnSetActive()
    {
        gameObject.SetActive(true);
    }

}
