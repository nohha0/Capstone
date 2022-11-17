using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWindy : MonoBehaviour
{
    public GameObject Windywave;
    SpriteRenderer rend;
    public float delray;
    float curtime;
    Vector3 setpos;
    int i = 0;

    void Start()
    {
        curtime = delray;
        rend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //test
        Setpos();
    }

    void Setpos()
    {
        if(i<7)
        {
            if (!rend.flipX)  //캐릭터가 오른쪽에 있을경우  
            {
                if (curtime <= 0 && i < 3)
                {
                    Vector3 setpos = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);
                    Instantiate(Windywave, setpos, transform.rotation);
                    curtime = delray;
                    i++;

                }
                if (i == 3 && curtime <= 0)
                {
                    curtime = 0.6f;
                    i++;
                }
                if (i > 3 && curtime <= 0)
                {
                    setpos = new Vector3(transform.position.x + 10, transform.position.y + 45, transform.position.z);
                    Instantiate(Windywave, setpos, transform.rotation);
                    curtime = delray;
                    i++;
                }


            }
            curtime -= Time.deltaTime;
        }
    }

}
