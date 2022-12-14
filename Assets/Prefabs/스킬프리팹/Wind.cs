using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    Rigidbody2D riged;
    SpriteRenderer rend;    //바람 좌우반전
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

        if(elapseTime <= timeToBack) //정방향 
        {
            rend.flipX = false;
            transform.Translate(new Vector2(-speed * Time.deltaTime, 0f));
        }
        else if (elapseTime <= timeToDestroy) //역방향
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
