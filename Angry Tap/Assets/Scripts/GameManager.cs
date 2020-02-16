using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOverConfirmed;
    public static GameManager Instance;
    public GameObject startPage;
    public GameObject gameOverPage;
    public GameObject countdownPage;
    public Text scoreText;
    enum PageState
    {
        None,
        Start,
        GameOver,
        Countdown
    }

    int score = 0;
    bool gameOver = true;

    public bool GameOver
    {
        get
        {
            return gameOver;
        }
    }

    public int Score
    {
        get
        {
            return score;
        }
    }

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        CountdownText.OnCountdownFinished += OnCountdownFinished;
        TapController.OnPlayerDied += OnPlayerDied;
        TapController.OnPlayerScored += OnPlayerScored;
    }

    void OnDisable()
    {
        CountdownText.OnCountdownFinished -= OnCountdownFinished;
        TapController.OnPlayerDied -= OnPlayerDied;
        TapController.OnPlayerScored -= OnPlayerScored;
    }
    void OnCountdownFinished()
    { 
        SetPageStage(PageState.None);
        OnGameStarted(); //event sent to TapController
        score = 0;
        gameOver = false;  
    }

    void OnPlayerDied()
    {

        gameOver = true;
        int savedScore = PlayerPrefs.GetInt("HighScore");
        PlayerPrefs.SetInt("ScoreRank", score);
        if (score > savedScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        SetPageStage(PageState.GameOver);
    }
    void OnPlayerScored()
    {
        score++;
        scoreText.text = score.ToString();
    }
    void SetPageStage(PageState state)
    {
        
        switch  (state)
        {
            case PageState.None:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countdownPage.SetActive(false);
                break;
            case PageState.Start:         
                startPage.SetActive(true);
                gameOverPage.SetActive(false);
                countdownPage.SetActive(false);        
                break;
            case PageState.GameOver:       
                startPage.SetActive(false);
                gameOverPage.SetActive(true);
                countdownPage.SetActive(false);


                break;
            case PageState.Countdown:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countdownPage.SetActive(true);
                break;
        }
    }

    public void ConfirmGameOver()
    {
        //activated when replay button is hit
        OnGameOverConfirmed(); //event sent to TapController
        scoreText.text = "0";

        SetPageStage(PageState.Start);
    }

    public void StartGame()
    {
        //activated when play button is hit
        SetPageStage(PageState.Countdown);
    }
}
