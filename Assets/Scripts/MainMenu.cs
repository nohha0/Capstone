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
        if(SaveManager.Instance._playerData.clearAllGame) StoryBtn.image.sprite = storySpr;

        if(gameStory.activeSelf && Input.GetKey(KeyCode.Space))
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
