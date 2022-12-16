using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skillDelray : MonoBehaviour
{
    public float MaxTime;
    float curtime;
    public int skillnum;
    float bar;
    Image Im;
    void Start()
    {
        curtime = MaxTime;
        Im = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(skillnum == 1)
        {
            X();
        }
        if (skillnum == 2)
        {
            A();
        }
        if (skillnum == 3)
        {
            S();
        }
        bar = curtime / MaxTime;
        Im.fillAmount = bar;
        curtime -= Time.deltaTime;
    }

    void X()
    {
        if(Input.GetKeyDown(KeyCode.X) && curtime <= 0)
        {
            curtime = MaxTime;
        }
    }

    void A()
    {
        if (Input.GetKeyDown(KeyCode.A)&& curtime <= 0)
        {
            curtime = MaxTime;
        }
    }

    void S()
    {
        if (Input.GetKeyDown(KeyCode.S) && curtime <= 0)
        {
            curtime = MaxTime;
        }
    }
}
