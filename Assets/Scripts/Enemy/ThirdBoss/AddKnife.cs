using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddKnife : MonoBehaviour
{
    private ThirdMiddleBoss script;
    SpriteRenderer rend;  //보스에 스프라이트렌더
    SpriteRenderer Rend;
    public float speed = 130;
    void Start()
    {
        script = GameObject.Find("Player").GetComponent<ThirdMiddleBoss>();
        rend = GameObject.Find("ThirdBoss").GetComponent<SpriteRenderer>();
        Rend = GetComponent<SpriteRenderer>();
        Invoke("OnDestroy", 5);
        
        if (!rend.flipX) //왼쪽 보는 중
        {
            if (script.Knife_Pattern == 1)//정면
            {
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            if (script.Knife_Pattern == 2)//위로
            {
                transform.rotation = Quaternion.Euler(0, 0, 70);
            }
            if (script.Knife_Pattern == 3)//아래로
            {
                transform.rotation = Quaternion.Euler(0, 0, 115);
            }
        }
        if(rend.flipX) //오른쪽 보는중
        {
            Rend.flipX = true;
            if (script.Knife_Pattern == 1)//정면
            {
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            if (script.Knife_Pattern == 2)//위로
            {
                transform.rotation = Quaternion.Euler(0, 0, 120);
            }
            if (script.Knife_Pattern == 3)//아래로
            {
                transform.rotation = Quaternion.Euler(0, 0, 70);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.forward * speed * Time.deltaTime;
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}