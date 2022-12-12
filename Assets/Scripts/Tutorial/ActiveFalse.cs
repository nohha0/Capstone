using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveFalse : MonoBehaviour
{
    
    float time = 0;
    Image image;
    PlayerController Move;
    void Start()
    {
        Move = GameObject.Find("Player").GetComponent<PlayerController>();
        image = GetComponent<Image>();
    }

    void Update()
    {
        if(gameObject.activeSelf)
        {
            Move.movable = false;
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
                    Move.movable = true;
                }
            }
            time += Time.deltaTime;
        }

    }
}
