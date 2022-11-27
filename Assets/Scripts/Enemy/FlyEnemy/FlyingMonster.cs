using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FlyingMonster : Enemy
{
    public Transform pos;     //�ش� ���ʹ��� ��ġ��ǥ ���� �ڽ� Ŭ���������� ���

    protected bool SetOn = false;
    float cool =  2;

    override protected void Start()
    {
        base.Start();
        InvokeRepeating("SETON", 0f, 0.15f);
    }

    override protected void Update()
    {
        base.Update();
    }

    override protected void UpdateTarget()
    {
        Vector3 PlayerPos = targetGameObject.transform.position;
        float setflip = pos.position.x - PlayerPos.x;  // �¿� ����

        if ((targetGameObject.transform.position - transform.position).magnitude <= mag)
        {
            SetOn = true;
            if (setflip > 0)    //�÷��̾ ���ʿ� ��ġ�� ���
            {
                spriteRend.flipX = false;
                Vector3 enemypos = new Vector3(PlayerPos.x + 35f, PlayerPos.y + 25, this.transform.position.z); //�Ƿ��� ���� ��ġ
                Vector2 direction = (enemypos - transform.position).normalized;
                rigid.velocity = direction * speed;
            }
            else if (setflip < 0)  //�÷��̾ �����ʿ� ��ġ�� ���
            {
                spriteRend.flipX = true;
                Vector3 enemypos = new Vector3(PlayerPos.x - 35f, PlayerPos.y + 25, this.transform.position.z); //�Ƿ��� ���� ��ġ
                Vector2 direction = (enemypos - transform.position).normalized;
                rigid.velocity = direction * speed;
            }
        }


    }

    public void SETON()
    {
        SetOn = false;
    }

   
}