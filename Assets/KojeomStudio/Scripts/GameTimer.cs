using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;

public class GameTime
{
    Stopwatch stopWatch;
    TimeSpan timeSpan;

    public void Init()
    {
        stopWatch = new Stopwatch();
    }
    public void StartTime()
    {
        stopWatch.Start();
    }

    public string GetTime()
    {
        timeSpan = stopWatch.Elapsed;
        string elapsedTime = string.Format("{0:00}:{1:00}.{2:00}",
            timeSpan.Minutes, timeSpan.Seconds,
            timeSpan.Milliseconds / 10);

        return elapsedTime;
    }

    public int GetSeconds()
    {
        timeSpan = stopWatch.Elapsed;
        return timeSpan.Seconds;
    }
    public int GetMinutes()
    {
        timeSpan = stopWatch.Elapsed;
        return timeSpan.Minutes;
    }
    public int GetHours()
    {
        timeSpan = stopWatch.Elapsed;
        return timeSpan.Hours;
    }

    public void StopTime()
    {
        stopWatch.Stop();
    }
}

public class GameTimer : MonoBehaviour
{

    [SerializeField]
    private InGameUIManager inGameUIMgr;
    private UILabel lbl_gameTime;
    private GameTime gameTime;
    IEnumerator gameTimeCoRoutine;

    public void InitGameTimer()
    {
        lbl_gameTime = inGameUIMgr.GetGameTimerLbl();
        gameTime = new GameTime();
        gameTime.Init();
        gameTimeCoRoutine = TimeProcess();
    }

    public void StartGameTimer()
    {
        ShowGameTimer();
        gameTime.StartTime();
        StartCoroutine(gameTimeCoRoutine);
    }

    public int GetMinutes()
    {
        return gameTime.GetMinutes();
    }
    public int GetHours()
    {
        return gameTime.GetHours();
    }
    public int GetSeconds()
    {
        return gameTime.GetSeconds();
    }

    public void StopGameTimer()
    {
        gameTime.StopTime();
        StopCoroutine(gameTimeCoRoutine);
    }
    private void ShowGameTimer()
    {
        lbl_gameTime.gameObject.SetActive(true);
    }
    private void HideGameTimer()
    {
        lbl_gameTime.gameObject.SetActive(false);
    }

    IEnumerator TimeProcess()
    {
        while (true)
        {
            lbl_gameTime.text = gameTime.GetTime();
            yield return null;
        }
    }
}
