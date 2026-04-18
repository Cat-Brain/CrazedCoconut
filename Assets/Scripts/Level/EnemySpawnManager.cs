using ClownLib;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    private static EnemySpawnManager instance;
    public static EnemySpawnManager Instance
    { get {
            if (instance == null)
                instance = FindAnyObjectByType<EnemySpawnManager>();
            return instance;
    } }

    public List<Enemy> spawnableEnemies;
    public float spawnDelay, spawnDelayPerEnemy, spawnRadius;

    public List<(Enemy type, int count)> toSpawnEnemies = new();
    public List<Enemy> currentEnemies = new();

    public int RemainingEnemiesToSpawn()
    {
        int result = 0;
        foreach ((Enemy _, int count) in toSpawnEnemies)
            result += count;
        return result;
    }

    public void SpawnWave()
    {
        int totalSpawned = 0, remaining;
        while ((remaining = RemainingEnemiesToSpawn()) > 0)
        {
            int randomIndex = Random.Range(0, remaining);
            Enemy chosenEnemy = null;
            for (int i = 0; i < toSpawnEnemies.Count; i++)
            {
                if ((randomIndex -= toSpawnEnemies[i].count) >= 0)
                    continue;
                chosenEnemy = toSpawnEnemies[i].type;
                toSpawnEnemies[i] = (chosenEnemy, toSpawnEnemies[i].count - 1);
            }

            currentEnemies.Add(chosenEnemy.DelayedSpawn(GetEnemySpawnPos(),
                spawnDelay + totalSpawned++ * spawnDelayPerEnemy));
        }
    }

    public Vector3 GetEnemySpawnPos()
    {
        return transform.position + (spawnRadius * Random.insideUnitCircle).XZ_Y(0);
    }
}
