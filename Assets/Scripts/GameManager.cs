using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum Difficulty
{
    EASY, MEDIUM, HARD
}

public enum GameState
{
    MAIN_MENU, IN_GAME, WIN, LOSE, CLOSE_GAME
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public EnemySpawnManager enemySpawnManager; // BAD SYSTEM PLEASE REPLACE LATER
    public PlayerManager playerManager; // BAD SYSTEM PLEASE REPLACE LATER

    public Canvas loseCanvas, winCanvas;
    public TextMeshProUGUI speedrunText;

    public GameObject smokePrefab;

    public string mainMenuScene, inGameScene;
    public float waterLevel;

    public List<float> enemyPointStart, enemyPointsPerIsland;

    public Difficulty difficulty;
    public float startTime, finishTime;

    public GameState gameState;
    public bool paused;

    public UnityEvent combatEnter, combatExit;

    private static void TryFindInstance()
    {
        if (instance == null)
            instance = FindAnyObjectByType<GameManager>();
    }

    public static GameManager GetInstance()
    {
        TryFindInstance();
        return instance; // REPLACE THIS LATER!
    }

    public static void SetPlayerManager(PlayerManager playerManager)
    {
        TryFindInstance();
        instance.playerManager = playerManager;
    }

    public static void SetPlayerManager(EnemySpawnManager enemySpawnManager)
    {
        TryFindInstance();
        instance.enemySpawnManager = enemySpawnManager;
    }

    public static int IslandIndexToEnemyPoints(int islandIndex)
    {
        TryFindInstance();

        return Mathf.FloorToInt(instance.enemyPointStart[(int)instance.difficulty] +
            islandIndex * instance.enemyPointStart[(int)instance.difficulty]);
    }

    public static GameObject SpawnSmoke(Vector3 pos, float scale = 1)
    {
        TryFindInstance();

        GameObject newSmoke = Instantiate(instance.smokePrefab, pos, Quaternion.identity);
        newSmoke.transform.localScale *= scale;
        return newSmoke;
    }

    public static void SetGameState(GameState gameState)
    {
        TryFindInstance();

        if (instance.gameState == gameState)
            return;

        switch (instance.gameState)
        {
            case GameState.MAIN_MENU:
                SceneManager.LoadScene(instance.inGameScene);
                break;

            case GameState.IN_GAME:
                instance.playerManager.active = false;
                break;

            case GameState.LOSE:
                instance.loseCanvas.gameObject.SetActive(false);
                break;

            case GameState.WIN:
                instance.winCanvas.gameObject.SetActive(false);
                break;
        }

        switch (gameState)
        {
            case GameState.MAIN_MENU:
                SceneManager.LoadScene(instance.mainMenuScene);
                break;

            case GameState.IN_GAME:
                if (instance.gameState != GameState.MAIN_MENU)
                    SceneManager.LoadScene(instance.inGameScene);

                if (instance.playerManager)
                    instance.playerManager.active = true;
                instance.startTime = Time.time;
                break;

            case GameState.LOSE:
                instance.loseCanvas.gameObject.SetActive(true);
                break;

            case GameState.WIN:
                instance.winCanvas.gameObject.SetActive(true);
                instance.finishTime = Time.time;
                instance.speedrunText.text = "Time: " +
                    (instance.finishTime - instance.startTime).ToString("0.000");
                break;
        }

        instance.gameState = gameState;
    }
}
