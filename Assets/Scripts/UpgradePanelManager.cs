using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanelManager : MonoBehaviour
{
    Level level;
    CharacterStats stats;
    public GameObject panel;
    public Button btn1, btn2, btn3;

    //여기서 해야하는것
    //1. 7번 강화 끝난 스텟은 이미지 어둡게 처리하고 밑에 "최대 강화 횟수를 모두 소진하였습니다." Text 띄우기?
    //2. 강화 끝난 스텟은 클릭해도 강화창이 안 꺼지게 하기

    //일단 무슨 버튼을 눌렀는지를 알아야함.


    private void Start()
    {
        level = GameObject.Find("Player").GetComponent<Level>();
        stats = GameObject.Find("Player").GetComponent<CharacterStats>();
        btn1.onClick.AddListener(delegate { ClosePanel(1); });
        btn2.onClick.AddListener(delegate { ClosePanel(2); });
        btn3.onClick.AddListener(delegate { ClosePanel(3); });
    }

    public void OpenPanel()
    {
        Time.timeScale = 0f;
        panel.SetActive(true);
    }

    public void ClosePanel(int btnNum)
    {
        //강화 횟수가 다 소진된 스텟은 클릭해도 꺼지지 않음
        switch (btnNum)
        {
            case 1:
                string name1 = btn1.transform.Find("Image1").GetComponent<Image>().sprite.name.Substring(0, 1);
                GameObject.Find("OverText").transform.Find("OverText1").gameObject.SetActive(true);
                if (stats.APCount >= 7 && name1 == "1") return;
                else if (stats.AvoidCount >= 7 && name1 == "2") return;
                else if (stats.ASCount >= 7 && name1 == "3") return;
                else if (stats.ARCount >= 7 && name1 == "4") return;
                else if (stats.HPCount >= 7 && name1 == "5") return;
                break;
            case 2:
                string name2 = btn2.transform.Find("Image2").GetComponent<Image>().sprite.name.Substring(0, 1);
                GameObject.Find("OverText").transform.Find("OverText2").gameObject.SetActive(true);
                if (stats.APCount >= 7 && name2 == "1") return;
                else if (stats.AvoidCount >= 7 && name2 == "2") return;
                else if (stats.ASCount >= 7 && name2 == "3") return;
                else if (stats.ARCount >= 7 && name2 == "4") return;
                else if (stats.HPCount >= 7 && name2 == "5") return;
                break;
            case 3:
                string name3 = btn3.transform.Find("Image3").GetComponent<Image>().sprite.name.Substring(0, 1);
                GameObject.Find("OverText").transform.Find("OverText3").gameObject.SetActive(true);
                if (stats.APCount >= 7 && name3 == "1") return;
                else if (stats.AvoidCount >= 7 && name3 == "2") return;
                else if (stats.ASCount >= 7 && name3 == "3") return;
                else if (stats.ARCount >= 7 && name3 == "4") return;
                else if (stats.HPCount >= 7 && name3 == "5") return;
                break;
        }
        level.Wait = false;
        GameObject.Find("OverText").transform.Find("OverText1").gameObject.SetActive(false);
        GameObject.Find("OverText").transform.Find("OverText2").gameObject.SetActive(false);
        GameObject.Find("OverText").transform.Find("OverText3").gameObject.SetActive(false);
        Time.timeScale = 1f;
        panel.SetActive(false);
    }
}
