using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveFalse : MonoBehaviour
{
    
    float time = 0;
    Image image;
    PlayerController Move;

    float Starttime = 0.7f;

    GameObject Upgrade;
    bool start = false;
    void Start()
    {
        Move = GameObject.Find("Player").GetComponent<PlayerController>();
        image = GetComponent<Image>();
        image.color = new Color(1, 1, 1, 0);
        Upgrade = GameObject.Find("Canvas").transform.Find("UpgradePanel").gameObject;
    }

    void Update()
    {
        if(Starttime <= 0)
        {
            start = true;
        }
        if(Upgrade.activeSelf)
        {
            Starttime = 2f;
        }
        if(!Upgrade.activeSelf)
        {
            Starttime -= Time.deltaTime;
        }


        if(gameObject.activeSelf && start)
        {
            Move.IsMove = false;
            if (time < 0.5f)
            {
                image.color = new Color(1, 1, 1, time / 0.5f);
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    gameObject.SetActive(false);
                    time = 0;
                    //Debug.Log("คั");
                    Move.IsMove = true;
                }
            }
            time += Time.deltaTime;
        }

    }

}
