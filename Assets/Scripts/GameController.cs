using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool FirstSaveCol = false;
    public bool FirstPuzzleCol = false;

    
    public bool Boss1Die = false;
    public bool Boss2Die = false;
    public bool Boss3Die = false;

    public PlayerController player;
    //���� 3�� ������ ���ְ� SaveManager.Instance._playerData.killedBoss1,2,3�� ��� ���� �����
    //���� true�� �Ǵ� ������ ���� 1,2,3 �� Die �Լ��� ȣ��� ���� �̹� ó���س���!


    //�̹� ���⿡ �ִ� �������� ������ �����ϰ� SaveManager.Instance.solvedPuzzle �� ��ü�ߴ� ����
    //���� �������� ������ �־��� �ٸ� ��ũ��Ʈ�鵵 ���� ������!


    public bool FirstStart = false;


    void Start()
    {
        
    }

    void Update()
    {

    }


}
