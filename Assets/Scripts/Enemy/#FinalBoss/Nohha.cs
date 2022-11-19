using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nohha : Enemy
{
    public GameObject WaterWave; //프리팹 가져옴
    public float height;
 
    GameObject Shooting;

    float timeUntilChangeState;
    bool onShooting;
    bool onWaterWave;
    bool onJumping;
    bool callWaterWave;
    bool callJumping;


    protected override void Start()
    {
        base.Start(); //attacked(쿨타임), targetGameObject, rigid, spriteRend, animator 초기화
        Shooting = gameObject.transform.Find("ShootingWater").gameObject;
        Shooting.SetActive(false);
        timeUntilChangeState = 0f;
        onShooting = false;
        onWaterWave = false;
        callWaterWave = false;
        onJumping = false;
        callJumping = false;
    }

    protected override void Update()
    {
        base.Update();
        ChangeState();

        if (onShooting) Shooting.SetActive(true);
        else Shooting.SetActive(false);

        if (onWaterWave && !callWaterWave) Wave();
        if (onJumping) Jumping();
        timeUntilChangeState -= Time.deltaTime;

    }

    void Jumping()
    {
        //최종보스는 점프가 아니라 대쉬or텔레포트 였나...?
        if (!onJumping) return;
        callJumping = true;

        Vector2 JumpPosition = targetGameObject.transform.position;
        rigid.AddForce(JumpPosition);
    }

    void Wave()
    {
        if (!onWaterWave) return;
        callWaterWave = true;

        Vector2 WavePosition = new Vector2(targetGameObject.transform.position.x, height);
        Instantiate(WaterWave, WavePosition, transform.rotation);
        Invoke("Wave", 1);
    }

    void ChangeState()
    {
        if (!onJumping && timeUntilChangeState <= 0)
        {
            onJumping = true;
            timeUntilChangeState = 5f;
            onWaterWave = false;
            callWaterWave = false;
            onShooting = false;
        }
        if (onJumping && timeUntilChangeState <= 0)
        {
            onJumping = false;
            timeUntilChangeState = 10f;
            int rand = Random.Range(0, 2);

            if (rand == 0)
            {
                onWaterWave = true;
            }
            else
            {
                onShooting = true;
            }
        }
    }
}
