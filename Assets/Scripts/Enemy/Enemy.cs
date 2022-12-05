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
    public Level GiveValue;        //����ġ ���

    bool One;
    virtual protected void Start()
    {
        One = true;
        attacked = false;
        targetGameObject = GameObject.FindWithTag("Player");
        rigid = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        GiveValue = GameObject.Find("Player").GetComponent<Level>();
    }

    virtual protected void Update()
    {
        if (HP <= 0) Invoke("DIE",0.5f);
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
        //Debug.Log("�÷��̾� ����!");
    }

    virtual protected void UpdateTarget()
    {
        //������ ~Monster �ڽ� Ŭ�������� ����
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
        if(One)
        {
            GiveValue.expCurrent += Enhance_value;
            Destroy(gameObject);
            One = false;
        }

    }


}
