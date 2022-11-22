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

    float[] branchXs;
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
        branchXs = new float[3];
        branchXs[0] = transform.position.x;
        branchXs[1] = transform.position.x - 70;
        branchXs[2] = transform.position.x + 70;

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

        int randPos = Random.Range(0, 3);
        branchPosition = new Vector3(branchXs[randPos], transform.position.y + height, transform.position.z);
        Instantiate(branch, branchPosition, transform.rotation);
        Invoke("CreateBranch", 2);
    }

    void ThrowScissors()
    {
        if (!onScissors) return;
        callScissors = true;
        Debug.Log("ThrowScissors");

        if(spriteRend.flipX) scissorsPosition = new Vector3(transform.position.x + 10f, transform.position.y - 10f, transform.position.z);
        else scissorsPosition = new Vector3(transform.position.x -10f, transform.position.y - 10f, transform.position.z);
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
            timeUntilChangeState = 2.5f;
        }
        if (onRest && timeUntilChangeState <= 0)
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
