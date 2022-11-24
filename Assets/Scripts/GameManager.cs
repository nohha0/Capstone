using System;
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
    public Image Enhance_Slider;

    //-----------------------
    CharacterStats Stats;
    Level Bar;

    float itemCooldownTime = 5.0f;
    float updateTime = 0.0f;

    void Start()
    {
        Stats = GameObject.Find("Player").GetComponent<CharacterStats>();
        Bar = GameObject.Find("Player").GetComponent<Level>();
    }

    void Update()
    {
        MaxLifeUpdate();
        LifeUpdate();
        //Bar_con();
        Bar_con();
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
    void Bar_con()
    {
        float bar = Bar.expCurrent / Bar.expLeft;
        if (Enhance_Slider.fillAmount >= 1)
        {
            Enhance_Slider.fillAmount = 0;
            updateTime = 0;
        }
        if (Enhance_Slider.fillAmount <= bar)
        {

            updateTime -= Time.deltaTime;
            Enhance_Slider.fillAmount += Time.deltaTime;
        }


    }

}
