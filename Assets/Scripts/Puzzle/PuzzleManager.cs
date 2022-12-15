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
