using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour {
    [SerializeField]
    float backgoundDislpace = 40.90001f;
    [SerializeField]
    float backgroundstartspeed;
    float backgroundspeed;
    public BackgroundController bg1;
    public BackgroundController bg2;
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

    // Use this for initialization
    void Start () {
        backgroundspeed = 0;
        currentBG = bg1;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(-backgroundspeed, 0, 0);
	}

    public void RespawnBG(BackgroundController bg)
    {
        if (bg != currentBG)
            return;
        BackgroundController other = (bg == bg1) ? bg2 : bg1;
        bg.transform.position = new Vector3(other.transform.position.x + backgoundDislpace, other.transform.position.y, other.transform.position.z);
        currentBG = other;
    }

    private void GameEnd()
    {
        backgroundspeed = 0;
    }

    private void GameStart()
    {
        backgroundspeed = backgroundstartspeed;
    }
}
