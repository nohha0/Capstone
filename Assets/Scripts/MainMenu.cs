using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool haveSaveFile;
    public bool clearAllGame;

    public Sprite keepSpr;
    public Sprite storySpr;

    public Button KeepBtn;
    public Button StoryBtn;
    

    private void Start()
    {
        haveSaveFile = false;
        clearAllGame = false;
    }
    private void Update()
    {
        if(haveSaveFile) KeepBtn.image.sprite = keepSpr;
        if(clearAllGame) StoryBtn.image.sprite = storySpr;
    }

    public void GameStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void KeepGame()
    {

    }
    public void StoryOfGame()
    {

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
