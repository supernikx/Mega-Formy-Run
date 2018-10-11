using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour {

    public GameObject StartButton;
    public GameObject ScoreText;
    public TextMeshProUGUI ScoreCounter;
    public GameObject GameOverText;

    private void OnEnable()
    {
        EventManager.OnGameEnd += GameEnd;
        EventManager.OnGameStart += GameStart;
    }
    private void OnDisable()
    {
        EventManager.OnGameEnd -= GameEnd;
        EventManager.OnGameStart -= GameStart;
    }

    // Use this for initialization
    void Start () {
        ScoreText.SetActive(false);
        ScoreCounter.gameObject.SetActive(false);
        GameOverText.SetActive(false);
        StartButton.SetActive(true);
    }

    public void UpdateScore(float _score)
    {        
        ScoreCounter.text = Mathf.Round(_score).ToString();
    }

    private void GameEnd()
    {
        ScoreText.SetActive(true);
        ScoreCounter.gameObject.SetActive(true);
        GameOverText.SetActive(true);
        StartButton.SetActive(true);
    }

    private void GameStart()
    {
        StartButton.SetActive(false);
        GameOverText.SetActive(false);
        ScoreText.SetActive(true);
        ScoreCounter.gameObject.SetActive(true);
        ScoreCounter.text = "0";
    }
}
