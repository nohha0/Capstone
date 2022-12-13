using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
//using static UnityEditor.PlayerSettings;

public class Enemy : MonoBehaviour
{
    public float        speed;
    public float        HP;
    public bool         attacked;
    public float        mag;
    public float        Enhance_value;

    public bool middleBoss1;
    public bool middleBoss2;
    public bool middleBoss3;
    public bool FinalBoss;

    public ParticleSystem ps;
    protected GameObject        targetGameObject;
    protected Rigidbody2D       rigid;
    protected SpriteRenderer    spriteRend;
    protected Animator          animator;
    protected bool Istargeting = false;
    public Level GiveValue;        //����ġ ���
    protected data DiecurStage;

    protected bool MoveOn = true;

    public int DieStage;  //�� �������������� �״´�

    protected bool One;
    public PlayerController player;



    public bool BossPlay = false;
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
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    virtual protected void Update()
    {
        if (HP <= 0)
        {
            transform.GetChild(0).tag = "Untagged";

            rigid.gravityScale = 300f;
            animator.SetTrigger("����");
            spriteRend.color = new Color(0.8f, 0.8f, 0.8f);
            Invoke("DIE", 1.7f);
            if (One)
            {
                GiveValue.expCurrent += Enhance_value;
                One = false;
            }
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
            MoveOn = false;
            if (HP > 0)
            {
                Invoke("SpriteOn", 0.1f);
                animator.SetTrigger("�ǰ�");
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
        
        if (middleBoss1)
        {
            SaveManager.Instance._playerData.killedBoss1 = true;
        }
        else if (middleBoss2)
        {
            SaveManager.Instance._playerData.killedBoss2 = true;
        }
        else if (middleBoss3)
        {
            SaveManager.Instance._playerData.killedBoss3 = true;
        }
        else if (FinalBoss)
        {
            SaveManager.Instance._playerData.clearAllGame = true;
            Invoke("LoadOutroScene", 2f);
        }

        Destroy(gameObject);

    }

    void LoadOutroScene()
    {
        SceneManager.LoadScene("Outro");
    }

    void IsMove()
    {
        MoveOn = true;
    }


}
