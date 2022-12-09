using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //목록 : 스테이지, 위치, 목숨, 스텟, 퍼즐, 스킬, 보스
    public int stage;
    public Transform position;
    public int health;
    public int stat1;
    public int stat2;
    public int stat3;
    public int stat4;
    public int stat5;
    public bool solvedPuzzle1;
    public bool solvedPuzzle2;
    public bool solvedPuzzle3;
    public bool getSkillDash;
    public bool getSkillBall;
    public bool getSkillRange;
    public bool killBoss1;
    public bool killBoss2;
    public bool killBoss3;


    public GameData(int _stage)
    {
        stage = _stage;
    }
}
