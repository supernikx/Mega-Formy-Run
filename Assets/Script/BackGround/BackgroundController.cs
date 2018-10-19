using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    float Speed;
    float screenWidth;
    Bounds bound;
    BackgroundManager bgManager;

    private bool CheckScreenPosition()
    {
        if (bound.extents.x + transform.position.x < -screenWidth)
            return true;
        return false;
    }

    // Use this for initialization
    void Start()
    {
        bgManager = FindObjectOfType<BackgroundManager>();
        screenWidth = (Camera.main.orthographicSize * Camera.main.aspect);
        bound = GetComponent<SpriteRenderer>().bounds;
    }

    public void SetSpeed (float _speed)
    {
        Speed = _speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(-Speed, 0, 0);
        if (CheckScreenPosition())
            bgManager.RespawnBG(this);
    }
}
