using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    Rigidbody2D riged;
    SpriteRenderer rend;    //�ٶ� �¿����
    public float timeToBack;
    public float timeToDestroy;
    public float elapseTime;
    public float speed;


    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        riged = GetComponent<Rigidbody2D>();
        elapseTime = 0;
    }

    void Update()
    {
        elapseTime += Time.deltaTime;

        if(elapseTime <= timeToBack) //������ 
        {
            rend.flipX = false;
            transform.Translate(new Vector2(-speed * Time.deltaTime, 0f));
        }
        else if (elapseTime <= timeToDestroy) //������
        {
            rend.flipX = true;
            transform.Translate(new Vector2(speed * 2 * Time.deltaTime, 0f));
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
