using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActive : MonoBehaviour
{
    public GameObject Boss;
    CameraShake Shake;
    bool First = true;
    void Start()
    {
        Shake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Boss.GetComponent<Enemy>().BossPlay);
    }

    virtual protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player"&& First)
        {
            First = false;
            GameObject.Find("Player").GetComponent<PlayerController>().movable = false;
            Shake.voidShake(3f);

            Invoke("Play", 3f);
            //1초뒤 포효하는 사운드

        }
    }

    void Play()
    {
        Boss.GetComponent<Enemy>().BossPlay = true;
        GameObject.Find("Player").GetComponent<PlayerController>().movable = true;
    }
}
