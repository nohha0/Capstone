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

    //���⼭ �ؾ��ϴ°�
    //1. 7�� ��ȭ ���� ������ �̹��� ��Ӱ� ó���ϰ� �ؿ� "�ִ� ��ȭ Ƚ���� ��� �����Ͽ����ϴ�." Text ����?
    //2. ��ȭ ���� ������ Ŭ���ص� ��ȭâ�� �� ������ �ϱ�

    //�ϴ� ���� ��ư�� ���������� �˾ƾ���.


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
        //��ȭ Ƚ���� �� ������ ������ Ŭ���ص� ������ ����
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
