using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallBranch : MonoBehaviour
{
    public float speed;
    public bool moveable;
    public float destroyTime;
    public float offMoveTime;

    void Start()
    {
        moveable = true;
        Invoke("Delete", destroyTime);
        Invoke("OffMove", offMoveTime);
    }
     
    void Update()
    {
        if(moveable) transform.Translate(new Vector2(0, speed * Time.deltaTime));        
    }

    void Delete()
    {
        Destroy(gameObject);
    }

    void OffMove()
    {
        moveable = false;
    }
}
