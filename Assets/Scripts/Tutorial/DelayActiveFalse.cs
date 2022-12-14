using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayActiveFalse : MonoBehaviour
{
    Text text;
    float time = 0;

    float Starttime = 0.3f;

    GameObject Upgrade;
    bool start = false;

    public bool Boss;
    void Start()
    {
        text = GetComponent<Text>();
        text.color = new Color(1, 1, 1, 0);
        Upgrade = GameObject.Find("Canvas").transform.Find("튜토리얼").transform.Find("스킬").transform.Find("UpgradePanel").gameObject;
        if(Boss)
        {
            Starttime = 2f;
        }
    }

    void Update()
    {
        if (Starttime <= 0)
        {
            start = true;
        }
        if (Upgrade.activeSelf)
        {
            Starttime = 1.5f;
        }
        if (!Upgrade.activeSelf)
        {
            Starttime -= Time.deltaTime;
        }

        if (gameObject.activeSelf && !Upgrade.activeSelf&&start)
        {

            if(time <0.5f)
            {
                text.color = new Color(1, 1, 1, time / 0.5f);
            }
            else
            {
                Invoke("Activefalse", 3f);

            }
            time += Time.deltaTime;

        }
    }
    void Activefalse()
    {
        gameObject.SetActive(false);
        time = 0;
    }
}
