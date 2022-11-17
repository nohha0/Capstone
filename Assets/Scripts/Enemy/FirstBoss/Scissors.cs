using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissors : MonoBehaviour
{
    GameObject targetGameObject;
    Rigidbody2D rigid;

    float lookTime;

    void Start()
    {
        targetGameObject = GameObject.FindWithTag("Player");
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //lookTime += Time.deltaTime;
        //if (lookTime <= 5f)
        transform.LookAt(targetGameObject.transform);


    }
}
