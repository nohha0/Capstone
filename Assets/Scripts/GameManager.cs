using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    int Life;       // 현재 깎여진 목숨
    int MaxLife;    //현재 플레이어가 갖고 있는 최대목숨

    public Sprite Change_img;
    public Image[] UiLife;
    

    //-----------------------
    CharacterStats Stats;

    void Start()
    {
        Stats = GameObject.Find("Player").GetComponent<CharacterStats>();
    }

    void Update()
    {
        MaxLifeUpdate();
        LifeUpdate();
    }

    void MaxLifeUpdate()
    {
        int MaxLife = Stats.currentmaxHP;
        for (int i = MaxLife; i < UiLife.Length; i++)
        {
            UiLife[i].color = new Color(1, 1, 1, 0);
        }

    }

    void LifeUpdate()    // 현재목숨 UI 결정
    {
        int currentLife = Stats.currentHP;
        for (int i = currentLife; i < UiLife.Length; i++)
        {
            UiLife[i].sprite = Change_img;
        }

    }
}
