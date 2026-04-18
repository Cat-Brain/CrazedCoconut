using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public float smokeScale, spawnDuration;
    public int spawnPoints, spawnProgressionStart, spawnProgressionEnd;

    public Transform player;

    public void Init()
    {
        GameManager.SpawnSmoke(transform.position, smokeScale);
        transform.DOScale(transform.localScale, spawnDuration).From(0);

        player = PlayerManager.Instance.transform;
    }

    public override void OnDeath()
    {
        base.OnDeath();
        EnemySpawnManager.Instance.currentEnemies.Remove(this);
    }

    public bool CanSpawn(int wave)
    {
        return wave >= spawnProgressionStart && wave <= spawnProgressionEnd;
    }

    public Enemy SimpleSpawn(Vector3 pos)
    {
        Enemy enemy = Instantiate(gameObject, pos, Random.rotation).GetComponent<Enemy>();
        return enemy;
    }

    public Enemy Spawn(Vector3 pos)
    {
        Enemy enemy = SimpleSpawn(pos);
        enemy.Init();
        return enemy;
    }

    public Enemy DelayedSpawn(Vector3 pos, float delay)
    {
        Enemy enemy = SimpleSpawn(pos);
        enemy.gameObject.SetActive(false);
        EnemySpawnManager.Instance.StartCoroutine(enemy.ActivateOnDelay(delay));
        return enemy;
    }

    public IEnumerator ActivateOnDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        gameObject.SetActive(true);
        Init();
    }
}
