using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Sprite keepSpr;
    public Sprite storySpr;

    public Button KeepBtn;
    public Button StoryBtn;

    public GameObject gameStory;
    

    private void Update()
    {
        if(SaveManager.Instance._playerData.haveSaveFile) KeepBtn.image.sprite = keepSpr;

        if (SaveManager.Instance._playerData.clearAllGame)
        { 
            StoryBtn.image.sprite = storySpr;
            transform.Find("Story").gameObject.SetActive(true);
            //종료하기 내리기
            transform.Find("Close").position = transform.Find("PosClose").position;
        }
        else
        {
            transform.Find("Story").gameObject.SetActive(false);
            //종료하기 올리기
            Vector2 closePosition = new Vector2(transform.Find("PosStory").position.x, 
                                            transform.Find("PosStory").position.y - 10);
            transform.Find("Close").position = closePosition;
        }



        if (gameStory.activeSelf && Input.GetKey(KeyCode.Space))
        {
            gameStory.SetActive(false);
        }
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Intro");
    }

    public void KeepGame()
    {
        if (!SaveManager.Instance._playerData.haveSaveFile) return;

        SaveManager.Instance.settingPlayer = true;
        SceneManager.LoadScene("Main");
    }

    public void StoryOfGame()
    {
        if (!SaveManager.Instance._playerData.clearAllGame) return;

        gameStory.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
