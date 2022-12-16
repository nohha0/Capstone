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
    public Level GiveValue;        //경험치 드랍
    protected data DiecurStage;

    protected bool MoveOn = true;

    public int DieStage;  //외 스테이지에서는 죽는다

    protected bool One;
    public PlayerController player;
    GameManager gameManager;

    public bool BossPlay = false;
    public bool ImBoss;
    public ParticleSystem Die;

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

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    virtual protected void Update()
    {
        if (HP <= 0)
        {
            transform.GetChild(0).tag = "Untagged";

            rigid.gravityScale = 300f;
            if(!middleBoss1&&!middleBoss2&&!middleBoss3&&!FinalBoss) animator.SetTrigger("다이");
            spriteRend.color = new Color(0.8f, 0.8f, 0.8f);
            Invoke("DIE", 2f);
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
            Invoke("attackedOn", 0.45f);
            MoveOn = false;
            if (HP > 0)
            {
                Invoke("SpriteOn", 0.1f);
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
            One = false;
        }

        if (middleBoss1)
        {
            SaveManager.Instance._playerData.killedBoss1 = true;
            gameManager.skill_1.SetActive(true);
        }
        else if (middleBoss2)
        {
            SaveManager.Instance._playerData.killedBoss2 = true;
            gameManager.skill_2.SetActive(true);
        }
        else if (middleBoss3)
        {
            SaveManager.Instance._playerData.killedBoss3 = true;
            gameManager.skill_3.SetActive(true);
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
