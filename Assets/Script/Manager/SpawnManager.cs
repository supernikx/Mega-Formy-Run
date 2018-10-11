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

    PoolManager pool;
    float Timer;
    int UpSpawnWave;
    int UpSpawnWaveCounter;
    int WaveCounter;
    bool SpawnObstacles;

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
        pool = GetComponent<PoolManager>();
        ObstacleSpeed = StartObstacleSpeed;
        SpawnObstacles = false;
        UpSpawnWave = Random.Range(0, 3);
    }

    private void Update()
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
        SpawnTime = Random.Range(0.5f, 2);
        Obstacle PooledObstacle = pool.GetPooledObject(ObjectTypes.Obstalce).GetComponent<Obstacle>();

        if (WaveCounter % 2 == 0 && ObstacleSpeed  < 0.8f)
            ObstacleSpeed += 0.02f;

        if (UpSpawnWaveCounter > UpSpawnWave)
        {
            if (Random.Range(0, 2) == 0)
            {
                UpSpawnWaveCounter = 0;
                UpSpawnWave = Random.Range(0, 3);
                PooledObstacle.transform.position = SpawnPointUp.position;
            }
            else
            {
                PooledObstacle.transform.position = SpawnPointDown.position;
            }
        }
        else
        {
            PooledObstacle.transform.position = SpawnPointDown.position;
        }

        PooledObstacle.Init(ObstacleSpeed);
    }

    private void GameEnd()
    {
        foreach (GameObject g in pool.GetPooledObjectList(ObjectTypes.Obstalce))
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
        foreach (GameObject g in pool.GetPooledObjectList(ObjectTypes.Obstalce))
        {
            g.GetComponent<Obstacle>().StartGame();
        }

        UpSpawnWave = Random.Range(0, 3);
        Timer = 0;
        SpawnTime = Random.Range(0.5f, 2);
        UpSpawnWaveCounter = 0;
        WaveCounter = 0;
        ObstacleSpeed = StartObstacleSpeed;

        SpawnObstacles = true;
    }
}
