using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class data : MonoBehaviour
{
    public int Lcount;             //왼쪽으로 공격 시작
    public int Rcount;             //오른쪽으로 공격시작

    public int Stage = 5;
    //1:화장실 2:주방 3:거실 4:복도 가로, 5: 복도 세로, 6:거실,7:작은방, 8:옷장

    //----------------------
    public Transform Respawn;
    private void Start()
    {
        Lcount = 0;
        Rcount = 0;
    }

}
