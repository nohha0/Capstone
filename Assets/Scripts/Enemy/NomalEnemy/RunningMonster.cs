using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningMonster : Enemy
{
    //float time = 0;

    public bool Destroy;
    override protected void Start()
    {
        base.Start();
        Destroy = false;
        //gameObject.SetActive(false);
        //GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        //Invoke("OnSetActive", 1f);
        DieStage = 2;
    }
    override protected void UpdateTarget()
    {
        

        //-----------------------------------------------

        if ((targetGameObject.transform.position - transform.position).magnitude <= mag)
        {
            Istargeting = true;
        }
        else if((targetGameObject.transform.position - transform.position).magnitude <= mag+40)
        {
            Istargeting = false;
            rigid.velocity = Vector2.zero;
        }

        if(MoveOn)
        {
            if (Istargeting)
            {
                Vector2 direction = (targetGameObject.transform.position - transform.position).normalized;
                rigid.velocity = new Vector2(direction.x * speed, 0f);
            }
        }
        if(!MoveOn)
        {
            rigid.velocity = Vector2.zero;
        }

        if (rigid.velocity.x > 0) spriteRend.flipX = true;
        else if (rigid.velocity.x < 0) spriteRend.flipX = false;
        else { }

        if (rigid.velocity.magnitude == 0) animator.SetBool("walk", false);
        else animator.SetBool("walk", true);

        if (DiecurStage.Stage != DieStage)
        {
            Destroy(gameObject);
        }

    }

}
