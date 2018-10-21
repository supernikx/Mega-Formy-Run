using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField]
    BackgroundController bg1;
    [SerializeField]
    BackgroundController bg2;
    [SerializeField]
    float backgroundstartspeed;
    float _backgroundspeed;
    public float BackgroundSpeed
    {
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
    [SerializeField]
    float backgroundOffset;

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
        currentBG = bg1;
    }

    private void GameEnd()
    {
        BackgroundSpeed = 0;
    }

    private void GameStart()
    {
        BackgroundSpeed = backgroundstartspeed;
    }

    public void RespawnBG(BackgroundController bg)
    {
        if (bg != currentBG)
            return;

        BackgroundController other = (bg == bg1) ? bg2 : bg1;

        bg.transform.position = new Vector3(other.transform.position.x + backgroundOffset, other.transform.position.y, other.transform.position.z);
        currentBG = other;
    }

    private void SetBackgroundspeed(float _Speed)
    {
        bg1.SetSpeed(_Speed);
        bg2.SetSpeed(_Speed);
    }

    public void IncreaseBackgroundSpeed()
    {
        if (BackgroundSpeed < 0.5)
        {
            BackgroundSpeed += 0.01f;
        }
    }
}
