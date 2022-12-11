using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using UnityEngine.SceneManagement;


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
    public float exp;

    public Vector3 spawnPoint;
    public int life;

    public float stat1_AP;
    public float stat2_AR;
    public float stat3_AS;
    public float stat4_AV;
    public int stat5_HP;

    public int stat1_APCount;
    public int stat2_ARCount;
    public int stat3_ASCount;
    public int stat4_AVCount;
    public int stat5_HPCount;

    public bool gotSkillDash;
    public bool gotSkillBall;
    public bool gotSkillRange;
}


/* 세이브 매니저 */

public class SaveManager : MonoBehaviour
{
    public GameData _gameData;
    public PlayerData _playerData;
    public bool haveSaveFile;
    public bool clearAllGame;
    public bool settingPlayer;

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
            Debug.Log("인스턴스 파괴~");
        }

        //LoadGameDataFromJson();
        LoadPlayerDataFromJson();
    }

    private void Update()
    {
        if(settingPlayer)
        {
            settingPlayer = false;
            Invoke("SetPlayerDataValues", 3f);
        }
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
        GetPlayerDataValues();
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

    public void PlayerSpawn()
    {
        //; //Main Scene
    }

    public void GetGameDataValues()
    {
        //퍼즐 매니저
        //보스 킬 여부
    }

    public void GetPlayerDataValues()
    {
        GameObject player = GameObject.Find("Player");

        Level playerLevel = player.GetComponent<Level>();
        _playerData.level = playerLevel.level;
        _playerData.exp = playerLevel.expCurrent;
        
        CharacterStats playerStat = player.GetComponent<CharacterStats>();
        _playerData.life = playerStat.maxHP;
        _playerData.stat1_AP = playerStat.attackPower;
        _playerData.stat2_AR = playerStat.attackRange;
        _playerData.stat3_AS = playerStat.attackSpeed;
        _playerData.stat4_AV = playerStat.avoidanceRate;
        _playerData.stat5_HP = playerStat.maxHP;
        _playerData.stat1_APCount = playerStat.APCount;
        _playerData.stat2_ARCount = playerStat.ARCount;
        _playerData.stat3_ASCount = playerStat.ASCount;
        _playerData.stat4_AVCount = playerStat.AvoidCount;
        _playerData.stat5_HPCount = playerStat.HPCount;

        data playerData = GameObject.Find("Main Camera").GetComponent<data>();
        _playerData.spawnPoint = playerData.Respawn.position;
    }

    public void SetPlayerDataValues()
    {
        GameObject player = GameObject.Find("Player");
        CharacterStats playerStat = player.gameObject.GetComponent<CharacterStats>();

        playerStat.maxHP = _playerData.life;
        //player.transform.position = new Vector3(0, 0, 0);
        player.transform.position = _playerData.spawnPoint;

    }
}
