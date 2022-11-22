using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addWindy : MonoBehaviour
{
    GameObject secondboss;  //보스 반전 확인
    Rigidbody2D riged;
    SpriteRenderer rend;    //바람 좌우반전
    private data script;

    bool Xcon;              //x좌표 컨트롤
    bool SetShoot = true;   //방향 셋팅
    float backmove = 0;
    public float speed;
    public float fastspeed;


    void Start()
    {
        secondboss = GameObject.Find("SecondBoss");
        rend = GetComponent<SpriteRenderer>();
        riged = GetComponent<Rigidbody2D>();
        Invoke("OnDestroy", 10);
        script = GameObject.Find("Main Camera").GetComponent<data>();            //스크립트 접근

        fastspeed += speed;

        Debug.Log(script.Lcount);

    }


    void Update()
    {
        Shoot();

        if (script.Lcount == 6)
        {
            Invoke("Backright", 2f);
            Invoke("set", 2);
        }
        if (script.Rcount == 6)
        {
            Invoke("Backleft", 2f);
            Invoke("set", 2);
        }

    }

    void XFcon()
    {
        if (SetShoot && secondboss.GetComponent<SpriteRenderer>().flipX)
        {
            Xcon = true;
            SetShoot = false;
        }
        else if (SetShoot && !secondboss.GetComponent<SpriteRenderer>().flipX)
        {
            Xcon = false;
            SetShoot = false;
        }

    }
    void Shoot()
    {
        backmove += Time.deltaTime;
        XFcon();
        if (Xcon)
        {
            if (backmove <= 1.2f)
            {
                rend.flipX = true;
                transform.Translate(transform.right * speed * Time.deltaTime);
            }
            else
            {
                rend.flipX = false;
                riged.velocity = Vector2.zero;   //동작 정지
            }

        }
        else if (!Xcon)
        {
            if (backmove <= 1.2f)
            {
                rend.flipX = false;
                transform.Translate(transform.right * -1 * speed * Time.deltaTime);
            }
            else
            {
                rend.flipX = true;
                riged.velocity = Vector2.zero;   //동작 정지
            }
        }
    }

    public void Backright()     //오른쪽으로 돌아감
    {
        transform.Translate(transform.right * fastspeed * Time.deltaTime);
    }
    public void Backleft()     //왼쪽으로 돌아감
    {
        transform.Translate(transform.right * -1 * fastspeed * Time.deltaTime);
    }

    private void set()
    {
        script.Rcount = 0;
        script.Lcount = 0;
    }


    private void OnDestroy()
    {
        Destroy(gameObject);
    }

}
