﻿using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    dreamloLeaderBoard leaderBoard;
    UIManager ui;
    float scoreCounter;
    int score;
    bool isGameEnd;
    string PlayerName = "default";

    private void OnEnable()
    {
        EventManager.OnGameEnd += GameEnd;
    }
    private void OnDisable()
    {
        EventManager.OnGameEnd -= GameEnd;
    }

    private void Awake()
    {
        instance = this;
        leaderBoard = FindObjectOfType<dreamloLeaderBoard>();
        ui = FindObjectOfType<UIManager>();
        isGameEnd = true;
    }

    private void Update()
    {
        if (!isGameEnd)
        {
            scoreCounter += Time.deltaTime * 10;
            score = (int)Mathf.Round(scoreCounter);
            ui.UpdateScore(score);
        }
    }

    public void SetPlayerName(string name)
    {
        PlayerName = name;
    }

    public void GameStart()
    {
        if (EventManager.OnGameStart != null)
            EventManager.OnGameStart();
        scoreCounter = 0;
        score = 0;
        isGameEnd = false;
        Debug.Log("Game Iniziato");
    }

    private void GameEnd()
    {
        EventSystem.current.SetSelectedGameObject(null);
        isGameEnd = true;
        leaderBoard.AddScore(PlayerName, score);
        Debug.Log("Game Finito");
    }
}
