using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameGUIManager : Singleton<GameGUIManager>
{
    public GameObject homeGui;
    public GameObject gameGui;

    public Dialog gameDialog;
    public Dialog pauseDialog;
    public Dialog helpDialog;
    public Dialog bestscoreDialog;

    public GameObject bestScoreBtn;
    public GameObject playGameBtn;
    public GameObject helpBtn;

    public Image fireRateFilled;
    public Text timerText;
    public Text killedCountingText;
    public Text Score;

    //Giao dien hien tai
    Dialog m_curDialog;

    public Dialog CurDialog { get => m_curDialog; set => m_curDialog = value; }

    public override void Awake()
    {
        MakeSingleton(false);
    }

    public override void Start()
    {
        
    }

    public void ShowGameGui(bool isShow)
    {
        if (gameGui)
            gameGui.SetActive(isShow);

        if (homeGui)
            homeGui.SetActive(!isShow);
    }

    public void UpdateTimer(string time)
    {
        if (timerText)
            timerText.text = time;
    }

    public void UpdateKilledCounting(int killed)
    {
        if (killedCountingText)
            killedCountingText.text = "x" + killed.ToString();
    }

    public void ScorePlay(int score)
    {
        if (Score)
            Score.text = "SCORE: " + score.ToString();
    }

    public void UpdateFireRate(float rate)
    {
        if (fireRateFilled)
            fireRateFilled.fillAmount = rate;
    }

    public void SetHiddenBtn()
    {
        bestScoreBtn.SetActive(false);
        playGameBtn.SetActive(false);
        helpBtn.SetActive(false);
    }

    public void SetActiveBtn()
    {
        bestScoreBtn.SetActive(true);
        playGameBtn.SetActive(true);
        helpBtn.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;

        if (pauseDialog)
        {
            pauseDialog.Show(true);
            m_curDialog = pauseDialog;
        }
    }

    public void HelpGame()
    {
        if (helpDialog)
        {
            helpDialog.Show(true);
            m_curDialog = helpDialog;
            SetHiddenBtn();
        }
    }

    public void BestScoreGame()
    {
        if (bestscoreDialog)
        {
            bestscoreDialog.Show(true);
            bestscoreDialog.UpdateDialog("GAME PAUSE", "BEST SCORE : " + Prefs.bestScore, "");
            m_curDialog = bestscoreDialog;
            SetHiddenBtn();
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;

        if (m_curDialog)
        {
            m_curDialog.Show(false);
            SetActiveBtn();
        }
    }

    public void BackToHome()
    {
        ResumeGame();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Replay()
    {
        if (m_curDialog)
            m_curDialog.Show(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void ExitGame()
    {
        ResumeGame();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Application.Quit();
    }
}
