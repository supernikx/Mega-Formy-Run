using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IPoolObject
{

    private State _currentState;
    public State CurrentState
    {
        get
        {
            return _currentState;
        }
        set
        {
            _currentState = value;
        }
    }

    public event PoolManagerEvets.Events OnObjectSpawn;
    public event PoolManagerEvets.Events OnObjectDestroy;

    float screenWidth;
    float MovementSpeed;
    bool CanMove;

    private void Start()
    {
        screenWidth = (Camera.main.orthographicSize * Camera.main.aspect) + transform.localScale.x;
    }

    public void Init(float _movementSpeed)
    {
        MovementSpeed = _movementSpeed;
        CanMove = true;
        if (OnObjectSpawn != null)
            OnObjectSpawn(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CanMove)
        {
            transform.position += new Vector3(-MovementSpeed, 0, 0);
            if (transform.position.x < -screenWidth)
            {
                CanMove = false;
                if (OnObjectDestroy != null)
                    OnObjectDestroy(this);
            }
        }
    }
}
