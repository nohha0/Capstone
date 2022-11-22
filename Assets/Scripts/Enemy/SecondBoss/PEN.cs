using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PEN : MonoBehaviour
{
    GameObject bossObj;

    void Start()
    {
        bossObj = GameObject.Find("SecondBoss");
        Invoke("Destroy", 2);
    }

    void Update()
    {
        transform.position = new Vector2(bossObj.transform.position.x, bossObj.transform.position.y + 30f);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
