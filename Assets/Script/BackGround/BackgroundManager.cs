using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField]
    List<BackgroundController> Backgrounds = new List<BackgroundController>();
    List<Transform> BackgroundsStartPositions = new List<Transform>();
    [SerializeField]
    float backgroundstartspeed;
    float _backgroundspeed;
    public float BackgroundSpeed {
        get
        {
            return _backgroundspeed;
        }
        set
        {
            _backgroundspeed = value;
            SetBackgroundspeed(_backgroundspeed);
        }
    }
    Vector3 backgroundStartPos;
    Vector3 backgroundResetPosition;

    BackgroundController currentBG;

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

    void Start()
    {
        BackgroundSpeed = 0;
        currentBG = Backgrounds[0];
        backgroundStartPos = Backgrounds[Backgrounds.Count - 1].transform.position;
        backgroundStartPos.x -= 0.15f;
        backgroundResetPosition = backgroundStartPos;
        foreach (BackgroundController b in Backgrounds)
        {
            BackgroundsStartPositions.Add(b.transform);
        }
    }

    private void GameEnd()
    {
        BackgroundSpeed = 0;
    }

    private void GameStart()
    {
        for (int i = 0; i < Backgrounds.Count; i++)
        {
            Backgrounds[i].transform.position = BackgroundsStartPositions[i].position;
        }

        backgroundResetPosition = backgroundStartPos;
        BackgroundSpeed = backgroundstartspeed;
    }

    public void RespawnBG(BackgroundController bg)
    {
        if (bg != currentBG)
            return;

        BackgroundController other = bg;
        for (int i = 0; i < Backgrounds.Count; i++)
        {
            if (bg == Backgrounds[i])
            {
                if ((i + 1) <= (Backgrounds.Count - 1))
                {
                    other = Backgrounds[i + 1];
                }
                else
                {
                    other = Backgrounds[0];
                }
                break;
            }
        }

        bg.transform.position = new Vector3(backgroundResetPosition.x, backgroundResetPosition.y, backgroundResetPosition.z);
        currentBG = other;
    }

    private void SetBackgroundspeed(float _Speed)
    {
        foreach (BackgroundController b in Backgrounds)
        {
            b.SetSpeed(_Speed);
        }
    }

    public void IncreaseBackgroundSpeed()
    {
        if (BackgroundSpeed < 0.5)
        {
            BackgroundSpeed += 0.01f;
        }
    }
}
