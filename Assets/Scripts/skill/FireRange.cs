using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRange : MonoBehaviour
{
    CharacterStats playerStat;

    private void Start()
    {
        playerStat = GameObject.Find("Player").GetComponent<CharacterStats>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponentInParent<Enemy>().TakeDamage(100 * playerStat.attackPower);
            //Debug.Log("fireRange");
        }
    }
}
