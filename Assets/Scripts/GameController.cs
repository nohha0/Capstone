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
    //위의 3개 변수는 없애고 SaveManager.Instance._playerData.killedBoss1,2,3를 대신 갖다 써줘요
    //저게 true가 되는 순간은 보스 1,2,3 의 Die 함수가 호출될 때로 이미 처리해놨음!


    //이미 여기에 있던 오픈퍼즐 변수는 삭제하고 SaveManager.Instance.solvedPuzzle 로 대체했다 하하
    //기존 오픈퍼즐 변수가 있었던 다른 스크립트들도 전부 수정함!


    public bool FirstStart = false;


    void Start()
    {
        
    }

    void Update()
    {

    }


}
