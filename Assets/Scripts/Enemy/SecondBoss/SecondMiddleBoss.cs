using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondMiddleBoss : Enemy
{
    //Pen ------------------------------------------------------------------------------------
    public GameObject pen;
    public GameObject rotatingPen;
    public GameObject rotatePenPosObj; //인스펙터창에서 게임오브젝트 집어넣기

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
    float SetSkill;

    

    override protected void Start()
    {
        base.Start();
        firstPatten = false;
        viewing = false;
        curtime = delray;

        penXs = new float[4];
        penYs = new float[2];

        penXs[0] = rotatePenPosObj.transform.position.x;
        penXs[1] = rotatePenPosObj.transform.position.x - 50f;
        penXs[2] = rotatePenPosObj.transform.position.x - 100f;
        penXs[3] = rotatePenPosObj.transform.position.x - 150f;

        penYs[0] = rotatePenPosObj.transform.position.y - 15f;
        penYs[1] = rotatePenPosObj.transform.position.y + 15f;
    }

    override protected void Update()
    {
        base.Update();

        if (firstPatten && viewing && pens == 0) ViewingPen();
        else if (firstPatten && !viewing && rotatepens == 0)
        {
            CreateRotatePen();
        }
        else { }

<<<<<<< Updated upstream

        if (!OnPattern)
        {
            SetSkill = Random.RandomRange(0, 101);

            if (SetSkill > 60)
            {
                Invoke("Patten1", 2);
                //Patten1();
                Debug.Log("000000000");
            }
            else
            {
                Invoke("Setpos", 2);
                Debug.Log("111111111");
=======
            if (!OnPattern)
            {
                SetSkill = Random.RandomRange(0, 2);

                if (SetSkill == 0)
                {
                    Patten1();
                    Debug.Log("000000000");
                }
                else
                {
                    //Invoke("Setpos", 2);
                    Debug.Log("111111111");
                }
>>>>>>> Stashed changes
            }
        }
    }


    //Pen ------------------------------------------------------------------------------------

    void Patten1()
    {
        viewing = true;
        OnPattern = true;
        Invoke("Jump", 1);
    }

    void Jump()
    {
        firstPatten = true;
        distance = (targetGameObject.transform.position - transform.position);
        rigid.AddForce(new Vector2(distance.x * jumpSpeed, jumpForce));
        //rigid.velocity = Vector2.zero;
        //rigid.AddForce(new Vector2(distance.normalized.x * jumpSpeed, jumpForce));
        Debug.Log("점프..");
    }

    void ViewingPen()
    {
        pens++;
        penPosition = new Vector3(transform.position.x, transform.position.y + 50f, transform.position.z);
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
        //float penX = Random.RandomRange(1509f, 1652f);
        //float penY = Random.RandomRange(-524f, -483f);
        int Xindex = Random.Range(0, 4); //0,1,2,3
        int Yindex = Random.Range(0, 2); //0,1,2

        rotatingPenPosition = new Vector3(penXs[Xindex], penYs[Yindex], transform.position.z);
        Instantiate(rotatingPen, rotatingPenPosition, transform.rotation);

        Invoke("CreateRotatePen", 2);
    }

    void OffViewing()
    {
        viewing = false;
        pens = 0;
    }

    //Wind ------------------------------------------------------------------------------------

    void Setpos()
    {
        OnPattern = true;

        if (i >= 7)
        {
            i = 0;
            OnPattern = false;
            return;
        }

        if (i < 7)
        {
            if (!spriteRend.flipX)  //캐릭터가 오른쪽에 있을경우  
            {
                if (curtime <= 0 && i < 3)
                {
                    Vector3 setpos1 = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);
                    Instantiate(Windywave, setpos1, transform.rotation);
                    curtime = delray;
                    i++;

                }
                else if (i == 3 && curtime <= 0)
                {
                    curtime = 0.6f;
                    i++;
                }
                else if (i > 3 && curtime <= 0)
                {
                    Vector3 setpos2 = new Vector3(transform.position.x + 10, transform.position.y + 45, transform.position.z);
                    Instantiate(Windywave, setpos2, transform.rotation);
                    curtime = delray;
                    i++;
                }
                
            }
            curtime -= Time.deltaTime;
        }

        Setpos();
    }

}
