using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class FirstMiddleBoss : Enemy
{
    public GameObject scissors;
    public GameObject branch;
    public float height;

    Vector3 branchPosition;
    Vector3 scissorsPosition;
    
    float[] branchXs = { 1517, 1580, 1644 };

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
    }

    override protected void Update()
    {
        base.Update();
        ChangeState();
        if (onBranch && !callBranch) CreateBranch();
        if (onScissors && !callScissors) ThrowScissors();
        if (onRest) Rest();
        timeUntilChangeState -= Time.deltaTime;
    }

    void Rest()
    {

    }

    void CreateBranch()
    {
        if (!onBranch) return;
        callBranch = true;
        Debug.Log("CreateBranch");

        int randPos = Random.Range(0, 3);
        branchPosition = new Vector3(branchXs[randPos], transform.position.y - height, transform.position.z);
        Instantiate(branch, branchPosition, transform.rotation);
        Invoke("CreateBranch", 2);
    }

    void ThrowScissors()
    {
        if (!onScissors) return;
        callScissors = true;
        Debug.Log("ThrowScissors");

        scissorsPosition = new Vector3(transform.position.x, transform.position.y + 30f, transform.position.z);
        Instantiate(scissors, scissorsPosition, transform.rotation);
        Invoke("ThrowScissors", 1);
    }


    void ChangeState()
    {
        if (!onRest && timeUntilChangeState <= 0)
        {
            onScissors = false;
            onBranch = false;
            callBranch = false;
            callScissors = false;
            
            onRest = true;
            timeUntilChangeState = 5f;

            
        }
        if(onRest && timeUntilChangeState <= 0)
        {
            onRest = false;
            timeUntilChangeState = 10f;

            int rand = Random.Range(0, 2);
            Debug.Log(rand);

            if (rand == 0)
            {
                onBranch = true;
            }
            else
            {
                onScissors = true;
            }
        }
    }
}
