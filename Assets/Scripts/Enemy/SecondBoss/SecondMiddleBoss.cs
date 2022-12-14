using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondMiddleBoss : Enemy
{

    //Pen ------------------------------------------------------------------------------------
    public GameObject pen;
    public GameObject rotatingPen;
    public GameObject rotatePenPosObj;

    public int jumpForce;
    public float jumpSpeed;

    Vector3 penPosition;
    Vector3 rotatingPenPosition;
    Vector2 distance;
    Vector3 windPosition;

    float[] penXs;
    float[] penYs;

    public Transform windPos;
    float[] windYs;


    bool OnPattern = false;
    bool viewing;
    bool firstPatten;
    int rotatepens;
    int pens;

    int countwind = 0;

    //Wind ------------------------------------------------------------------------------------
    public GameObject Windywave;

    float SetSkill;
    bool SetWindy = false;

    override protected void Start()
    {
        base.Start();

        firstPatten = false;
        viewing = false;

        penXs = new float[5];
        penYs = new float[2];

        penXs[0] = rotatePenPosObj.transform.position.x - 140f;
        penXs[1] = rotatePenPosObj.transform.position.x - 70f;
        penXs[2] = rotatePenPosObj.transform.position.x;
        penXs[3] = rotatePenPosObj.transform.position.x + 70f;
        penXs[4] = rotatePenPosObj.transform.position.x + 140f;

        penYs[0] = rotatePenPosObj.transform.position.y;
        penYs[1] = rotatePenPosObj.transform.position.y + 50f;

        windYs = new float[3];

        windYs[0] = windPos.position.y;
        windYs[1] = windPos.position.y + 30;
        windYs[2] = windPos.position.y + 60;

    }

    override protected void Update()
    {
        base.Update();

        if ((targetGameObject.transform.position - transform.position).magnitude <= mag &&BossPlay)
        {
            if (targetGameObject.transform.position.x > transform.position.x) spriteRend.flipX = true;
            else if (targetGameObject.transform.position.x < transform.position.x) spriteRend.flipX = false;

            if (firstPatten && viewing && pens == 0) ViewingPen();
            else if (firstPatten && !viewing && rotatepens == 0) CreateRotatePen();

            if (!OnPattern)
            {
                SetSkill = Random.Range(0, 2);

                if (SetSkill == 0)
                {
                    Patten1();
                    Debug.Log("000000000");
                }
                else
                {
                    Patten2();
                    Debug.Log("111111111");
                }
            }
        }
        //---------------------------------
        if (HP <= 0)
        {
            Destroy(transform.GetChild(0).gameObject);
            spriteRend.color = new Color(0.8f, 0.8f, 0.8f);
            //애니메이션 끝나면서 경험치 주셈
            //애니함수 호출해야하는데 지금은 그냥 다이 스킬 획득 호출
            DieAni();
        }

    }


    //Pen ------------------------------------------------------------------------------------

    void Patten2()
    {
        OnPattern = true;

        CreateWind();
    }

    void CreateWind()
    {
        if (countwind >= 10)
        {
            countwind = 0;
            OnPattern = false;
            return;
        }
        countwind++;
        int Yindex = Random.Range(0, 3);

        windPosition = new Vector3(windPos.position.x, windYs[Yindex], transform.position.z);
        Instantiate(Windywave, windPosition, transform.rotation);

        Invoke("CreateWind", 1);
    }


    void Patten1()
    {
        OnPattern = true;
        Jump();
    }

    void Jump()
    {
        Invoke("OnViewing", 2);
        firstPatten = true;
        distance = targetGameObject.transform.position - transform.position;
        rigid.AddForce(new Vector2(distance.x * jumpSpeed, jumpForce));
    }

    void ViewingPen()
    {
        pens++;
        penPosition = new Vector3(transform.position.x, transform.position.y + 30f, transform.position.z);
        Instantiate(pen, penPosition, transform.rotation);

        Invoke("OffViewing", 1);
    }

    void CreateRotatePen()
    {
        if (rotatepens >= 10)
        {
            rotatepens = 0;
            OnPattern = false;
            firstPatten = false;
            return;
        }

        rotatepens++;
        int Xindex = Random.Range(0, 5); //0,1,2,3,4
        int Yindex = Random.Range(0, 2); //0,1

        rotatingPenPosition = new Vector3(penXs[Xindex], penYs[Yindex], transform.position.z);
        Instantiate(rotatingPen, rotatingPenPosition, transform.rotation);

        Invoke("CreateRotatePen", 2);

    }

    void OnViewing()
    {
        viewing = true;
    }

    void OffViewing()
    {
        viewing = false;
        pens = 0;
    }

    //Wind ------------------------------------------------------------------


    void DieAni()
    {
        //애니가 끝나고, 경험치 획득후(파티클 획득)
        GameObject.Find("Canvas").transform.Find("튜토리얼").transform.Find("스킬").transform.Find("설명 텍스트").gameObject.SetActive(true);
        Invoke("delaytutorial", 1f);
        SaveManager.Instance._playerData.killedBoss1 = true;  //이거 키면 게임 오브젝트 삭제
    }

    void delaytutorial()
    {
        GameObject.Find("Canvas").transform.Find("튜토리얼").transform.Find("스킬").transform.Find("스킬1").gameObject.SetActive(true);
    }
}
