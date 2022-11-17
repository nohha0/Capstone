using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PEN : MonoBehaviour
{
    void Start()
    {
        Invoke("Destroy", 2);
    }

    void Update()
    {
        //시간되면 머리위에서 플레이어한테 날아가는 벡터 만들어서 쏘는 패턴까지
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
