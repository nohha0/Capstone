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
    float                   jumptime;
    bool                    NotRun = false;

    override protected void Start()
    {
        base.Start();
        //gameObject.SetActive(false);
        //Invoke("OnSetActive", 1f);
        //GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        isGround = true;
        onWalk = false;
        DieStage = 3;
        jumptime = 2f;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform") && other.contacts[0].normal.y >= 0.8f)
        {
            isGround = true;
        }
    }

    override protected void UpdateTarget()
    {
        distance = targetGameObject.transform.position - transform.position;

        if (distance.magnitude <= mag && isGround && !onWalk)
        {
            if(!NotRun&&MoveOn)
            {
                Vector2 direction = (targetGameObject.transform.position - transform.position).normalized;
                rigid.velocity = new Vector2(direction.x * speed, 0f);
            }
            else if(NotRun&&MoveOn)
            {
                rigid.velocity = Vector2.zero;
                rigid.AddForce(new Vector2(distance.normalized.x * speed * 5, jumpForce));
                NotRun = false;
                isGround = false;
            }
            jumptime -= Time.deltaTime;
        }

        if (jumptime <= 0)
        {
            NotRun = true;
            jumptime = 2f;
        }
        if (!MoveOn)
        {
            rigid.velocity = Vector2.zero;
        }


        if (rigid.velocity.x > 0)
            spriteRend.flipX = true;
        else if (rigid.velocity.x < 0)
            spriteRend.flipX = false;

        if (rigid.velocity.magnitude == 0)
            animator.SetBool("walk", false);
        else
            animator.SetBool("walk", true);


        if (DiecurStage.Stage != DieStage)
        {
            Destroy(gameObject);
        }

        if (HP <= 0)
        {
            transform.GetChild(0).tag = "Untagged";

            rigid.gravityScale = 300f;
            animator.SetTrigger("????");
            spriteRend.color = new Color(0.8f, 0.8f, 0.8f);
            Invoke("DIE", 2f);
        }
    }

    public void OffWalk()
    {
        onWalk = false;
    }

    void OnSetActive()
    {
        gameObject.SetActive(true);
    }

    public override void DIE()
    {
        //animator.SetTrigger("????");
        base.DIE();
        rigid.gravityScale = 100f;
    }
}
