using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject PuzzlePosSet;
    public GameObject PuzzlePieceSet;
    public int Num;

    bool FirstClear = true;

    GameController Puzzle;

    public AudioSource audioSource;
    public AudioClip drawer;
    public AudioClip clear;

    public bool IsClear()
    {
        for (int i = 0; i < PuzzlePosSet.transform.childCount; i++)
        {
            //퍼즐위치의 자식이 없으면 모든 퍼즐조각이 놓여지지 않은 것
            if (PuzzlePosSet.transform.GetChild(i).childCount == 0)
            {
                return false;
            }
            //퍼즐조각의 번호와 퍼즐 위치 번호가 일치하지 않으면 퍼즐은 완성되지 않은 것
            if (PuzzlePosSet.transform.GetChild(i).GetChild(0).GetComponent<PuzzlePiece>().piece_no != i)
            {
                return false;
            }
        }
        if (FirstClear)
        {
            if (Num == 1)
            {
                SaveManager.Instance._playerData.solvedPuzzle1 = true;
            }
            if (Num == 2)
            {
                SaveManager.Instance._playerData.solvedPuzzle2 = true;
            }
            if (Num == 3)
            {
                SaveManager.Instance._playerData.solvedPuzzle3 = true;
            }
            audioSource.PlayOneShot(clear);
            Invoke("SetOff", 1f);
        }
        FirstClear = false;

        return true;
    }

    void Start()
    {
        Puzzle = GameObject.Find("GameManager").GetComponent<GameController>();
    }

    void Update()
    {
        IsClear();
    }

    void SetOff()
    {
        audioSource.PlayOneShot(drawer);
        gameObject.SetActive(false);
    }
}
