using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    [SerializeField]
    Button btn1, btn2, btn3;
    Image spr1, spr2, spr3;

    public int currentHP;               //�÷��̾��� ���� ���

    //��ȭ ������ ����
    public float attackPower = 1f;      //���ݷ�
    public float avoidanceRate = 1f;    //ȸ����
    public float attackSpeed = 1f;      //���ݼӵ�
    public float attackRange = 1f;      //���ݹ���
    public int maxHP = 10;              //�÷��̾��� �ִ� ���

    //�� ������ �ִ� 7ȸ���� ��ȭ ������
    public int APCount = 0;
    public int AvoidCount = 0;
    public int ASCount = 0;
    public int ARCount = 0;
    public int HPCount = 0;

    private void Start()
    {
        maxHP = 10;
        currentHP = maxHP;

        //�̷��� ������ ã�ư��� ������ UpgradePanel�� ó���� ��Ȱ��ȭ �����̱� ������~
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
                Debug.Log("���ݷ� ����");
                break;
            case "2Avoidance_0":
                if (AvoidCount >= 7) return;
                avoidanceRate += 0.2f;
                AvoidCount++;
                Debug.Log("ȸ���� ����~!");
                break;
            case "3AttckSpeed_0":
                if (ASCount >= 7) return;
                attackSpeed += 0.2f;
                ASCount++;
                Debug.Log("���� �ӵ� ����~!");
                break;
            case "4AttackRange_0":
                if (ARCount >= 7) return;
                attackRange += 0.2f;
                ARCount++;
                Debug.Log("���� ���� ����~!");
                break;
            case "5HpUp_0":
                if (HPCount >= 7) return;
                maxHP++;
                HPCount++;
                Debug.Log("��� �� �� ����~!");
                break;
        }
    }

}