using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<GameObject> islandPrefabs, bridgePrefabs, gatePrefabs;
    public GameObject winIslandPrefab;

    public float gridWidth, islandWidth, gateDist;
    public int islandCount, minIslandsPerTurn, maxIslandsPerTurn;

    public List<GameObject> bridges = new();
    public List<Island> islands = new();

    void Awake()
    {
        Init();
    }

    public void Init()
    {
        Vector3Int currentPos = Vector3Int.zero,
            currentDir = Vector3Int.forward;

        for (int i = 0, islandsTillTurn = Random.Range(minIslandsPerTurn, maxIslandsPerTurn + 1);
            i < islandCount; i++, islandsTillTurn--)
        {
            if (islandsTillTurn <= 0)
            {
                if (currentDir == Vector3Int.forward)
                    currentDir = Random.Range(0, 2) == 0 ? Vector3Int.left : Vector3Int.right;
                else
                    currentDir = Vector3Int.forward;
            }

            if (islands.Count > 0)
                islands[^1].gates.Add(Instantiate(gatePrefabs[Random.Range(0, gatePrefabs.Count)],
                    gridWidth * (Vector3)currentPos + gateDist * (Vector3)currentDir,
                    Quaternion.Euler(0, currentDir.x == 0 ? 0 : 90 +
                    180 * Random.Range(0, 2), 0), transform).GetComponent<Gate>());

            bridges.Add(Instantiate(bridgePrefabs[Random.Range(0, bridgePrefabs.Count)],
                gridWidth * (currentPos + 0.5f * (Vector3)currentDir),
                Quaternion.Euler(0, currentDir.x == 0 ? 0 : 90 +
                180 * Random.Range(0, 2), 0), transform));

            currentPos += currentDir;

            islands.Add(Instantiate(islandPrefabs[Random.Range(0, islandPrefabs.Count)],
                gridWidth * (Vector3)currentPos,
                Quaternion.Euler(0, 90 * Random.Range(0, 4), 0), transform).GetComponent<Island>());

            islands[^1].islandIndex = islands.Count - 2;

            islands[^1].gates.Add(Instantiate(gatePrefabs[Random.Range(0, gatePrefabs.Count)],
                gridWidth * (Vector3)currentPos - gateDist * (Vector3)currentDir,
                Quaternion.Euler(0, currentDir.x == 0 ? 0 : 90 +
                180 * Random.Range(0, 2), 0), transform).GetComponent<Gate>());
        }

        currentDir = Vector3Int.forward;

        if (islands.Count > 0)
            islands[^1].gates.Add(Instantiate(gatePrefabs[Random.Range(0, gatePrefabs.Count)],
                gridWidth * (Vector3)currentPos + gateDist * (Vector3)currentDir,
                Quaternion.Euler(0, currentDir.x == 0 ? 0 : 90 +
                180 * Random.Range(0, 2), 0), transform).GetComponent<Gate>());

        bridges.Add(Instantiate(bridgePrefabs[Random.Range(0, bridgePrefabs.Count)],
            gridWidth * (currentPos + 0.5f * (Vector3)currentDir),
            Quaternion.Euler(0, currentDir.x == 0 ? 0 : 90 +
            180 * Random.Range(0, 2), 0), transform));

        currentPos += currentDir;

        Instantiate(winIslandPrefab,
            gridWidth * (Vector3)currentPos,
            Quaternion.identity, transform);
    }
}
