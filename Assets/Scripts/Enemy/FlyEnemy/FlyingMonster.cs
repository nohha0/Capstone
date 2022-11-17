using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FlyingMonster : Enemy
{
    public Transform pos;     //해당 에너미의 위치좌표 저장 자식 클래스에서도 사용

    protected bool SetOn = false;
    
    override protected void Start()
    {
        base.Start();
        //InvokeRepeating("UpdateTarget", 0f, 0.25f); //0초 후에 0.25초마다 함수 실행
        InvokeRepeating("SETON", 0f, 0.15f);
    }

    override protected void Update()
    {
        base.Update();
    }

    override protected void UpdateTarget()
    {
        Vector3 PlayerPos = targetGameObject.transform.position;
        float setflip = pos.position.x - PlayerPos.x;  // 좌우 구별

        if ((targetGameObject.transform.position - transform.position).magnitude <= mag)
        {
            SetOn = true;
            if (setflip > 0)    //플레이어가 왼쪽에 위치할 경우
            {
                spriteRend.flipX = false;
                Vector3 enemypos = new Vector3(PlayerPos.x + 35f, PlayerPos.y + 25, this.transform.position.z); //악령이 있을 위치
                Vector2 direction = (enemypos - transform.position).normalized;
                rigid.velocity = direction * speed;
            }
            else if (setflip < 0)  //플레이어가 오른쪽에 위치할 경우
            {
                spriteRend.flipX = true;
                Vector3 enemypos = new Vector3(PlayerPos.x - 35f, PlayerPos.y + 25, this.transform.position.z); //악령이 있을 위치
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