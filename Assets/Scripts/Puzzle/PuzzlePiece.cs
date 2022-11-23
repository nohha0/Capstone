using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public int snapOffset = 30;
    public PuzzleManager puzzle;
    public int piece_no;

    bool CheckSnapPuzzle()
    {
        for (int i = 0; i < puzzle.PuzzlePosSet.transform.childCount; i++)
        {
            //��ġ�� �ڽĿ�����Ʈ�� ������ �̹� ���������� ������ ��
            if (puzzle.PuzzlePosSet.transform.GetChild(i).childCount != 0)
            {
                continue;
            }
            else if (Vector2.Distance(puzzle.PuzzlePosSet.transform.GetChild(i).position, transform.position) < snapOffset)
            {
                transform.SetParent(puzzle.PuzzlePosSet.transform.GetChild(i).transform);
                transform.localPosition = Vector3.zero;
                return true;
            }
        }
        return false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //��ġ�ϴ� ��ġ�� ���� ��� �θ��ڽ� ���踦 ����
        if (!CheckSnapPuzzle())
        {
            transform.SetParent(puzzle.PuzzlePieceSet.transform);
        }

        if (puzzle.IsClear())
        {
            Debug.Log("Clear");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        piece_no = gameObject.name[gameObject.name.Length - 1] - '0';
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
