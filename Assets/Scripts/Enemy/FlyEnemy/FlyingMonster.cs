using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FlyingMonster : Enemy
{
    public Transform pos;     //�ش� ���ʹ��� ��ġ��ǥ ���� �ڽ� Ŭ���������� ���
    public bool FlipOn = true;
    protected bool SetOn = false;

    float X, Y;
    override protected void Start()
    {
        base.Start();
        //InvokeRepeating("SetPos", 1, 1);
    }


    override protected void UpdateTarget()
    {
        Vector3 PlayerPos = targetGameObject.transform.position;
        float setflip = pos.position.x - PlayerPos.x;  // �¿� ����
        SetPos();
        if ((targetGameObject.transform.position - transform.position).magnitude <= mag)
        {
            SetOn = true;
            if (setflip > 0 && MoveOn)    //�÷��̾ ���ʿ� ��ġ�� ���
            {
                if(FlipOn) spriteRend.flipX = false;
                Vector3 enemypos = new Vector3(PlayerPos.x + X, PlayerPos.y + Y, this.transform.position.z); //�Ƿ��� ���� ��ġ
                Vector2 direction = (enemypos - transform.position).normalized;
                rigid.velocity = direction * speed;
            }
            else if (setflip < 0 && MoveOn)  //�÷��̾ �����ʿ� ��ġ�� ���
            {
                if (FlipOn) spriteRend.flipX = true;
                Vector3 enemypos = new Vector3(PlayerPos.x - X, PlayerPos.y + Y, this.transform.position.z); //�Ƿ��� ���� ��ġ
                Vector2 direction = (enemypos - transform.position).normalized;
                rigid.velocity = direction * speed;
            }
        }
        if(!MoveOn)
        {
            rigid.velocity = Vector2.zero;
        }


    }

   void SetPos()
    {
        X = Random.Range(29f, 40f);
        Y = Random.Range(22f, 30f);
    }
}