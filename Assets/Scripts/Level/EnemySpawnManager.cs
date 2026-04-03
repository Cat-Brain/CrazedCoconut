using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager instance;

    public List<Enemy> spawnableEnemies;
    public float spawnDelay, spawnDelayPerEnemy;

    public static void TryFindInstance()
    {
        if (!instance)
            instance = FindAnyObjectByType<EnemySpawnManager>();
    }

    public static void SpawnWave(EnemySpawner spawner)
    {
        TryFindInstance();

        List<Enemy> currentEnemies = new();
        foreach (Enemy potentialEnemy in instance.spawnableEnemies)
            if (spawner.progression >= potentialEnemy.spawnProgressionStart &&
                spawner.progression <= potentialEnemy.spawnProgressionEnd)
                currentEnemies.Add(potentialEnemy);

        int remainingPoints = spawner.difficultyPoints;
        while (remainingPoints > 0)
        {
            Enemy chosenEnemy = currentEnemies[Random.Range(0, currentEnemies.Count)];
            int spawnCount = Random.Range(1,
                Mathf.CeilToInt((float)remainingPoints / chosenEnemy.spawnPoints));

            for (int i = 0; i < spawnCount; i++)
                spawner.currentEnemies.Add(chosenEnemy.DelayedSpawn(spawner.GetEnemySpawnPos(),
                    instance.spawnDelay + spawner.currentEnemies.Count * instance.spawnDelayPerEnemy,
                    spawner));

            remainingPoints -= spawnCount * chosenEnemy.spawnPoints;
        }
    }
}
