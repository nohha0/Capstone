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

    float[] penXs;
    float[] penYs;

    bool OnPattern = false;
    bool viewing;
    bool firstPatten;
    int rotatepens;
    int pens;

    //Wind ------------------------------------------------------------------------------------
    public GameObject Windywave;

    public float delray;
    float curtime;
    Vector3 setpos;
    int i = 0;

    private data script;

    float SetSkill;
    bool SetWindy = false;

    override protected void Start()
    {
        base.Start();
        script = GameObject.Find("Main Camera").GetComponent<data>();
        firstPatten = false;
        viewing = false;

        curtime = delray;

        penXs = new float[5];
        penYs = new float[2];

        penXs[0] = rotatePenPosObj.transform.position.x - 140f;
        penXs[1] = rotatePenPosObj.transform.position.x - 70f;
        penXs[2] = rotatePenPosObj.transform.position.x;
        penXs[3] = rotatePenPosObj.transform.position.x + 70f;
        penXs[4] = rotatePenPosObj.transform.position.x + 140f;

        penYs[0] = rotatePenPosObj.transform.position.y;
        penYs[1] = rotatePenPosObj.transform.position.y + 50f;
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
                    i = 0;
                    curtime = 0;
                    OnPattern = true;
                    Setpos();
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
        if (rotatepens >= 4)
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


    void Setpos()
    {

        SetWindy = true;
        
    }

    void InstantDOWN()
    {
        Vector3 setpos1 = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);
        Instantiate(Windywave, setpos1, transform.rotation);
        script.Lcount++;
    }
    void InstantUP()
    {
        Vector3 setpos2 = new Vector3(transform.position.x + 10, transform.position.y + 45, transform.position.z);
        Instantiate(Windywave, setpos2, transform.rotation);
        script.Lcount++;
    }

    private void FixedUpdate()
    {
        if (SetWindy)
        {
            if (!spriteRend.flipX)
            {
                if (curtime <= 0 && i < 3)
                {
                    InstantDOWN();
                    Invoke("InstantUP", 0.2f);
                    curtime = delray;
                    i++;
                }
            }
            curtime -= Time.deltaTime;
        }
        if (i >= 3)
        {
            SetWindy = false;
            Invoke("OnPettern", 7);
            i = 0;
        }
    }
    void OnPettern()
    {
        OnPattern = false;
    }

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
