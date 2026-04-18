using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomEnemySelector : MonoBehaviour
{
    public TextMeshProUGUI text;

    public List<int> enemyCounts;
    public Seed seed;

    public void UpdateUI()
    {
        enemyCounts = EnemySpawnManager.Instance.GetRandomEnemySpawnCounts();
        seed = PlayerManager.Instance.GetRandomSeed();

        string displayText = "";

        for (int i = 0; i < enemyCounts.Count; i++)
            if (enemyCounts[i] > 0)
                displayText += EnemySpawnManager.Instance.enemyTypes[i].name + " * " + enemyCounts[i] + "\n";

        displayText += "\nFor a " + seed.name + " seed";
        text.text = displayText;
    }

    public void Activate()
    {
        EnemySpawnManager.Instance.toSpawnEnemyCounts = enemyCounts;
        PlayerManager.Instance.plant.deck.AddSeed(seed);
    }
}
