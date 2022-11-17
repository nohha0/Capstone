using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class data : MonoBehaviour
{
    public int Lcount;             //왼쪽으로 공격 시작
    public int Rcount;             //오른쪽으로 공격시작

    private void Start()
    {
        Lcount = 0;
        Rcount = 0;
    }
}
