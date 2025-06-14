using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }
    [SerializeField] private GameObject creepPrefab;
    [SerializeField] private GameObject demonPrefab;
    [SerializeField] private GameObject ballDemonPrefab;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private float spawnCheckRadius = 0.5f;   // adjust to your enemy size
    [SerializeField] private int maxSpawnAttempts = 10;       // how many times to try
    private int enemiesSpawned = 0;
    public int totalEnemiesToSpawn = 0;
    private int currentActiveEnemies = 0;

    private LevelData currentLevelData;
    private bool shouldSpawn = false;
    public int killCount = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        EventBus.OnEnemyKilled += OnEnemyKilled;
    }

    void OnEnemyKilled()
    {
        killCount++;
        currentActiveEnemies = Mathf.Max(0, currentActiveEnemies - 1);
        if (currentActiveEnemies == 0)
        {
            shouldSpawn = true;
        }
        if (killCount >= totalEnemiesToSpawn)
        {
            EventBus.TriggerOnLevelCleared();
        }

    }

    public void StartSpawner(LevelData data)
    {
        currentLevelData = data;
        totalEnemiesToSpawn = 0;

        foreach (var enemy in data.enemyData)
            totalEnemiesToSpawn += enemy.enemyCount;
        shouldSpawn = true;
        killCount = 0;
        StartCoroutine(SpawnEnemiesRoutine());
    }

    private GameObject GetEnemyPrefab(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.Creep: return creepPrefab;
            case EnemyType.Demon: return demonPrefab;
            case EnemyType.BallDemon: return ballDemonPrefab;
            default:
                Debug.LogError($"Unknown enemy type: {enemyType}");
                return null;
        }
    }

    private IEnumerator SpawnEnemiesRoutine()
    {
        foreach (var enemy in currentLevelData.enemyData)
        {
            for (int i = 0; i < enemy.enemyCount; i++)
            {
                // Pause here until there's room to spawn (i.e. active < limit)
                yield return new WaitUntil(
                    () => currentActiveEnemies < currentLevelData.concurrentEnemyCount
                );

                // Spawn one
                var prefab = GetEnemyPrefab(enemy.enemyType);
                if (prefab != null)
                {
                    int idx = Random.Range(0, spawnPoints.Count);

                    Transform sp = spawnPoints[idx];
                    Vector3 spawnPos = GetFreeSpawnPosition(sp);
                    GameObject enemyObject = Instantiate(prefab, spawnPos, Quaternion.identity);
                    SetEnemyStats(enemyObject, enemy);
                    enemiesSpawned++;
                    currentActiveEnemies++;
                }

                // Wait your configured delay
                yield return new WaitForSeconds(currentLevelData.timeBetweenSpawns);
            }
        }
    }
    private Vector3 GetFreeSpawnPosition(Transform spawnPoint)
    {
        for (int attempt = 0; attempt < maxSpawnAttempts; attempt++)
        {
            // random offset in a unit circle on XZ plane
            Vector2 offset2D = Random.insideUnitCircle;
            Vector3 candidatePos = spawnPoint.position
                                   + new Vector3(offset2D.x, 0f, offset2D.y);

            // check if anything's within spawnCheckRadius
            if (Physics.OverlapSphere(candidatePos, spawnCheckRadius).Length == 0)
            {
                return candidatePos;  // free to spawn here
            }
        }
        // fallback
        return spawnPoint.position;
    }



    void SetEnemyStats(GameObject enemy, EnemyData data)
    {
        EnemyStats stats = enemy.GetComponentInChildren<EnemyStats>();
        stats.attackSpeed = data.movementMultiplier;
        stats.maxHealth = data.maxHealth;
        stats.invincibilityDuration = data.invincibilityDuration;
        stats.baseDamage = data.baseDamage;
        stats.defense = data.defense;
        StartCoroutine(SetActiveWithDelay(enemy));
    }

    IEnumerator SetActiveWithDelay(GameObject obj, float delay = 3f)
    {
        EnemyBaseStateMachine machine = obj.GetComponentInChildren<EnemyBaseStateMachine>();
        if (machine != null)
        {

            yield return new WaitForSeconds(delay);
            machine.agent.enabled = true;
            machine.enabled = true;
        }
    }
    void OnDestroy()
    {
        EventBus.OnEnemyKilled -= OnEnemyKilled;

    }
}

