using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


/* 데이터 타입 */

public class GameData
{
    [Header("퍼즐, 중간보스 클리어")]

    public bool solvedPuzzle1;
    public bool solvedPuzzle2;
    public bool solvedPuzzle3;

    public bool killedBoss1;
    public bool killedBoss2;
    public bool killedBoss3;
}

public class PlayerData
{
    [Header("플레이어 데이터")]

    public int level;
    public int exp;
    public int stage;

    public int savePointNum;
    public int life;

    public int stat1_AP;
    public int stat2_AR;
    public int stat3_AS;
    public int stat4_AV;
    public int stat5_HP;

    public bool gotSkillDash;
    public bool gotSkillBall;
    public bool gotSkillRange;
}


/* 세이브 매니저 */

public class SaveManager : MonoBehaviour
{
    public GameData _gameData;
    public PlayerData _playerData;

    private static SaveManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        LoadGameDataFromJson();
        LoadPlayerDataFromJson();
    }

    public static SaveManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }

            return instance;
        }
    }

    [ContextMenu("To Json GameData")]
    public void SaveGameDataToJson()
    {
        string jsonData = JsonUtility.ToJson(_gameData, true);
        string filePath = Path.Combine(Application.dataPath, "GameData.json");
        File.WriteAllText(filePath, jsonData);
    }

    [ContextMenu("From Json GameData")]
    public void LoadGameDataFromJson()
    {
        string filePath = Path.Combine(Application.dataPath, "GameData.json");

        if (File.Exists(filePath))
        {
            Debug.Log("Game Data Load");
            string jsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(jsonData);
        }
        else
        {
            Debug.Log("New Game Data");
            _gameData = new GameData();
        }
    }

    [ContextMenu("To Json PlayerData")]
    public void SavePlayerDataToJson()
    {
        string jsonData = JsonUtility.ToJson(_playerData, true);
        string filePath = Path.Combine(Application.dataPath, "PlayerData.json");
        File.WriteAllText(filePath, jsonData);
    }

    [ContextMenu("From Json PlayerData")]
    public void LoadPlayerDataFromJson()
    {
        string filePath = Path.Combine(Application.dataPath, "PlayerData.json");

        if (File.Exists(filePath))
        {
            Debug.Log("Player Data Load");
            string jsonData = File.ReadAllText(filePath);
            _playerData = JsonUtility.FromJson<PlayerData>(jsonData);
        }
        else
        {
            Debug.Log("New Player Data");
            _playerData = new PlayerData();
        }
    }

}
