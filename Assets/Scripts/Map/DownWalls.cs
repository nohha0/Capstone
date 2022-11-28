using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownWalls : MonoBehaviour
{
    Animator animator;
    CharacterStats Die;
    void Start()
    {
        animator = GameObject.Find("º®").GetComponent<Animator>();
        Die = GameObject.Find("Player").GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Die.currentHP <= 0) animator.SetBool("DownUpdate", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Ãæµ¹");
            animator.SetTrigger("Down");
            animator.SetBool("DownUpdate", true);

        }
    }
}
