using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour {

    public GameObject StartButton;
    public TextMeshProUGUI ScoreCounter;
    public GameObject GameOverText;
    public GameObject Title;

    IEnumerator StartButtonCoroutine;

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
        Title.SetActive(true);
        GameOverText.SetActive(false);
        StartButtonCoroutine = StartLampeggio();
        StartCoroutine(StartButtonCoroutine);
    }

    public void UpdateScore(int _score)
    {
        string score = _score.ToString();
        string displayscore = null;

        for (int i = 0; i < (8 - score.Length); i++)
        {
            displayscore += "0";
        }

        ScoreCounter.text = displayscore + score;
    }

    private void GameEnd()
    {        
        GameOverText.SetActive(true);
        StartButton.SetActive(true);
        StartCoroutine(StartButtonCoroutine);
    }

    private void GameStart()
    {
        Title.SetActive(false);
        StopCoroutine(StartButtonCoroutine);
        StartButton.SetActive(false);
        GameOverText.SetActive(false);
        ScoreCounter.text = "000000000";
    }

    IEnumerator StartLampeggio()
    {
        Image i = StartButton.GetComponent<Image>();
        Color c = i.color;
        while (true)
        {
            i.color = new Color(c.r, c.g, c.b, 1);
            yield return new WaitForSeconds(1f);
            i.color = new Color(c.r, c.g, c.b, 0);
            yield return new WaitForSeconds(0.4f);
        }
    }
}
