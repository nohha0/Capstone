using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class Scissors : MonoBehaviour
{
    GameObject targetGameObject;
    SpriteRenderer gameObjSpr;
    Rigidbody2D rigid;
    Vector2 distance;
    public float throwSpeed;
    //public float upForce;
    public int rotateForce;

    float rand;


    void Start()
    {
        targetGameObject = GameObject.FindWithTag("Player");
        gameObjSpr = GameObject.Find("boss1").GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

        distance = targetGameObject.transform.position - transform.position;
        rand = Random.Range(0.9f, 1.1f);

        if(gameObjSpr.flipX) rigid.velocity = new Vector2 (throwSpeed, 0f);
        else rigid.velocity = new Vector2(-throwSpeed, 0f);
        //rigid.AddForce(new Vector2(distance.x * (throwSpeed * rand), upForce));
        Invoke("DestroyScissors", 2);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotateForce));
    }

    void DestroyScissors()
    {
        Destroy(gameObject);
    }
}
