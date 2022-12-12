using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayActiveFalse : MonoBehaviour
{
    Text text;
    float time = 0;
    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        if(gameObject.activeSelf)
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
