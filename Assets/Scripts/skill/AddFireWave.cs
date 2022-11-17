using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFireWave : MonoBehaviour
{
    //fire wave 스킬 컨트롤
    public GameObject firewave;
    public Transform pos;
    public float cooltime;
    private float curtime;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q) && curtime <= 0)
        {
            Instantiate(firewave, pos.position, transform.rotation);
            curtime = cooltime;
        }
        curtime -= Time.deltaTime;

    }
}
