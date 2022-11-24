using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    Sprite[] Images;

    public int level = 1;
    public int levelCount = 0;
    public float expCurrent = 0;             //현재경험치
    public float expLeft = 1000;             //변수. 레벨업에 필요한 경험치
    int expBase = 1000;             //상수. 레벨1→레벨2 필요한 경험치
    float expMod = 1.21f;           //경험치 증가량 (지수)

    public bool Wait = false;

    [SerializeField] UpgradePanelManager upgradePanel;

    float TO_LEVEL_UP
    {
        get 
        {
            return expLeft;
        }
    }

    private void Update()
    {
        CheckLevelUp();

        if (levelCount > 0 && Wait==false)
        {
            Wait = true;
            Enhance();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            GainExp(10000);
            Debug.Log("경험치 10000 획득");
        }
    }

    public void GainExp(int amount)
    {
        expCurrent += amount;
    }

    public void CheckLevelUp()
    {
        if (expCurrent >= TO_LEVEL_UP)
        {
            expLeft = (int)Mathf.Floor(expBase * Mathf.Pow(expMod, level));
            expCurrent -= TO_LEVEL_UP;
            level++;
            levelCount++;
        }
    }

    public void Enhance()
    {
        levelCount--;

        List<int> list = new List<int>() { 0, 1, 2, 3, 4 };
        
        for (int i = list.Count - 1; i > 0; i--)
        {
            int rnd = Random.Range(0, i);
            int temp = list[i];

            list[i] = list[rnd];
            list[rnd] = temp;
        }

        Images = Resources.LoadAll<Sprite>("Upgrades");

        upgradePanel.OpenPanel();

        Image img1 = GameObject.Find("Image1").GetComponent<Image>();
        Image img2 = GameObject.Find("Image2").GetComponent<Image>();
        Image img3 = GameObject.Find("Image3").GetComponent<Image>();

        img1.sprite = Images[list[0]];
        img2.sprite = Images[list[1]];
        img3.sprite = Images[list[2]];

    }


    void MaxValueCon()
    {

    }
}
