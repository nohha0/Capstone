using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] Transform Base;
    SpriteRenderer rend;
    Vector3 Pos;
    int a = 0;

    public Transform PlayerPos;

    bool Skill_SprayWater = false;
    bool Skill_Teleport = false;
    bool Skill_Summon = false;

    float summoncool = 60;


    //-----------------------------------------
    int curtell = 0;

    override protected void Start()
    {
        //noh-----------------------------------------------
        base.Start(); //attacked(쿨타임), targetGameObject, rigid, spriteRend, animator 초기화
        Shooting = gameObject.transform.Find("슈팅아래").gameObject;
        Shooting.SetActive(false);
        timeUntilChangeState = 0f;
        onShooting = false;
        onWaterWave = false;
        callWaterWave = false;
        onRest = false;

        //shin-------------------------------------------
        rend = GetComponent<SpriteRenderer>();
        Pos = new Vector3(Base.position.x + 4, Base.position.y + 55, 1);
        InvokeRepeating("OnActive", 5, 2);
        animator = GetComponent<Animator>();
    }

    override protected void Update()
    {
        base.Update();

        if ((targetGameObject.transform.position - transform.position).magnitude <= mag)
        {
            ChangeState();

            if (onShooting) Shooting.SetActive(true) ;
            else Shooting.SetActive(false);
            if (onWaterWave && !callWaterWave) Wave();
            if (onRest) Rest();
            timeUntilChangeState -= Time.deltaTime;
            if (Skill_SprayWater) Water_Teleport();
            if (Skill_Teleport) Teleport();
        }

    }

    //noh----------------------------------------------------
    void Wave()
    {
        if (!onWaterWave) return;
        callWaterWave = true;

        Vector3 WavePosition = new Vector3(targetGameObject.transform.position.x, Base.position.y + height, transform.position.z);
        Instantiate(WaterWave, WavePosition, transform.rotation);
        Invoke("Wave", 1);
    }

    void Rest()
    {
        //SaveManager.Instance._playerData.clearAllGame = true;
        //LoadOutroScene();
    }

    void ChangeState()
    {
        if (!onRest && timeUntilChangeState <= 0)
        {
            onRest = true;
            timeUntilChangeState = 3.5f;
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
                Teleport();
                onShooting = true;
                Debug.Log("onShooting");
                timeUntilChangeState = 5f;
            }
            else if (rand == 1)
            {
                Teleport();
                onShooting = true;
                Debug.Log("onShooting");
                timeUntilChangeState = 5f;
            }
            else if (rand == 2)
            {
                Debug.Log("스프레이");
                Skill_SprayWater = true;
                timeUntilChangeState = 23;
            }
            else if (rand == 3)
            {
                Teleport();
                onWaterWave = true;
                Debug.Log("onWaterWave");
                timeUntilChangeState = 4f;
            }


        }
    }


    //shin-------------------------------------------
    void Set_BigWter()
    {
        Skill_SprayWater = false;
        Instantiate(Big_Water, Pos, transform.rotation);

    }

    void Water_Teleport()   //물폭탄 소환을 하기 위한 텔포
    {
        Skill_SprayWater = false;  //스킬 재 동작 금지
        transform.position = new Vector2(Base.position.x, Base.position.y);
        rend.flipX = false;
        Invoke("Set_BigWter", 0.5f);
    }





    void Teleport()    //텔포
    {
        //gameObject.SetActive(false);
        //파란색 오브젝트 남기고 옴
        //플립 변경
        Skill_Teleport = false;
        float X = Random.Range(190, 190);
        if(X >40 && X> -40)
        {
            X = Random.Range(-70, 70);
        }
        transform.position = new Vector2(PlayerPos.position.x + X, Base.position.y);
        //Invoke("OnActive", 1.5f);


        if(curtell == 0 )
        {
            transform.position = new Vector2(PlayerPos.position.x + 200, Base.position.y);
        }
        if (curtell == 0)
        {
            transform.position = new Vector2(PlayerPos.position.x - 200, Base.position.y);
        }
    }
    void OnActive()
    {
        gameObject.SetActive(true);
    }


}
