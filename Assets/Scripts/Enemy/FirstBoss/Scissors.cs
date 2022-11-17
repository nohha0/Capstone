using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scissors : MonoBehaviour
{
    GameObject targetGameObject;
    Rigidbody2D rigid;
    Vector2 distance;
    public float throwSpeed;
    public float upForce;

    float lookTime;

    void Start()
    {
        targetGameObject = GameObject.FindWithTag("Player");
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        distance = targetGameObject.transform.position - transform.position;
        rigid.AddForce(new Vector2(distance.x * throwSpeed, upForce));
        //lookTime += Time.deltaTime;
        //if (lookTime <= 5f)
        //transform.LookAt(targetGameObject.transform);
    }
}
