using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
            //Toons = Resources.LoadAll<Sprite>("Intro"); //toon 18개 순서대로 받아옴
            currentToon.sprite = Toons[0];
        }
        else if(outro)
        {
            //Toons = Resources.LoadAll<Sprite>("Outro"); //toon 12개 순서대로 받아옴
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
            Debug.Log("현재가 마지막 페이지입니다.");
            if(intro) GoMainScene();
            return;
        }

        currentToon.sprite = Toons[toonIndex];
        Debug.Log((toonIndex + 1) + " 페이지입니다.");
    }

    void GoMainScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
