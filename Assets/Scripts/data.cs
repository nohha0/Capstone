using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class data : MonoBehaviour
{
    public int Lcount;             //�������� ���� ����
    public int Rcount;             //���������� ���ݽ���

    public int Stage = 5;
    //1:ȭ��� 2:�ֹ� 3:�Ž� 4:���� ����, 5: ���� ����, 6:�Ž�,7:������, 8:����

    //----------------------
    public Transform Respawn;
    private void Start()
    {
        Lcount = 0;
        Rcount = 0;
    }

}
