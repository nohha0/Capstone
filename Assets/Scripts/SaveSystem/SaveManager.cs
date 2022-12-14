using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using UnityEngine.SceneManagement;


public class PlayerData
{
    /* 게임 정보 */
    public bool haveSaveFile = false;
    public bool FirstSaveCol = false;
    public bool FirstStart = false;

    /* 퍼즐 클리어 여부 */
    public bool solvedPuzzle1 = false;
    public bool solvedPuzzle2 = false;
    public bool solvedPuzzle3 = false;

    /* 보스 클리어 (스킬 획득) 여부 */
    public bool killedBoss1 = false;
    public bool killedBoss2 = false;
    public bool killedBoss3 = false;
    public bool clearAllGame = false;

    /* 플레이어 정보 */
    public int level;
    public float exp;
    public Vector3 spawnPoint;
    public int stage;

    /* 스텟 정보 */
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
}


/* 세이브 매니저 */

public class SaveManager : MonoBehaviour
{
    public PlayerData _playerData;
    
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

        LoadPlayerDataFromJson();
    }

    private void Update()
    {
        if(settingPlayer)
        {
            settingPlayer = false;
            Invoke("SetPlayerDataValues", 0.5f);
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

    public void GetPlayerDataValues()
    {
        GameObject player = GameObject.Find("Player");
        Level playerLevel = player.GetComponent<Level>();
        CharacterStats playerStat = player.GetComponent<CharacterStats>();
        data playerData = GameObject.Find("Main Camera").GetComponent<data>();

        _playerData.spawnPoint = playerData.Respawn.position;
        _playerData.stage = playerData.Stage;
        _playerData.level = playerLevel.level;
        _playerData.exp = playerLevel.expCurrent;
        
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
    }

    public void SetPlayerDataValues()
    {
        GameObject player = GameObject.Find("Player");
        CharacterStats playerStat = player.gameObject.GetComponent<CharacterStats>();
        Level playerLevel = player.GetComponent<Level>();
        data playerData = GameObject.Find("Main Camera").GetComponent<data>();

        player.transform.position = _playerData.spawnPoint;
        playerData.Stage = _playerData.stage;
        playerLevel.level = _playerData.level;
        playerLevel.expCurrent = _playerData.exp;

        playerStat.attackPower = _playerData.stat1_AP;
        playerStat.attackRange = _playerData.stat2_AR;
        playerStat.attackSpeed = _playerData.stat3_AS;
        playerStat.avoidanceRate = _playerData.stat4_AV;
        playerStat.maxHP = _playerData.stat5_HP;

        playerStat.APCount = _playerData.stat1_APCount;
        playerStat.ARCount = _playerData.stat2_ARCount;
        playerStat.ASCount = _playerData.stat3_ASCount;
        playerStat.AvoidCount = _playerData.stat4_AVCount;
        playerStat.HPCount = _playerData.stat5_HPCount;
    }
}
