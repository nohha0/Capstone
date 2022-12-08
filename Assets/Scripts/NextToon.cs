using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextToon : MonoBehaviour
{
    public Image currentToon;
    public bool intro;
    public bool outro; 

    public Sprite[] Toons;
    int toonIndex;

    void Start()
    {
        toonIndex = 0;

        if (intro)
        {
            //Toons = Resources.LoadAll<Sprite>("Intro"); //toon 18�� ������� �޾ƿ�
            currentToon.sprite = Toons[0];
        }
        else if(outro)
        {
            //Toons = Resources.LoadAll<Sprite>("Outro"); //toon 12�� ������� �޾ƿ�
            currentToon.sprite = Toons[0];
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            nextPage();
        }
    }

    void nextPage()
    {
        toonIndex++;

        if (toonIndex >= Toons.Length)
        {
            Debug.Log("���簡 ������ �������Դϴ�.");
            return;
        }

        currentToon.sprite = Toons[toonIndex];
        Debug.Log((toonIndex + 1) + " �������Դϴ�.");
    }
}
