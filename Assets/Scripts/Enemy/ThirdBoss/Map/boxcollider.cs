using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxcollidet : MonoBehaviour
{
    Animator animator;
    bool Filst = false;

    //--------------------
    //public Vector2 boxSize;
    //public Transform Pos;
    void Start()
    {
        animator = GameObject.Find("��"). GetComponent<Animator>();
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if(!Filst)
            {
                animator.SetTrigger("Down");
                bool Filst = false;
                animator.SetBool("DownUpdate", true);
            }
        }

        //���� �÷��̾� ����� 0���� �ȴٸ�, �ִϸ��̼� ����
    }

}
