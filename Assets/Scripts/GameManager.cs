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
    public Sprite Defult_img;
    public Image[] UiLife;
    public Image Enhance_Slider;

    public GameObject dialogueBox;
    public Sprite dialogue;

    public GameObject startPanel;

    public bool isAction;
    public PlayerController playerCon;
    public data currentStage;
    bool SettedOn;
    bool onDialogue;
    bool changed;


    public GameObject skill_1;
    public GameObject skill_2;
    public GameObject skill_3;

    //-----------------------
    CharacterStats Stats;
    Level Bar;
    int leval;

    float itemCooldownTime = 5.0f;
    float updateTime = 0.0f;
    float fadeTime = 1f;
    bool onFadeIn;

    void Start()
    {
        Stats = GameObject.Find("Player").GetComponent<CharacterStats>();
        Bar = GameObject.Find("Player").GetComponent<Level>();
        SettedOn = false;
        onDialogue = false;
        changed = false;
        onFadeIn = false;
        startPanel.SetActive(true);
        Invoke("SetFadeIn", 1f);
        MaxLife = Stats.maxHP;
        leval = Bar.level;

    }

    void SetFadeIn()
    {
        onFadeIn = true;
    }

    void Update()
    {
        MaxLifeUpdate();
        LifeUpdate();
        //Bar_con();
        Bar_con();

        if(startPanel.activeSelf)
        {
            if (fadeTime > 0)
            {
                startPanel.GetComponent<Image>().color = new Color(0,0,0, fadeTime/1f);
            }
            else
            {
                startPanel.SetActive(false);
            }
            fadeTime -= Time.deltaTime;
        }

        if (currentStage.Stage == 8 && !SettedOn)
        {
            SettedOn = true;
            Invoke("OnDialog", 1f);
        }

        if (onDialogue && Input.GetKeyDown(KeyCode.Space))
        {
            //스프라이트 바꾸기
            //currentToon.sprite = Toons[toonIndex];
            if (!changed)
            {
                dialogueBox.GetComponent<Image>().sprite = dialogue;
                changed = true;
            }
            else
            {
                OffDialog();
            }
        }

    }

    void MaxLifeUpdate()
    {
        if(MaxLife<Stats.maxHP)
        {
            Stats.currentHP++;
        }

        MaxLife = Stats.maxHP;

        for (int i = MaxLife; i < UiLife.Length; i++)
        {
            UiLife[i].color = new Color(1, 1, 1, 0);
        }
        for (int i = 0; i < MaxLife; i++)
        {
            UiLife[i].color = new Color(1, 1, 1, 1);
        }

    }

    void LifeUpdate()    // 현재목숨 UI 결정
    {
        int currentLife = Stats.currentHP;

        if (currentLife < 0) return;

        for (int i = currentLife; i < UiLife.Length; i++)
        {
            UiLife[i].sprite = Change_img;
        }
        for (int i = 0; i < currentLife; i++)
        {
            UiLife[i].sprite = Defult_img;
        }
    }

    void Bar_con()
    {
        if(leval < Bar.level)
        {
            leval = Bar.level;
            Enhance_Slider.fillAmount = 0;
        }
        float bar = Bar.expCurrent / Bar.expLeft;
        if (Enhance_Slider.fillAmount <= bar)
        {
            //updateTime -= Time.deltaTime;
            Enhance_Slider.fillAmount += Time.deltaTime;
        }
    }

    public void OnDialog()
    {
        onDialogue = true;
        playerCon.IsMove = false;
        dialogueBox.SetActive(true);
    }

    public void OffDialog()
    {
        if (!changed) return;

        onDialogue = false;
        Debug.Log("ㅡ");
        playerCon.IsMove = true;
        dialogueBox.SetActive(false);
    }

}
