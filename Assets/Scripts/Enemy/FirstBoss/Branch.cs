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
    public float timeToState2;
    public float timeToState3;
    public float timeToState4;
    public float timeToDestroy;
    Rigidbody2D rb;

    //  0.5up/0.5down/1up/1down

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        state1 = true;
        //timeToState2 = 0.5f;
        //timeToState3 = 0.5f;
        //timeToState4 = 1f;
        //timeToDestroy = 1f;
    }

    void Update()
    {
        if (state1)
        {
            rb.velocity = new Vector2(0, speed);
            //transform.Translate(new Vector2(0, speed));
            timeToState2 -= Time.deltaTime;
            if (timeToState2 <= 0)
            {
                state1 = false;
                state2 = true;
            }
        }
        if (state2)
        {
            rb.velocity = new Vector2(0, -speed);
            //transform.Translate(new Vector2(0, -speed));
            timeToState3 -= Time.deltaTime;
            if (timeToState3 <= 0)
            {
                state2 = false;
                state3 = true;
            }
        }
        if (state3)
        {
            rb.velocity = new Vector2(0, speed * 2);
            //transform.Translate(new Vector2(0, speed * 2));
            timeToState4 -= Time.deltaTime;
            if (timeToState4 <= 0)
            {
                state3 = false;
                state4 = true;
            }
        }
        if (state4)
        {
            rb.velocity = new Vector2(0, -speed * 2);
            //transform.Translate(new Vector2(0, -speed * 2));
            timeToDestroy -= Time.deltaTime;
            if (timeToDestroy <= 0)
            {
                state4 = false;
                Destroy(gameObject);
            }
        }
    }
}
