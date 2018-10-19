﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float JumpForce;
    [SerializeField]
    float JumpDuration;
    float StartJumpDuration;

    Animator anim;
    bool CanJump;
    Vector2 JumpPosition;
    Vector2 startPosition;

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

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {
        CanJump = false;
        JumpPosition = new Vector2(transform.position.x, transform.position.y + JumpForce);
        startPosition = transform.position;
        StartJumpDuration = JumpDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanJump)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                StartCoroutine(JumpCoroutine());
            }
        }
    }

    IEnumerator JumpCoroutine()
    {
        CanJump = false;
        anim.SetTrigger("Jump");
        while (Vector2.Distance(transform.position, JumpPosition) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, JumpPosition, Time.deltaTime * JumpDuration);
            yield return null;
        }


        anim.SetTrigger("EndJump");
        while (Vector2.Distance(transform.position, startPosition) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPosition, Time.deltaTime * JumpDuration);
            yield return null;
        }
        CanJump = true;
    }

    private void GameEnd()
    {
        anim.enabled = false;
        CanJump = false;
        StopAllCoroutines();
    }

    private void GameStart()
    {
        anim.enabled = true;
        JumpDuration = StartJumpDuration;
        anim.SetTrigger("Start");
        transform.position = startPosition;
        CanJump = true;
    }

    public void ReduceJumpDuration()
    {
        if (JumpDuration < 20)
        {
            JumpDuration += 0.15f;
        }
    }
}
