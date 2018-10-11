using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    UIManager ui;
    float score;
    bool isGameEnd;

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
        ui = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        if (!isGameEnd)
        {
            score += Time.deltaTime * 10;
            ui.UpdateScore(score);
        }
    }

    public void GameStart()
    {
        if (EventManager.OnGameStart != null)
            EventManager.OnGameStart();
        score = 0;
        isGameEnd = false;
        Debug.Log("Game Iniziato");
    }

    private void GameEnd()
    {
        isGameEnd = true;
        Debug.Log("Game Finito");
    }
}
