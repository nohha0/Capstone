using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    [SerializeField]
    Button btn1, btn2, btn3;
    protected float attackSpeed = 1.5f;          //공격속도
    float attackRange;           //공격범위
    protected int attackPower = 20;        //공격력
    int avoidanceRate = 0;       //회피율
    int maxHP = 10;               //최대 목숨
    public int currentHP;               //현재 목숨
    int attackPowerCount = 0;
    int attackSpeedCount = 0;
    int attackRangeCount = 0;
    int avoidanceRateCount = 0;
    int hpCount = 0;

    private void Start()
    {
        currentHP = maxHP;
        Image spr1 = GameObject.Find("Canvas").transform.Find("UpgradePanel").transform.Find("UpgradeBtn1").transform.Find("Image1").GetComponent<Image>();
        Image spr2 = GameObject.Find("Canvas").transform.Find("UpgradePanel").transform.Find("UpgradeBtn2").transform.Find("Image2").GetComponent<Image>();
        Image spr3 = GameObject.Find("Canvas").transform.Find("UpgradePanel").transform.Find("UpgradeBtn3").transform.Find("Image3").GetComponent<Image>();
        btn1.onClick.AddListener(delegate { StatUp(spr1.sprite.name); });
        btn2.onClick.AddListener(delegate { StatUp(spr2.sprite.name); });
        btn3.onClick.AddListener(delegate { StatUp(spr3.sprite.name); });
    }

    public void TakeDamage()
    {
        currentHP--;
    }

    public void StatUp(string spriteName)
    {
        switch (spriteName)
        {
            case "AttackPower":
                //int inc = (attackPower / 100) * 35;  //증가율 계산, 35% = (공격력 / 100) *35;
                //attackPower += inc;
                attackPowerCount++;
                Debug.Log("공격력 증가~!");
                break;
            case "AttackRange":
                attackRangeCount++;
                Debug.Log("공격 범위 증가~!");
                break;
            case "AttckSpeed":
                //attackSpeed -= 0.07; //공격주기(시간) 감소
                attackSpeedCount++;
                Debug.Log("공격 속도 증가~!");
                break;
            case "Avoidance":
                //avoidanceRate += 5;
                avoidanceRateCount++;
                Debug.Log("회피율 증가~!");
                break;
            case "HpUp":
                hpCount++;
                maxHP += 1;
                Debug.Log("목숨 한 개 증가~!");
                break;
        }
    }
}