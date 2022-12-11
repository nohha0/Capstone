using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    [SerializeField]
    Button btn1, btn2, btn3;
    Image spr1, spr2, spr3;

    public int currentHP;               //플레이어의 현재 목숨

    //강화 가능한 스텟
    public float attackPower = 1f;      //공격력
    public float avoidanceRate = 1f;    //회피율
    public float attackSpeed = 1f;      //공격속도
    public float attackRange = 1f;      //공격범위
    public int maxHP = 10;              //플레이어의 최대 목숨

    //각 스텟은 최대 7회까지 강화 가능함
    public int APCount = 0;
    public int AvoidCount = 0;
    public int ASCount = 0;
    public int ARCount = 0;
    public int HPCount = 0;

    private void Start()
    {
        maxHP = 10;
        currentHP = maxHP;

        //이렇게 귀찮게 찾아가는 이유는 UpgradePanel이 처음에 비활성화 상태이기 때문에~
        spr1 = GameObject.Find("Canvas").transform.Find("UpgradePanel").transform.Find("UpgradeBtn1").transform.Find("Image1").GetComponent<Image>();
        spr2 = GameObject.Find("Canvas").transform.Find("UpgradePanel").transform.Find("UpgradeBtn2").transform.Find("Image2").GetComponent<Image>();
        spr3 = GameObject.Find("Canvas").transform.Find("UpgradePanel").transform.Find("UpgradeBtn3").transform.Find("Image3").GetComponent<Image>();
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
            case "1AttackPower_0":
                if (APCount >= 7) return;
                attackPower += 0.2f;
                APCount++;
                Debug.Log("공격력 증가");
                break;
            case "2Avoidance_0":
                if (AvoidCount >= 7) return;
                avoidanceRate += 0.2f;
                AvoidCount++;
                Debug.Log("회피율 증가~!");
                break;
            case "3AttckSpeed_0":
                if (ASCount >= 7) return;
                attackSpeed += 0.2f;
                ASCount++;
                Debug.Log("공격 속도 증가~!");
                break;
            case "4AttackRange_0":
                if (ARCount >= 7) return;
                attackRange += 0.2f;
                ARCount++;
                Debug.Log("공격 범위 증가~!");
                break;
            case "5HpUp_0":
                if (HPCount >= 7) return;
                maxHP++;
                HPCount++;
                Debug.Log("목숨 한 개 증가~!");
                break;
        }
    }

}