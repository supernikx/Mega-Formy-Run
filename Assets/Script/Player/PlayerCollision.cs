﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (EventManager.OnGameEnd != null)
            EventManager.OnGameEnd();
    }
}
