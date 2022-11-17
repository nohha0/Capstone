using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Branch : MonoBehaviour
{
    public float speed;
    bool state1;
    bool state2;
    bool state3;
    bool state4;
    float timeToState2;
    float timeToState3;
    float timeToState4;
    float timeToDestroy;

    //  0.5up/0.5down/1up/1down

    void Start()
    {
        state1 = true;
        timeToState2 = 0.5f;
        timeToState3 = 0.5f;
        timeToState4 = 1f;
        timeToDestroy = 1f;
    }

    void Update()
    {
        if (state1)
        {
            transform.Translate(new Vector2(0, speed));
            timeToState2 -= Time.deltaTime;
            if (timeToState2 <= 0)
            {
                state1 = false;
                state2 = true;
            }
        }
        if (state2)
        {
            transform.Translate(new Vector2(0, -speed));
            timeToState3 -= Time.deltaTime;
            if (timeToState3 <= 0)
            {
                state2 = false;
                state3 = true;
            }
        }
        if (state3)
        {
            transform.Translate(new Vector2(0, speed * 2));
            timeToState4 -= Time.deltaTime;
            if (timeToState4 <= 0)
            {
                state3 = false;
                state4 = true;
            }
        }
        if (state4)
        {
            transform.Translate(new Vector2(0, -speed * 2));
            timeToDestroy -= Time.deltaTime;
            if (timeToDestroy <= 0)
            {
                state4 = false;
                Destroy(gameObject);
            }
        }
    }
}
