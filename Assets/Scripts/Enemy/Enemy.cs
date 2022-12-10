using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class Enemy : MonoBehaviour
{
    public float        speed;
    public float        HP;
    public bool         attacked;
    public float        mag;
    public float        Enhance_value;
    public ParticleSystem ps;


    protected GameObject        targetGameObject;
    protected Rigidbody2D       rigid;
    protected SpriteRenderer    spriteRend;
    protected Animator          animator;
    protected bool Istargeting = false;
    public Level GiveValue;        //경험치 드랍
    protected data DiecurStage;

    protected bool MoveOn = true;

    public int DieStage;  //외 스테이지에서는 죽는다

    bool One;
    virtual protected void Start()
    {
        One = true;
        attacked = false;
        targetGameObject = GameObject.FindWithTag("Player");
        rigid = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        DiecurStage = GameObject.Find("Main Camera").GetComponent<data>();

        GiveValue = GameObject.Find("Player").GetComponent<Level>();
    }

    virtual protected void Update()
    {
        if (HP <= 0)
        {
            rigid.gravityScale = 300f;
            animator.SetTrigger("다이");
            spriteRend.color = new Color(0.8f, 0.8f, 0.8f);
            Invoke("DIE", 1.7f);
        }
        UpdateTarget();

    }

    virtual protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Attack();
        }
    }

    public void Attack()
    {
        //Debug.Log("플레이어 공격!");
    }

    virtual protected void UpdateTarget()
    {
        //내용은 ~Monster 자식 클래스에서 구현
    }

    virtual public void TakeDamage(float damage)
    {
        if (!attacked)
        {
            spriteRend.color = new Color(0.5f, 0.5f, 0.5f);
            HP -= damage;
            attacked = true;
            Invoke("attackedOn", 1f);
            Invoke("SpriteOn", 0.1f);
            MoveOn = false;
            if (HP > 0)
            {
                animator.SetTrigger("피격");
                Invoke("IsMove", 0.5f);
            }
        }
    }

    public void attackedOn()
    {
        attacked = false;
    }

    public void SpriteOn()
    {
        spriteRend.color = new Color(1, 1, 1);
    }

    virtual public void DIE()
    {
        //ps.Play();
        speed = 0;
        if (One)
        {
            GiveValue.expCurrent += Enhance_value;
            Destroy(gameObject);
            One = false;
        }

    }

    void IsMove()
    {
        MoveOn = true;
    }


}
