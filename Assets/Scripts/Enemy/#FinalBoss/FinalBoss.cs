using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : Enemy
{
    //noh-----------------------------------------------
    public GameObject WaterWave; //프리팹 가져옴
    public float height;

    GameObject Shooting;

    float timeUntilChangeState;
    bool onShooting;
    bool onWaterWave;
    bool onRest;
    bool callWaterWave;

    //shin-------------------------------------------
    [SerializeField] GameObject Big_Water;
    [SerializeField] GameObject Summon_Monster;
    [SerializeField] GameObject MagicCircle;
    [SerializeField] Transform Base;
    SpriteRenderer rend;
    Vector3 Pos;
    int a = 0;

    public Transform PlayerPos;

    bool Skill_SprayWater = false;
    bool Skill_Teleport = false;
    bool Skill_Summon = false;


    //-----------------------------------------


    override protected void Start()
    {
        //noh-----------------------------------------------
        base.Start(); //attacked(쿨타임), targetGameObject, rigid, spriteRend, animator 초기화
        Shooting = gameObject.transform.Find("ShootingWater").gameObject;
        Shooting.SetActive(false);
        timeUntilChangeState = 0f;
        onShooting = false;
        onWaterWave = false;
        callWaterWave = false;
        onRest = false;

        //shin-------------------------------------------
        rend = GetComponent<SpriteRenderer>();
        Pos = new Vector3(Base.position.x + 4, Base.position.y + 55, 1);

    }

    override protected void Update()
    {
        base.Update();

        if ((targetGameObject.transform.position - transform.position).magnitude <= mag)
        {
            ChangeState();

            if (onShooting) Shooting.SetActive(true);
            else Shooting.SetActive(false);
            if (onWaterWave && !callWaterWave) Wave();
            if (onRest) Rest();
            timeUntilChangeState -= Time.deltaTime;
            if (Skill_SprayWater) Water_Teleport();
            if (Skill_Teleport) Teleport();
            if (Skill_Summon) Summon();

        }
    }

    //noh----------------------------------------------------
    void Wave()
    {
        if (!onWaterWave) return;
        callWaterWave = true;

        Vector3 WavePosition = new Vector3(targetGameObject.transform.position.x, height, transform.position.z);
        Instantiate(WaterWave, WavePosition, transform.rotation);
        Invoke("Wave", 1);
    }

    void Rest()
    {

    }

    void ChangeState()
    {
        if (!onRest && timeUntilChangeState <= 0)
        {
            onRest = true;
            timeUntilChangeState = 1.5f;
            onWaterWave = false;
            callWaterWave = false;
            onShooting = false;
        }
        if (onRest && timeUntilChangeState <= 0)
        {
            onRest = false;
            int rand = Random.Range(0, 4);

            if (rand == 0)
            {
                onWaterWave = true;
                Debug.Log("onWaterWave");
                timeUntilChangeState = 5f;
            }
            else if (rand == 1)
            {
                onShooting = true;
                Debug.Log("onShooting");
                timeUntilChangeState = 5f;
            }
            else if (rand == 10) //2
            {
                //Skill_SprayWater = true;
                //timeUntilChangeState = 47;
            }
            else if(rand == 3)
            {
                Skill_Summon = true;
                timeUntilChangeState = 12f;
            }

        }
    }


    //shin-------------------------------------------
    void Set_BigWter()
    {
        if (a == 0)
        {
            Instantiate(Big_Water, Pos, transform.rotation);
            a++;
        }

    }

    void Water_Teleport()   //물폭탄 소환을 하기 위한 텔포
    {
        Skill_SprayWater = false;  //스킬 재 동작 금지
        gameObject.SetActive(false);
        transform.position = new Vector2(Base.position.x, Base.position.y);
        rend.flipX = false;
        Invoke("OnActive", 1f);
        Invoke("Set_BigWter", 1.5f);
    }

    void Summon()
    {
        gameObject.SetActive(false);
        Skill_Summon = false;
        Vector2 a = new Vector2(Base.position.x, Base.position.y - 10);
        Instantiate(MagicCircle, a, transform.rotation);
        Invoke("Summon_boss", 1f);
        Invoke("OnActive", 11f);
    }




    void Teleport()    //텔포
    {
        gameObject.SetActive(false);
        Skill_Teleport = false;
        float X = Random.Range(-50, 50);

        transform.position = new Vector2(PlayerPos.position.x + X, Base.position.y);
        Invoke("OnActive", 1.5f);
    }
    void OnActive()
    {
        gameObject.SetActive(true);
    }

    void Summon_boss()
    {
        Instantiate(Summon_Monster, Base.position, transform.rotation);
    }
}
