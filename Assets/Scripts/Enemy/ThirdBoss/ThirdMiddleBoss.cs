using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdMiddleBoss : Enemy
{
    //몬스터 소환프리팹 
    [SerializeField]
    GameObject Nomal_1;
    GameObject Nomal_2;
    GameObject Fly_1;
    GameObject Fly_2;


    //백터 배열


    override protected void Start()
    {
        base.Start();

    }

    override protected void Update()
    {
        base.Update();


    }
    //몬스터 소환스킬
    void SetMonster()
    {

    }

}
