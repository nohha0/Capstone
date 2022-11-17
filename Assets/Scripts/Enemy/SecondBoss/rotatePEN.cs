using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rotatePEN : MonoBehaviour
{
    public Vector3 rotation;
    public SpriteRenderer penSprite;

    bool rotatingOn;

    void Awake()
    {
        penSprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        gameObject.tag = "Untagged"; //1초동안 플레이어에게 위치를 예고하는 용도
        penSprite.color = new Color(0.3f,0.3f,0.3f);
        rotatingOn = false;
        Invoke("Destroy", 8);
        Invoke("OnTag", 2);
    }

    void Update()
    {
        if(rotatingOn)
          transform.Rotate(rotation * Time.deltaTime);
    }

    void OnTag()
    {
        rotatingOn = true;
        gameObject.tag = "Enemy";
        penSprite.color = new Color(1, 1, 1);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
