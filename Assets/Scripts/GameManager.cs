using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void GameDelegate();
    public static event GameDelegate OnGameStart;
    public static event GameDelegate OnGameOverConfirmed;

    public static GameManager Instance;
    public GameObject startPage;
    public GameObject GameOverPage;
    public GameObject CountDownPage;
    public Transform t;
    public Text scoreText;

    Vector3 v;

    enum PageState
    {
        None,
        Start,
        GameOver,
        CountDown
    }

    int score = 0;
    bool gameOver = true;
    public bool GameOver { get {return gameOver; } }

    void OnEnable()
    {
        CountDownText.OnCountDownFinished += OnCountDownFinished;
        TapControl.OnPlayerScored += OnPlayerScored;
        TapControl.OnPlayerDied += OnPlayerDied;
    }

    void OnDisable()
    {
        CountDownText.OnCountDownFinished -= OnCountDownFinished;
        TapControl.OnPlayerScored -= OnPlayerScored;
        TapControl.OnPlayerDied -= OnPlayerDied;
    }

    void OnCountDownFinished()
    {
        SetPageState(PageState.None);
        OnGameStart();
        score = 0;
        gameOver = false;
    }

    void OnPlayerDied()
    {
        gameOver = true;
        int savedscore = PlayerPrefs.GetInt("HighScore");
        if(score > savedscore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        SetPageState(PageState.GameOver);
    }

    void OnPlayerScored()
    {
        score++;
        scoreText.text = score.ToString();
    }

    void Awake()
    {
        Instance = this;
        //Transform t = Environment.GetComponent<Transform>();
        //t.position = v;
        v.x = 0;
        v.y = 0;
        v.z = t.position.z;
        v.z += 5;
        t.position = v;
    }

    void SetPageState(PageState state)
    {
        switch (state)
        {
            case PageState.None:
                startPage.SetActive(false);
                GameOverPage.SetActive(false);
                CountDownPage.SetActive(false);
                break;
            case PageState.Start:
                startPage.SetActive(true);
                GameOverPage.SetActive(false);
                CountDownPage.SetActive(false);
                break;
            case PageState.GameOver:
                startPage.SetActive(false);
                GameOverPage.SetActive(true);
                CountDownPage.SetActive(false);
                break;
            case PageState.CountDown:
                startPage.SetActive(false);
                GameOverPage.SetActive(false);
                CountDownPage.SetActive(true);
                break;
        }
    }

    public void ConfirmGameOver()
    {
        //activated when player clicks restart button
        OnGameOverConfirmed(); //event declared above
        v.x = 0;
        v.y = 0;
        v.z = t.position.z;
        v.z += 5;
        t.position = v;
        scoreText.text = "0";
        SetPageState(PageState.Start);
    }

    public void StartGame()
    {
        //activated when player clicks play button
        SetPageState(PageState.CountDown);
    }

    public void EndGame()
    {
        SetPageState(PageState.None);
        SceneManager.UnloadSceneAsync("FlappyNema");
        SceneManager.LoadSceneAsync("OpeningPage");
    }

}
