using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownWalls : MonoBehaviour
{
    public GameObject wall;
    public GameObject BOSS;
    bool firstTime;
    Animator animator;
    CharacterStats Die;

    void Start()
    {
        firstTime = true;
        animator = wall.GetComponent<Animator>();
        Die = GameObject.Find("Player").GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Die.currentHP <= 0) animator.SetBool("DownUpdate", false);

        if(BOSS == null)
        {
            animator.SetBool("DownUpdate", false);
            animator.SetBool("¹Ù´Ú¾÷", true);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Ãæµ¹");
            animator.SetTrigger("Down");
            animator.SetBool("¹Ù´Ú¾÷", false);
            animator.SetBool("DownUpdate", true);
            if(firstTime)
            {
                firstTime = false;
                //Invoke("SetOnBoss", 5f);
            }
        }
    }

    void SetOnBoss()
    {
        BOSS.SetActive(true);
    }
}
