using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    public EnemySpawner enemySpawner;

    public string activateTag;
    public float spawnRadius;

    public bool complete = false, active = false;

    public int islandIndex;
    public List<Gate> gates = new();

    private void OnTriggerEnter(Collider other)
    {
        if (!complete && !active && other.CompareTag(activateTag))
            Activate();
    }

    public void Activate()
    {
        if (complete || active)
            return;

        active = true;
        foreach (Gate gate in gates)
            gate.SetActive(true);

        enemySpawner.difficultyPoints = GameManager.IslandIndexToEnemyPoints(islandIndex);
        enemySpawner.progression = islandIndex;
        enemySpawner.spawnRadius = spawnRadius;

        enemySpawner.SpawnWave();

        GameManager.GetInstance().combatEnter?.Invoke();
    }

    void Update()
    {
        if (enemySpawner.currentEnemies.Count <= 0)
            Complete();
    }

    public void Complete()
    {
        if (complete || !active)
            return;

        active = false;
        complete = true;

        foreach (Gate gate in gates)
            gate.SetActive(false);

        GameManager.GetInstance().combatExit?.Invoke();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}