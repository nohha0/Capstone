using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class FirstMiddleBoss : Enemy
{
    public GameObject scissors;
    public GameObject branch;

    Vector3 scissorsPosition;
    Vector3 branchPosition;
    int scissorsCount;
    int branchCount;

    float[] penXs = { 1517, 1580, 1644 };

    override protected void Start()
    {
        base.Start();
        scissorsCount = 0;
        branchCount = 0;
        //ThrowScissors();
        CreateBranch();
    }

    override protected void Update()
    {
        base.Update();
    }

    void CreateBranch()
    {
        branchCount++;

        int randPos = Random.Range(0, 3);
        branchPosition = new Vector3(penXs[randPos], transform.position.y - 50f, transform.position.z);
        Instantiate(branch, branchPosition, transform.rotation);

        Invoke("CreateBranch", 3);
    }

    void ThrowScissors()
    {
        if (scissorsCount >= 10)
        {
            scissorsCount = 0;
            return;
        }
        scissorsCount++;
        scissorsPosition = new Vector3(transform.position.x, transform.position.y + 30f, transform.position.z);
        Instantiate(scissors, scissorsPosition, transform.rotation);

        //Invoke("ThrowScissors", 2);
    }

}
