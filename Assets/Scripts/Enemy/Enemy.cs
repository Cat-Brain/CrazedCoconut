using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public EnemySpawner spawner;

    public float smokeScale, spawnDuration;
    public int spawnPoints, spawnProgressionStart, spawnProgressionEnd;

    public Transform player;

    public void Init()
    {
        GameManager.SpawnSmoke(transform.position, smokeScale);
        transform.DOScale(transform.localScale, spawnDuration).From(0);

        player = GameManager.instance.playerManager.transform;
    }

    public override void OnDeath()
    {
        base.OnDeath();
        spawner.currentEnemies.Remove(this);
    }

    public Enemy SimpleSpawn(Vector3 pos, EnemySpawner spawner)
    {
        Enemy enemy = Instantiate(gameObject, pos, Random.rotation, spawner.transform).GetComponent<Enemy>();
        enemy.spawner = spawner;
        return enemy;
    }

    public Enemy Spawn(Vector3 pos, EnemySpawner spawner)
    {
        Enemy enemy = SimpleSpawn(pos, spawner);
        enemy.Init();
        return enemy;
    }

    public Enemy DelayedSpawn(Vector3 pos, float delay, EnemySpawner spawner)
    {
        Enemy enemy = SimpleSpawn(pos, spawner);
        enemy.gameObject.SetActive(false);
        spawner.StartCoroutine(enemy.ActivateOnDelay(delay));
        return enemy;
    }

    public IEnumerator ActivateOnDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        gameObject.SetActive(true);
        Init();
    }
}
