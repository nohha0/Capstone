using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int Life;       // ���� ���� ���
    int MaxLife;    //���� �÷��̾ ���� �ִ� �ִ���

    public Sprite Change_img;
    public Sprite Defult_img;
    public Image[] UiLife;
    public Image Enhance_Slider;

    public GameObject dialogueBox;
    public Text dialogue;
    public GameObject scanObject;
    public bool isAction;
    public PlayerController playerCon;

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
        int MaxLife = Stats.maxHP;
        for (int i = MaxLife; i < UiLife.Length; i++)
        {
            UiLife[i].color = new Color(1, 1, 1, 0);
        }

    }

    void LifeUpdate()    // ������ UI ����
    {
        int currentLife = Stats.currentHP;
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

    public void Action()
    {
        if(isAction){
            playerCon.movable = true;
            isAction = false;
        }else{
            playerCon.movable = false;
            isAction = true;
            //scanObject = scanObj;
            dialogue.text = "T�� ������ ���ܳ��� �ѹ� �� ������ �������~!";
        }
        dialogueBox.SetActive(isAction);
    }

}
