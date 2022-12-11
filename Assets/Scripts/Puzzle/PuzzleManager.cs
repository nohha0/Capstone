using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject PuzzlePosSet;
    public GameObject PuzzlePieceSet;
    public int Num;

    bool FirstClear = true;

    //-----------------------
    GameController Puzzle;
    public bool IsClear()
    {
        for (int i = 0; i < PuzzlePosSet.transform.childCount; i++)
        {
            //������ġ�� �ڽ��� ������ ��� ���������� �������� ���� ��
            if (PuzzlePosSet.transform.GetChild(i).childCount == 0)
            {
                if(FirstClear)
                {
                    if (Num == 1)
                    {
                        Puzzle.OpenPuzzle1 = true;
                    }
                    if (Num == 2)
                    {
                        Puzzle.OpenPuzzle2 = true;
                    }
                    if (Num == 3)
                    {
                        Puzzle.OpenPuzzle3 = true;
                    }
                }
                FirstClear = false;
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
        Puzzle = GameObject.Find("GameManager").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        IsClear();
    }
}
