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

        branchXs = new float[2];
        for (int i = 0; i < 2; i++)
        {
            branchXs[i] = bigbasePosition.position.x + (bigDistance * i);
        }

        smallbranchXs = new float[20];
        for (int i = 0; i < 20; i++)    
        {
            smallbranchXs[i] = basePosition.position.x + (smallDistance * i);
        }
    }

    override protected void Update()
    {
        base.Update();
        if ((targetGameObject.transform.position - transform.position).magnitude <= mag)
        {
            ChangeState();
            if (onBranch && !callBranch) CreateBranch();
            if (onScissors && !callScissors) ThrowScissors();
            if (onRest) Rest();
            timeUntilChangeState -= Time.deltaTime;
        }
        if (targetGameObject.transform.position.x > transform.position.x) spriteRend.flipX = true;
        else if (targetGameObject.transform.position.x < transform.position.x) spriteRend.flipX = false;

    }

    void Rest()
    {

    }

    void CreateBranch()
    {
        if (!onBranch) return;
        callBranch = true;
        Debug.Log("CreateBranch");

        int randPos = Random.Range(0, 2);
        branchPosition = new Vector3(branchXs[randPos], bigbasePosition.position.y, transform.position.z);
        Instantiate(branch, branchPosition, transform.rotation);
        Invoke("CreateBranch", 3);


    }

    void ThrowScissors()
    {
        if (!onScissors) return;
        callScissors = true;
        Debug.Log("ThrowScissors");

        smallBranchPos = new Vector3(smallbranchXs[smallPosNum], basePosition.position.y, transform.position.z);
        Instantiate(smallBranch, smallBranchPos, transform.rotation);
        smallPosNum++;
        if(smallPosNum >= 20) smallPosNum = 0;
        Invoke("ThrowScissors", 0.08f);

        /*
        if (spriteRend.flipX) scissorsPosition = new Vector3(transform.position.x + 10f, transform.position.y - 5f, transform.position.z);
        else scissorsPosition = new Vector3(transform.position.x -10f, transform.position.y - 5f, transform.position.z);
        Instantiate(scissors, scissorsPosition, transform.rotation);
        Invoke("ThrowScissors", 1);
        */
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
            timeUntilChangeState = 5f;
        }
        else if (onRest && timeUntilChangeState <= 0)
        {
            onRest = false;
            timeUntilChangeState = 10f;

            int rand = Random.Range(0, 2);
            Debug.Log(rand);

            if (rand == 0)
                onBranch = true;
            else
                onScissors = true;
        }
    }
}
