using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle : MonoBehaviour
{
    public bool obstacle1;
    public bool obstacle2;

    public bool up;
    public bool down;

    public float time;  //동작시간(편도)
    public float speed;

    float _TIME_; //동작시간 상수 저장해놓기
    bool stop;

    Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        _TIME_ = time;
        up = true;
        down = false;
        stop = true;
        Invoke("OffStop", time);
    }

    void Update()
    {
        Check();

        if (obstacle1)
        {
            time -= Time.deltaTime;
            //Work();
        }
        if (obstacle2)
        {
            if (stop) return;
            time -= Time.deltaTime;
            //Work();
        }
    }

    void Work()
    {
        if (up)
        {
            //rigid.velocity = Vector2.zero;
            rigid.velocity = new Vector2(0f, speed);
        }
        if (down)
        {
            //rigid.velocity = Vector2.zero;
            rigid.velocity = new Vector2(0f, -speed);
        }
    }

    void Check()
    {
        if (time <= 0f)
        {
            time = _TIME_;
            rigid.velocity = Vector2.zero;

            if (up)
            {
                up = false;
                down = true;
                rigid.velocity = new Vector2(0f, speed);
                //time = _TIME_;
            }
            else if (down)
            {
                down = false;
                up = true;
                rigid.velocity = new Vector2(0f, -speed);
                //time = _TIME_;
            }
        }
    }

    void OffStop()
    {
        stop = false;
    }
}
