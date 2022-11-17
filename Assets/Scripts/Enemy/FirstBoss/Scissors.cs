using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class Scissors : MonoBehaviour
{
    GameObject targetGameObject;
    Rigidbody2D rigid;
    Vector2 distance;
    public float throwSpeed;
    public float upForce;
    public int rotateForce;

    float lookTime;
    float rand;


    void Start()
    {
        targetGameObject = GameObject.FindWithTag("Player");
        rigid = GetComponent<Rigidbody2D>();

        distance = targetGameObject.transform.position - transform.position;
        rand = Random.Range(0.9f, 1.1f);

        rigid.AddForce(new Vector2(distance.x * (throwSpeed * rand), upForce));
        Invoke("DestroyScissors", 1);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotateForce));

        //distance = targetGameObject.transform.position - transform.position;
        //rigid.AddForce(new Vector2(distance.x * throwSpeed, upForce));
        //lookTime += Time.deltaTime;
        //if (lookTime <= 5f)
        //transform.LookAt(targetGameObject.transform);
    }

    void DestroyScissors()
    {
        Destroy(gameObject);
    }
}
