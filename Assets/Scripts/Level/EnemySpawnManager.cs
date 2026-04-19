using ClownLib;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawnManager : MonoBehaviour
{
    private static EnemySpawnManager instance;
    public static EnemySpawnManager Instance
    { get {
            if (instance == null)
                instance = FindAnyObjectByType<EnemySpawnManager>();
            return instance;
    } }

    public UnityEvent exitCombat;

    public int startPoints, pointsPerWave;
    public List<Enemy> enemyTypes;
    public float spawnDelay, spawnDelayPerEnemy, spawnRadius;

    public List<int> toSpawnEnemyCounts = new();
    public List<Enemy> currentEnemies = new();

    public bool inWave = false;

    public int RemainingEnemiesToSpawn()
    {
        int result = 0;
        foreach (int count in toSpawnEnemyCounts)
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
            for (int i = 0; i < toSpawnEnemyCounts.Count; i++)
            {
                if ((randomIndex -= toSpawnEnemyCounts[i]) >= 0)
                    continue;
                chosenEnemy = enemyTypes[i];
                toSpawnEnemyCounts[i]--;
                break;
            }

            currentEnemies.Add(chosenEnemy.DelayedSpawn(GetEnemySpawnPos(),
                spawnDelay + totalSpawned++ * spawnDelayPerEnemy));
        }
        inWave = true;
        GameManager.Instance.combatEnter?.Invoke();
    }

    public int GetPoints()
    {
        return GameManager.Instance.currentWave * pointsPerWave + startPoints;
    }

    public Vector3 GetEnemySpawnPos()
    {
        return transform.position + (spawnRadius * Random.insideUnitCircle).XZ_Y(0);
    }

    public List<int> GetRandomEnemySpawnCounts()
    {
        List<int> results = new();

        int desiredPoints = GetPoints();

        float totalWeight = 0;
        float[] randomWeights = new float[enemyTypes.Count];
        for (int i = 0; i < randomWeights.Length; i++)
        {
            results.Add(0);
            randomWeights[i] = enemyTypes[i].CanSpawn(
                GameManager.Instance.currentWave) ? Random.value : 0;
            totalWeight += randomWeights[i];
        }
        for (int i = 0; i < randomWeights.Length; i++)
            randomWeights[i] /= totalWeight;

        while (desiredPoints > 0)
        {
            float randomValue = Random.value;

            for (int i = 0; i < randomWeights.Length; i++)
            {
                if ((randomValue -= randomWeights[i]) >= 0)
                    continue;

                results[i]++;
                desiredPoints -= enemyTypes[i].spawnPoints;
                break;
            }
        }
        
        return results;
    }

    void LateUpdate()
    {
        if (inWave && currentEnemies.Count <= 0)
        {
            inWave = false;
            exitCombat?.Invoke();
            GameManager.Instance.combatExit?.Invoke();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.orange;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
