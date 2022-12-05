using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAni : MonoBehaviour
{
    public bool Dest = false;
    void Start()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + 25);
    }

    // Update is called once per frame
    void Update()
    {
        if(Dest)
        {
            Destroy(gameObject);
        }
    }
}
