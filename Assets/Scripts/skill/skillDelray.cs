using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skillDelray : MonoBehaviour
{
    public float MaxTime;
    float curtime;
    public int skillnum;

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
        curtime = Time.deltaTime;
    }

    void X()
    {
        float bar = curtime / MaxTime;
        Im.fillAmount = bar;
        if(Input.GetKeyDown(KeyCode.X))
        {
            curtime = MaxTime;
        }
    }

    void A()
    {
        float bar = curtime / MaxTime;
        Im.fillAmount = bar;
        if (Input.GetKeyDown(KeyCode.A))
        {
            curtime = MaxTime;
        }
    }

    void S()
    {
        float bar = curtime / MaxTime;
        Im.fillAmount = bar;
        if (Input.GetKeyDown(KeyCode.S))
        {
            curtime = MaxTime;
        }
    }
}
