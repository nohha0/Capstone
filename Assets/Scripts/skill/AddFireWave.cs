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

    public AudioSource audioSource;
    public AudioClip fire;

    void Start()
    {
        playerStat = GameObject.Find("Player").GetComponent<CharacterStats>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A) && curtime <= 0 && SaveManager.Instance._playerData.killedBoss2)
        {
            Instantiate(firewave, pos.position, transform.rotation);
            curtime = 6;

            audioSource.PlayOneShot(fire);
        }
        curtime -= Time.deltaTime;

    }
}
