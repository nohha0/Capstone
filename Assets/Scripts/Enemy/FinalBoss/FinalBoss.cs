using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalBoss : Enemy
{
    Animator animator;
    //noh-----------------------------------------------
    public GameObject WaterWave; //������ ������
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
    //


    //-----------------------------------------


    override protected void Start()
    {
        //noh-----------------------------------------------
        base.Start(); //attacked(��Ÿ��), targetGameObject, rigid, spriteRend, animator �ʱ�ȭ
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
        InvokeRepeating("OnActive", 5, 2);

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
                //onWaterWave = true;
                onShooting = true;
                Debug.Log("onShooting");
                timeUntilChangeState = 5f;
            }
            else if (rand == 2)
            {
                Skill_SprayWater = true;
                timeUntilChangeState = 30;
            }
            else if (rand == 3)
            {
                //onWaterWave = true;
                onShooting = true;
                Debug.Log("onShooting");
                timeUntilChangeState = 5f;
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

    void Water_Teleport()   //����ź ��ȯ�� �ϱ� ���� ����
    {
        Skill_SprayWater = false;  //��ų �� ���� ����
        gameObject.SetActive(false);
        transform.position = new Vector2(Base.position.x, Base.position.y);
        rend.flipX = false;
        Invoke("OnActive", 1f);
        Invoke("Set_BigWter", 1.5f);
    }





    void Teleport()    //����
    {
        //gameObject.SetActive(false);
        //�Ķ��� ������Ʈ ����� ��
        //�ø� ����
        Skill_Teleport = false;
        float X = Random.Range(-65, 65);
        if(X >15 && X> -15)
        {
            X = Random.Range(-50, 50);
        }
        transform.position = new Vector2(PlayerPos.position.x + X, Base.position.y);
        //Invoke("OnActive", 1.5f);
    }
    void OnActive()
    {
        gameObject.SetActive(true);
    }


}
