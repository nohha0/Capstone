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

    public bool IsDown = false;
    bool Firstcoll = true;

    void Start()
    {
        firstTime = true;
        animator = wall.GetComponent<Animator>();
        Die = GameObject.Find("Player").GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Die.currentHP <= 0)
        {
            Firstcoll = true;
            animator.SetBool("DownUpdate", false);
            animator.SetBool("�ٴھ�", true);
        }

        if(BOSS == null)
        {
            animator.SetBool("DownUpdate", false);
            animator.SetBool("�ٴھ�", true);
        }

        if(IsDown)
        {
            animator.SetBool("�ٴھ�", false);
            animator.SetBool("DownUpdate", true);
        }



    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("�浹");
            if(Firstcoll)
            {
                Firstcoll = false;
                animator.SetTrigger("Down");
            }
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
