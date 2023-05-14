using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIManager))]
[RequireComponent(typeof(HighScoreSystem))]
public class GameManager : MonoBehaviour
{
    // DESIGN PATTERN: SINGLETON
    public static GameManager Instance { get; private set; }
    public UIManager UIManager { get; private set; }

    public HighScoreSystem HighScoreSystem { get; private set; }

    private static float secondsSinceStart = 0;
    private static float timeElapsed = 0f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        UIManager = GetComponent<UIManager>();
        HighScoreSystem = GetComponent<HighScoreSystem>();
    }

    void Update()
    {
        secondsSinceStart += Time.deltaTime;
        Instance.UIManager.UpdateTimeUI(secondsSinceStart);
    }

    public static string GetTimeElapsedText()
    {
        return secondsSinceStart.ToString("F2");
    }

    public static void IncrementTime(float value)
    {
        timeElapsed += value;
        Instance.UIManager.UpdateTimeUI(timeElapsed);
        Debug.Log("Time Taken: " + timeElapsed);
    }

    public static void ResetGame()
    {
        ResetTime();
        secondsSinceStart = 0f;
    }

    private static void ResetTime()
    {
        timeElapsed = 0f;
        Instance.UIManager.UpdateTimeUI(timeElapsed);
        Debug.Log("Time Taken: " + timeElapsed);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        Instance.UIManager.ActivateEndGame(timeElapsed);
        MenuController.IsGamePaused = true;
        HighScoreSystem.CheckHighScore("anon", (int)timeElapsed);
    }
}
