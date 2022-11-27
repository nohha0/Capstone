using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingMonster : Enemy
{
    [SerializeField] int    number;
    public int              jumpForce;
    Vector2                 distance;
    bool                    isGround;
    bool                    onWalk;
    float                   time = 0;
    override protected void Start()
    {
        base.Start();
        gameObject.SetActive(false);
        Invoke("OnSetActive", 1f);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        isGround = true;
        onWalk = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            rigid.velocity = Vector2.zero;
            isGround = true;
        }
    }

    override protected void UpdateTarget()
    {

        if(gameObject.activeSelf)
        {
            if (time <= 1f)
            {
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, time / 1);
            }
            time += Time.deltaTime;
        }



        //-----------------------------------
        distance = targetGameObject.transform.position - transform.position;

        if (distance.magnitude <= mag && isGround)
        {
            number = Random.Range(1, 11);

            if (number > 1) //걸어옴
            {
                //onWalk = true;
                //Invoke("OffWalk", 2);
                rigid.velocity = new Vector2(distance.normalized.x * speed, 0f);
            }
            else //점프함
            {
                rigid.velocity = Vector2.zero;
                rigid.AddForce(new Vector2(distance.normalized.x * speed * 5, jumpForce));
                isGround = false;
            }
        }
        else if(isGround)
        {
            rigid.velocity = Vector2.zero;
        }
        else { }


        if (rigid.velocity.x > 0)
            spriteRend.flipX = true;
        else if (rigid.velocity.x < 0)
            spriteRend.flipX = false;
        else { }

        if (rigid.velocity.magnitude == 0)
            animator.SetBool("walk", false);
        else
            animator.SetBool("walk", true);
    }

    public void OffWalk()
    {
        onWalk = false;
    }
    void OnSetActive()
    {
        gameObject.SetActive(true);
    }
}
