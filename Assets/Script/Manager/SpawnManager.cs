using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    float SpawnTime;
    [SerializeField]
    Transform SpawnPointDown;
    [SerializeField]
    Transform SpawnPointUp;
    [SerializeField]
    float StartObstacleSpeed;
    float ObstacleSpeed;

    BackgroundManager bg;
    PlayerController player;
    PoolManager pool;
    float Timer;
    int UpSpawnWave;
    int UpSpawnWaveCounter;
    int WaveCounter;
    bool SpawnObstacles;

    float SpawnTimeRateMin;
    float SpawnTimeRateMax;

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
        bg = FindObjectOfType<BackgroundManager>();
        player = FindObjectOfType<PlayerController>();
        pool = GetComponent<PoolManager>();
        ObstacleSpeed = StartObstacleSpeed;
        SpawnObstacles = false;
        UpSpawnWave = Random.Range(0, 3);
        SpawnTimeRateMin = 1.5f;
        SpawnTimeRateMax = 3f;
    }

    private void FixedUpdate()
    {
        if (SpawnObstacles)
        {
            if (Timer > SpawnTime)
            {
                SpawnObstacle();
            }
            Timer += Time.deltaTime;
        }
    }

    void SpawnObstacle()
    {
        Timer = 0;
        UpSpawnWaveCounter++;
        WaveCounter++;
        bg.IncreaseBackgroundSpeed();
        player.ReduceJumpDuration();

        SpawnTime = Random.Range(SpawnTimeRateMin, SpawnTimeRateMax);
        Obstacle PooledObstacle;

        if (ObstacleSpeed < 1f)
        {
            ObstacleSpeed += 0.01f;
        }

        if (SpawnTimeRateMin >= 0.5f && SpawnTimeRateMax >= 1f)
        {
            SpawnTimeRateMin -= 0.0125f;
            SpawnTimeRateMax -= 0.0375f;
        }

        if (UpSpawnWaveCounter > UpSpawnWave)
        {
            UpSpawnWaveCounter = 0;
            UpSpawnWave = Random.Range(0, 3);
            PooledObstacle = pool.GetPooledObject(ObjectTypes.FlyObstacle).GetComponent<Obstacle>();
            PooledObstacle.transform.position = SpawnPointUp.position;
        }
        else
        {
            PooledObstacle = pool.GetPooledObject(ObjectTypes.FireObstacle).GetComponent<Obstacle>();
            PooledObstacle.transform.position = SpawnPointDown.position;
        }

        PooledObstacle.Init(ObstacleSpeed);
    }

    private void GameEnd()
    {
        foreach (GameObject g in pool.GetPooledObjectList(ObjectTypes.FireObstacle))
        {
            Obstacle gobstacle = g.GetComponent<Obstacle>();
            IPoolObject gpool = gobstacle as IPoolObject;
            if (gpool.CurrentState == State.InUse)
                gobstacle.StopMoving();
        }

        foreach (GameObject g in pool.GetPooledObjectList(ObjectTypes.FlyObstacle))
        {
            Obstacle gobstacle = g.GetComponent<Obstacle>();
            IPoolObject gpool = gobstacle as IPoolObject;
            if (gpool.CurrentState == State.InUse)
                gobstacle.StopMoving();
        }

        SpawnObstacles = false;
    }

    private void GameStart()
    {
        foreach (GameObject g in pool.GetPooledObjectList(ObjectTypes.FireObstacle))
        {
            g.GetComponent<Obstacle>().StartGame();
        }

        foreach (GameObject g in pool.GetPooledObjectList(ObjectTypes.FlyObstacle))
        {
            g.GetComponent<Obstacle>().StartGame();
        }

        SpawnTimeRateMin = 1.5f;
        SpawnTimeRateMax = 3f;
        UpSpawnWave = Random.Range(0, 3);
        Timer = 0;
        SpawnTime = Random.Range(SpawnTimeRateMin, SpawnTimeRateMax);
        UpSpawnWaveCounter = 0;
        WaveCounter = 0;
        ObstacleSpeed = StartObstacleSpeed;

        SpawnObstacles = true;
    }
}
