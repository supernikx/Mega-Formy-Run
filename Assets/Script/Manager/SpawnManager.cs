using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [SerializeField]
    float SpawnTime;
    [SerializeField]
    Transform SpawnPosition;
    [SerializeField]
    float ObstacleSpeed;

    PoolManager pool;
    float Timer;

    private void Awake()
    {
        pool = GetComponent<PoolManager>();
    }

    private void Update()
    {
        if (Timer > SpawnTime)
        {
            Timer = 0;
            Obstacle PooledObstacle = pool.GetPooledObject(ObjectTypes.Obstalce).GetComponent<Obstacle>();
            PooledObstacle.transform.position = SpawnPosition.position;
            PooledObstacle.Init(ObstacleSpeed);
        }
        Timer += Time.deltaTime;
    }

}
