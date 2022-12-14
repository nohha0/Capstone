using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class FirstMiddleBoss : Enemy
{
    public GameObject scissors;
    public GameObject branch;
    public GameObject smallBranch;
    //public float height;
    public Transform basePosition;
    public Transform bigbasePosition;

    Vector3 branchPosition;
    Vector3 smallBranchPos;
    Vector3 scissorsPosition;

    float[] branchXs;
    float[] smallbranchXs;
    public float smallDistance;
    public float bigDistance;
    public int smallPosNum;

    float timeUntilChangeState;
    bool onBranch;
    bool onScissors;
    bool onRest;
    bool callBranch;
    bool callScissors;

    override protected void Start()
    {
        base.Start();
        onScissors = false;
        onBranch = false;
        callBranch = false;
        callScissors = false;
        onRest = false;
        timeUntilChangeState = 0f;
        smallPosNum = 0;

        branchXs = new float[4];


        branchXs[0] = bigbasePosition.position.x;
        branchXs[1] = bigbasePosition.position.x + 50f;
        branchXs[2] = bigbasePosition.position.x + 100f;
        branchXs[3] = bigbasePosition.position.x + 150f;


        smallbranchXs = new float[20];
        for (int i = 0; i < 20; i++)    
        {
            smallbranchXs[i] = basePosition.position.x + (smallDistance * i);
        }
    }

    override protected void Update()
    {
        if(HP <= 0)
        {
            Die.transform.position = transform.position;
            Die.Play();
        }

        base.Update();

        if ((targetGameObject.transform.position - transform.position).magnitude <= mag &&BossPlay)
        {
            ChangeState();
            if (onBranch && !callBranch) CreateBranch();
            if (onScissors && !callScissors) ThrowScissors();
            if (onRest) Rest();
            timeUntilChangeState -= Time.deltaTime;
        }

        if (targetGameObject.transform.position.x > transform.position.x) spriteRend.flipX = true;
        else if (targetGameObject.transform.position.x < transform.position.x) spriteRend.flipX = false;

        if (HP <= 0)
        {
            DieAni();
        }

    }

    void Rest()
    {

    }

    void CreateBranch()
    {
        if (!onBranch) return;
        callBranch = true;

        int randPos = Random.Range(0, 4);
        branchPosition = new Vector3(branchXs[randPos], bigbasePosition.position.y, transform.position.z);
        Instantiate(branch, branchPosition, transform.rotation);
        Invoke("CreateBranch", 2);
    }

    void ThrowScissors()
    {
        if (!onScissors) return;
        callScissors = true;

        smallBranchPos = new Vector3(smallbranchXs[smallPosNum], basePosition.position.y, transform.position.z);
        Instantiate(smallBranch, smallBranchPos, transform.rotation);
        smallPosNum++;
        if(smallPosNum >= 20) smallPosNum = 0;
        Invoke("ThrowScissors", 0.08f);
    }


    void ChangeState()
    {
        if (!onRest && timeUntilChangeState <= 0)
        {
            onScissors = false;
            onBranch = false;
            callBranch = false;
            callScissors = false;
            smallPosNum = 0;

            onRest = true;
            timeUntilChangeState = 3f;
        }
        else if (onRest && timeUntilChangeState <= 0)
        {
            onRest = false;

            int rand = Random.Range(0, 2);

            if (rand == 0)
            {
                timeUntilChangeState = 8f;
                onBranch = true;
            }
            else
            {
                timeUntilChangeState = 12f;
                onScissors = true; //??????
            }
        }
    }

    void DieAni()
    {
        //?????? ??????, ?????? ??????(?????? ????)
        GameObject.Find("Canvas").transform.Find("????????").transform.Find("????").transform.Find("???? ??????").gameObject.SetActive(true);
        Invoke("delaytutorial", 1f);
        SaveManager.Instance._playerData.killedBoss1 = true;  //???? ???? ???? ???????? ????
    }

    void delaytutorial()
    {
        //???? ???? ????
        GameObject.Find("Canvas").transform.Find("????????").transform.Find("????").transform.Find("????0").gameObject.SetActive(true);
    }
}
