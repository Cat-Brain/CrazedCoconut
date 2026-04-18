using UnityEngine;

public class SpawnWaveCall : MonoBehaviour
{
    public void Activate()
    {
        EnemySpawnManager.Instance.SpawnWave();
    }
}
