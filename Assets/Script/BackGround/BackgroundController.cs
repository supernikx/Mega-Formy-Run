using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{

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

    // Update is called once per frame
    void Update()
    {
        if (CheckScreenPosition())
            bgManager.RespawnBG(this);
    }
}
