using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    public int ToStage;
    GameObject PlayerPos;
    public Vector2 MoveSetPos;
    public Animator penelani;
    data stage;

    public int NumDoor = 0;

    public bool asDoor = false;
    bool FirstKey =true;

    //--------------------------------
    GameController Puzzle;

    //GameObject P1, P2, P3;
    private void Start()
    {
        penelani = GameObject.Find("���").GetComponent<Animator>();
        stage = GameObject.Find("Main Camera").GetComponent<data>();
        PlayerPos = GameObject.Find("Player");
        Puzzle = GameObject.Find("GameManager").GetComponent<GameController>();


    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player") && !asDoor)
        {
            penelani.SetTrigger("��� ����");
            GameObject.Find("Player").GetComponent<PlayerController>().movable = false;
            Invoke("IsMove", 1.3f);
            PlayerPos.transform.position = MoveSetPos;
            stage.Stage = ToStage;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.tag == "Player" && asDoor && Input.GetKey(KeyCode.Space))
        {
            if(NumDoor == 0)
            {
                penelani.SetTrigger("��� ����");
                GameObject.Find("Player").GetComponent<PlayerController>().movable = false;
                Invoke("IsMove", 1.3f);
                PlayerPos.transform.position = MoveSetPos;
                stage.Stage = ToStage;
            }
            if (NumDoor == 1)
            {
                if(SaveManager.Instance._playerData.solvedPuzzle1) 
                {
                    penelani.SetTrigger("��� ����");
                    GameObject.Find("Player").GetComponent<PlayerController>().movable = false;
                    Invoke("IsMove", 1.3f);
                    PlayerPos.transform.position = MoveSetPos;
                    stage.Stage = ToStage;
                }
                else if (!SaveManager.Instance._playerData.solvedPuzzle1 && FirstKey)
                {
                    GameObject.Find("Canvas").transform.Find("Ʃ�丮��").transform.Find("����").transform.Find("S3").gameObject.SetActive(true);
                    //��Ȱ��ȭ Ʃ�丮��
                    FirstKey = false;
                }
            }
            if (NumDoor == 2)
            {
                if (SaveManager.Instance._playerData.solvedPuzzle2)
                {
                    penelani.SetTrigger("��� ����");
                    GameObject.Find("Player").GetComponent<PlayerController>().movable = false;
                    Invoke("IsMove", 1.3f);
                    PlayerPos.transform.position = MoveSetPos;
                    stage.Stage = ToStage;
                }
                else if (!SaveManager.Instance._playerData.solvedPuzzle2 && FirstKey)
                {
                    GameObject.Find("Canvas").transform.Find("Ʃ�丮��").transform.Find("����").transform.Find("S6").gameObject.SetActive(true);
                    FirstKey = false;
                }
            }
            if (NumDoor == 3)
            {
                if (SaveManager.Instance._playerData.solvedPuzzle3)
                {
                    penelani.SetTrigger("��� ����");
                    GameObject.Find("Player").GetComponent<PlayerController>().movable = false;
                    Invoke("IsMove", 1.3f);
                    PlayerPos.transform.position = MoveSetPos;
                    stage.Stage = ToStage;
                }
                else if (!SaveManager.Instance._playerData.solvedPuzzle3 && FirstKey)
                {
                    GameObject.Find("Canvas").transform.Find("Ʃ�丮��").transform.Find("����").transform.Find("S7").gameObject.SetActive(true);
                    //��Ȱ��ȭ Ʃ�丮��
                    FirstKey = false;
                }
            }
        }
    }

    void IsMove()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().movable = true;
        Debug.Log("��");
    }
}
