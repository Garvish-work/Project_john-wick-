using UnityEngine;
using System.Collections.Generic;

public class EnemySpawnSystem : MonoBehaviour
{
    public static EnemySpawnSystem instance;

    [SerializeField] private ScoreConfig scoreConfig;
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private int spawnedEnemies = 0;
    [SerializeField] private int enemyToSpawn = 30;
    [SerializeField] private int currentKillCount = 0;
    [SerializeField] private int maxKillCount = 3;

    [SerializeField] private List<GameObject> activeEnemiesList = new List<GameObject>();
    [SerializeField] private List<GameObject> deactiveEnemiesList = new List<GameObject>();

    private void Awake()
    {
        instance = this;
        scoreConfig.zombieRemaining = enemyToSpawn;
        SpawnZombie();
    }

    public void DeactiveEnemies(GameObject enemies)
    {
        enemies.SetActive(false);
        activeEnemiesList.Remove(enemies);
        deactiveEnemiesList.Add(enemies);
    }

    private void OnEnable()
    {
        ScoreActions.EnemyDied += OnEnemyKilled;
    }

    private void OnDisable()
    {
        ScoreActions.EnemyDied -= OnEnemyKilled;
    }

    private void OnEnemyKilled()
    {
        if (spawnedEnemies == enemyToSpawn)
        {
            return;
        }

        currentKillCount++;
        if (currentKillCount == maxKillCount)
        {
            SpawnZombie();
            currentKillCount = 0;
        }
    }

    private void SpawnZombie()
    {
        if (activeEnemiesList.Count > 9) return;

        int spawnCount = Random.Range(3, 5);
        if (deactiveEnemiesList.Count < spawnCount) return;
        for (int i = 0; i < spawnCount; i++)
        {
            if (spawnedEnemies == enemyToSpawn) return;

            int spawnIndex = Random.Range(0, spawnPoints.Length);

            GameObject enemy = deactiveEnemiesList[0].gameObject;

            enemy.transform.position = spawnPoints[spawnIndex].position; 
            enemy.SetActive(true);

            deactiveEnemiesList.Remove(enemy);
            activeEnemiesList.Add(enemy);

            spawnedEnemies++;
        }
    }
}
