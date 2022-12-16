using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardFireWave : MonoBehaviour
{
    GameObject player;
    CharacterStats playerStat;
    SpriteRenderer rend;
    public float speed;
    bool Xcon;
    bool SetShoot = true;

    void Start()
    {
        player = GameObject.Find("Player");
        playerStat = player.GetComponent<CharacterStats>();
        rend = GetComponent<SpriteRenderer>();
        Invoke("OnDestroy", 0.5f);
    }

    void Update()
    {
        XFcon();
        if (Xcon)
        {
            rend.flipX = true;
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
        else if (!Xcon)
        {
            rend.flipX = false;
            transform.Translate(transform.right * -1 * speed * Time.deltaTime);
        }



    }
    private void OnDestroy()
    {
        Destroy(gameObject);
    }

    void XFcon()
    {
        if (SetShoot && player.GetComponent<SpriteRenderer>().flipX)
        {
            Xcon = true;
            SetShoot = false;
        }
        else if (SetShoot&& !player.GetComponent<SpriteRenderer>().flipX)
        {
            Xcon = false;
            SetShoot = false;
        }


    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponentInParent<Enemy>().TakeDamage(40 * playerStat.attackPower);
        }
    }


}
