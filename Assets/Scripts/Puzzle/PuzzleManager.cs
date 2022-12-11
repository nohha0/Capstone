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
            //퍼즐위치의 자식이 없으면 모든 퍼즐조각이 놓여지지 않은 것
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
            //퍼즐조각의 번호와 퍼즐 위치 번호가 일치하지 않으면 퍼즐은 완성되지 않은 것
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
