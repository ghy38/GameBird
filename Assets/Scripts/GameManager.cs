using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Bird[] birdPrefabs;
    // Thoi gian xuat hien 1 con chim
    public float spawnTime;
    // Thoi gian choi
    public int timeLimit;

    int m_curTimeLimit;
    // So luong chim bi ban
    int m_birdKilled;
    // Diem so
    int m_score;

    bool m_isGameover;

    public bool IsGameover { get => m_isGameover; set => m_isGameover = value; }
    public int BirdKilled { get => m_birdKilled; set => m_birdKilled = value; }
    public int Score { get => m_score; set => m_score = value; }

    public override void Awake()
    {
        MakeSingleton(false);

        //Dat thoi gian choi cho game
        m_curTimeLimit = timeLimit;
    }

    public override void Start()
    {
        GameGUIManager.Ins.ShowGameGui(false);
        GameGUIManager.Ins.UpdateKilledCounting(m_birdKilled);
        GameGUIManager.Ins.ScorePlay(m_score);
    }

    public void PlayGame()
    {
        StartCoroutine(GameSpawn());

        StartCoroutine(TimeCountDown());

        GameGUIManager.Ins.ShowGameGui(true);
    }

    IEnumerator TimeCountDown()
    {
        // Giam thoi gian choi
        while(m_curTimeLimit > 0)
        {
            yield return new WaitForSeconds(1f);
            m_curTimeLimit--;

            if(m_curTimeLimit <= 0)
            {
                m_isGameover = true;

                //Cap nhat va hien thi giao dien ket thuc game khi het thoi gian choi
                //Neu diem so cao hon lan truoc thi hien thi diem so moi
                if (m_score > Prefs.bestScore)
                {
                    GameGUIManager.Ins.gameDialog.UpdateDialog("NEW BEST", "BEST SCORE : " + m_score, "YOUR SCORE : " + m_score);
                }else if(m_score < Prefs.bestScore)
                {
                    GameGUIManager.Ins.gameDialog.UpdateDialog("YOUR BEST", "BEST SCORE : " + Prefs.bestScore, "YOUR SCORE : " + m_score);
                }

                //Luu lai diem so
                Prefs.bestScore = m_score;

                GameGUIManager.Ins.gameDialog.Show(true);
                GameGUIManager.Ins.CurDialog = GameGUIManager.Ins.gameDialog;
            }
            GameGUIManager.Ins.pauseDialog.UpdateDialog("GAME PAUSE", "BEST SCORE : " + Prefs.bestScore, "YOUR SCORE : " + m_score);
            //Cap nhat thoi gian hien thi len Texttimer
            GameGUIManager.Ins.UpdateTimer(IntToTime(m_curTimeLimit));
        }
    }

    IEnumerator GameSpawn()
    {
        // Neu game chua ket thuc (het thoi gian choi) thi tiep tuc vong lap
        while (!IsGameover)
        {
            //Tao ra chim trong mot khoang thoi gian la spawnTime
            SpawnBird();
            yield return new WaitForSeconds(spawnTime);
        }
    }

    void SpawnBird()
    {
        // Vi tri xuat hien chim
        Vector3 spawnPos = Vector3.zero;

        // Ti le phan tram cua vi tri ma chim xuat hien
        float randCheck = Random.Range(0f, 1f);

        if(randCheck >= 0.5f)
        {
            spawnPos = new Vector3(12, Random.Range(-2f, 4f), 0);
        }else
        {
            spawnPos = new Vector3(-12, Random.Range(-2f, 4f), 0);
        }

        if(birdPrefabs != null && birdPrefabs.Length > 0)
        {
            // Tao ngau nhien cac loai chim se xuat hien
            int randIdx = Random.Range(0, birdPrefabs.Length);
            
            if(birdPrefabs[randIdx] != null)
            {
                Bird birdClone = Instantiate(birdPrefabs[randIdx], spawnPos, Quaternion.identity);
            }
        }
    }

    // Chuyen thoi gian (kieu int) sang chuoi de hien thi len textTimer
    string IntToTime(int time)
    {
        float minutes = Mathf.Floor(time / 60);
        float seconds = Mathf.RoundToInt(time % 60);

        return minutes.ToString("00") + " : " + seconds.ToString("00");
    }
}
