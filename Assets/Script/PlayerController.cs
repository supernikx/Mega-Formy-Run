using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float JumpForce;
    [SerializeField]
    float JumpDuration;

    bool CanJump;
    Vector2 JumpPosition;

    // Use this for initialization
    void Start()
    {
        CanJump = true;
        JumpPosition = new Vector2(transform.position.x, transform.position.y + JumpForce);
    }

    // Update is called once per frame
    void Update()
    {
        if (CanJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(JumpCoroutine());                
            }
        }
    }

    IEnumerator JumpCoroutine()
    {
        CanJump = false;
        Vector2 startPosition = transform.position;
        while (Vector2.Distance(transform.position, JumpPosition) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, JumpPosition, Time.deltaTime * JumpDuration);
            yield return null;
        }
        while (Vector2.Distance(transform.position, startPosition) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPosition, Time.deltaTime * JumpDuration);
            yield return null;
        }
        CanJump = true;
    }
}
