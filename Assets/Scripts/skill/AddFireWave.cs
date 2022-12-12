using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFireWave : MonoBehaviour
{
    //fire wave 스킬 컨트롤
    public GameObject firewave;
    public Transform pos;
    private float curtime;

    CharacterStats playerStat;

    void Start()
    {
        playerStat = GameObject.Find("Player").GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) && curtime <= 0)
        {
            Instantiate(firewave, pos.position, transform.rotation);
            curtime = playerStat.attackSpeed;
        }
        curtime -= Time.deltaTime;

    }
}
