using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject PuzzlePosSet;
    public GameObject PuzzlePieceSet;

    public bool IsClear()
    {
        for (int i = 0; i < PuzzlePosSet.transform.childCount; i++)
        {
            //������ġ�� �ڽ��� ������ ��� ���������� �������� ���� ��
            if (PuzzlePosSet.transform.GetChild(i).childCount == 0)
            {
                return false;
            }
            //���������� ��ȣ�� ���� ��ġ ��ȣ�� ��ġ���� ������ ������ �ϼ����� ���� ��
            if (PuzzlePosSet.transform.GetChild(i).GetChild(0).GetComponent<PuzzlePiece>().piece_no != i)
            {
                return false;
            }
        }
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
