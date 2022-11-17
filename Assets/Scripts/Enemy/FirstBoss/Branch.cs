using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Branch : MonoBehaviour
{
    public float speed;
    bool step1;
    bool step2;
    bool step3;

    void Start()
    {
        Invoke("Destroy", 3);
    }

    void Update()
    {
        transform.Translate(new Vector2(0, speed));
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
