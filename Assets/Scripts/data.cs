using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class data : MonoBehaviour
{
    public Transform Respawn;

    public int Lcount;             //왼쪽으로 공격 시작
    public int Rcount;             //오른쪽으로 공격시작

    public int Stage = 4;
    int currentStage;
    //1:화장실 2:주방 3:거실 4:복도 가로, 5: 복도 세로, 6:거실,7:작은방, 8:옷장

    //----------------------
    public Vector2[] E1Pos;
    public Vector2[] E2Pos;
    public Vector2[] E3Pos;
    public Vector2[] E4Pos;
    public Vector2[] S5Pos;

    //----------------------
    public GameObject E1;
    public GameObject E2;
    public GameObject E3;
    public GameObject E4;

    private void Start()
    {
        Lcount = 0;
        Rcount = 0;
        currentStage = Stage;
    }

    private void Update()
    {
        if(currentStage != Stage)
        {
            if (Stage == 2)
            {
                for (int i = 0; i < E1Pos.Length; i++)
                {
                    Instantiate(E1, E1Pos[i], transform.rotation);
                }
                currentStage = Stage;
            }
            if (Stage == 3)
            {
                for (int i = 0; i < E2Pos.Length; i++)
                {
                    Instantiate(E2, E2Pos[i], transform.rotation);
                }
                currentStage = Stage;
            }
            if (Stage == 5)
            {
                for (int i = 0; i < S5Pos.Length; i++)
                {
                    int a = S5Pos.Length / 2;
                    if (i < 2)
                    {
                        Instantiate(E3, S5Pos[i], transform.rotation);
                    }
                    if (i > 2)
                    {
                        Instantiate(E4, S5Pos[i], transform.rotation);
                    }
                }
                currentStage = Stage;
            }
            if (Stage == 6)
            {
                for (int i = 0; i < E3Pos.Length; i++)
                {
                    Instantiate(E3, E3Pos[i], transform.rotation);
                }
                currentStage = Stage;
            }
            if (Stage == 7)
            {
                for (int i = 0; i < E4Pos.Length; i++)
                {
                    Instantiate(E4, E4Pos[i], transform.rotation);
                }
                currentStage = Stage;
            }
        }

    }

}
