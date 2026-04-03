using ClownLib;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int difficultyPoints, progression;
    public float spawnRadius;

    public List<Enemy> currentEnemies = new();

    public void SpawnWave()
    {
        EnemySpawnManager.SpawnWave(this);
    }

    public Vector3 GetEnemySpawnPos()
    {
        return transform.position + (spawnRadius * Random.insideUnitCircle).XZ_Y(0);
    }
}
