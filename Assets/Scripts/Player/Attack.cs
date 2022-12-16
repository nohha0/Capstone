using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : CharacterStats
{
    Animator animator;
    float curTime;
    bool fireRangeOn = false;
    public GameObject rangeObject;
    GameObject range_object;

    public GameObject AttackBox;  //���� ����Ʈ, ����Ʈ ��������Ʈ�� ��������

    //�ݶ��̴� ��ġ 
    public Transform Rpos;
    public Transform Lpos;
    public Vector2 boxSize;
    SpriteRenderer rend;  //�÷��̾� ��������Ʈ


    //������ �¿���� ����
    public bool AttackLeftOn = true;
    public bool AttackRightOn = true;

    CharacterStats playerStat;
    PlayerController Move;

    public AudioSource audioSource;
    public AudioClip Knife;
    public AudioClip fireRange;



    void Start()
    {
        playerStat = GameObject.Find("Player").GetComponent<CharacterStats>();
        animator = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        Move = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Move.movable&&Input.GetKeyDown(KeyCode.Z) && curTime <=0) // ���ݹ�ư�� �����ٸ�
        {
            audioSource.PlayOneShot(Knife);

            if (rend.flipX)  //������ �ü�
            {
                //�����Ҷ����� attackSpeed��ŭ �¿���ȯ ����
                animator.SetTrigger("attack");
                AttackLeftOn = false;  //���� �ü� ����
                Invoke("NotMove", 0.4f);  //0.4�ʵ� ����
                Vector2 newBoxSize = new Vector2(boxSize.x * playerStat.attackRange, boxSize.y);
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(Rpos.position, newBoxSize, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                    //���� ����Ʈ ��ȯ
                    //Instantiate(AttackBox, Rpos.position, transform.rotation);
                    if (collider.tag == "Enemy")
                    {
                        collider.GetComponentInParent<Enemy>().TakeDamage(30 * playerStat.attackPower);
                        curTime = playerStat.attackSpeed;
                    }
                }
            }
            else if(!rend.flipX)
            {
                animator.SetTrigger("attack");
                AttackRightOn = false;  //���� �ü� ����
                Invoke("NotMove", 0.4f);  //0.4�ʵ� ����
                Vector2 newBoxSize = new Vector2(boxSize.x * playerStat.attackRange, boxSize.y);
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(Lpos.position, newBoxSize, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                    //���� ����Ʈ ��ȯ
                    //Instantiate(AttackBox, Lpos.position, transform.rotation);
                    if (collider.tag == "Enemy")
                    {
                        collider.GetComponentInParent<Enemy>().TakeDamage(30 * playerStat.attackPower);
                        curTime = playerStat.attackSpeed;
                    }
                }
            }

            curTime = playerStat.attackSpeed;
        }
        else //������ �ʴ´ٸ� �ð� ����
        {
            curTime -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.S) && !fireRangeOn && SaveManager.Instance._playerData.killedBoss3)
        {
            audioSource.PlayOneShot(fireRange);
            fireRangeOn = true;
            Vector2 playerPos = new Vector2(transform.position.x, transform.position.y+7);
            range_object = Instantiate(rangeObject, playerPos, transform.rotation);
            Invoke("fireRangeOff", 12);
        }
    }

    
    //--------------------------------------------------------------------------

    // ��ȯ���� �޼ҵ�
    void NotMove()
    {
        AttackLeftOn = true;
        AttackRightOn = true;
    }


    public void fireRangeOff()
    {
        fireRangeOn = false;
        Destroy(range_object);
    }
}
