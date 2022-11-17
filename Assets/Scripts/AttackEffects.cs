using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//공격 이펙트 
public class AttackEffects : MonoBehaviour
{
    //어택 스크립트
    private Attack script;
    GameObject player;  //플레이어 정보 접근
    void Start()
    {
        Invoke("OnDestroy", 0.4f);
        script = GameObject.Find("Player").GetComponent<Attack>();  //스크립트 접근
        player = GameObject.Find("Player");   //플레이어 접근
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("create", 0.1f);
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }
    void create()
    {
        transform.localScale = new Vector3(script.boxSize.x, script.boxSize.y, 1);

    }

}
