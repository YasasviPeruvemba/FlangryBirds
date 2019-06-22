using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUP : MonoBehaviour
{
    public void OnRed()
    {
        PlayerPrefs.SetString("Character","Red");
        SceneManager.LoadSceneAsync("FlappyNema");
        SceneManager.UnloadSceneAsync("OpeningPage");
    }
    public void OnBlue()
    {
        PlayerPrefs.SetString("Character","Blue");
        SceneManager.UnloadSceneAsync("OpeningPage");
        SceneManager.LoadSceneAsync("FlappyNema");
    }
    public void OnBlack()
    {
        PlayerPrefs.SetString("Character","Black");
        SceneManager.UnloadSceneAsync("OpeningPage");
        SceneManager.LoadSceneAsync("FlappyNema");
    }
    public void OnWhite()
    {
        PlayerPrefs.SetString("Character","White");
        SceneManager.UnloadSceneAsync("OpeningPage");
        SceneManager.LoadSceneAsync("FlappyNema");
    }
    public void OnYellow()
    {
        PlayerPrefs.SetString("Character","Yellow");
        SceneManager.UnloadSceneAsync("OpeningPage");
        SceneManager.LoadSceneAsync("FlappyNema");
    }
    public void OnGreen()
    {
        PlayerPrefs.SetString("Character","Green");
        SceneManager.UnloadSceneAsync("OpeningPage");
        SceneManager.LoadSceneAsync("FlappyNema");
    }

    public void OnExiting()
    {
        SceneManager.UnloadSceneAsync("OpeningPage");
        Application.Quit();
    }
}
