using com.cyborgAssets.inspectorButtonPro;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum Difficulty
{
    EASY, MEDIUM, HARD
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get
        {
            if (instance == null)
                instance = FindAnyObjectByType<GameManager>();
            return instance;
        } }

    public List<Menu> menus;
    public string startMenuPath;

    public TextMeshProUGUI speedrunText;

    public GameObject smokePrefab, explosionPrefab;

    public string mainMenuScene, inGameScene;
    public float waterLevel;

    public List<float> enemyPointStart, enemyPointsPerIsland;

    public UnityEvent combatEnter, combatExit;

    public Difficulty difficulty;
    public float startTime, finishTime;

    public List<int> menuPath;
    public List<float> timeMultipliers;
    public bool paused;

    public int currentWave = 0;

    public static int IslandIndexToEnemyPoints(int islandIndex)
    {
        return Mathf.FloorToInt(Instance.enemyPointStart[(int)Instance.difficulty] +
            islandIndex * Instance.enemyPointStart[(int)Instance.difficulty]);
    }

    public static GameObject SpawnSmoke(Vector3 pos, float scale = 1)
    {
        GameObject newSmoke = Instantiate(instance.smokePrefab, pos, Quaternion.identity);
        newSmoke.transform.localScale *= scale;
        return newSmoke;
    }

    public GameObject SpawnExplosion(Vector3 pos, float scale = 1)
    {
        GameObject result = Instantiate(explosionPrefab, pos, Quaternion.identity);
        result.transform.localScale *= scale;
        return result;
    }

    public void ResetMenu()
    {
        while (ToParentMenu()) ;
    }

    public bool ToParentMenu()
    {
        if (menuPath.Count <= 0)
            return false;

        menus[menuPath[^1]].SetActivate(false);
        menuPath.RemoveAt(menuPath.Count - 1);
        if (menuPath.Count > 0)
            menus[menuPath[^1]].SetActivate(true);
        return true;
    }

    public void ApplyMenuDirection(string direction)
    {
        if (direction == "..")
        {
            ToParentMenu();
            return;
        }
        if (direction == "-")
        {
            ResetMenu();
            return;
        }

        if (menuPath.Count > 0)
            menus[menuPath[^1]].SetActivate(false);
        int index = menus.FindIndex((menu) => menu.internalName == direction);
        menus[index].SetActivate(true);
        menuPath.Add(index);
    }

    [ProPlayButton]
    public void ApplyMenuPath(string path)
    {
        if (path == "")
            return;

        string[] parsedPath = path.Split('/');
        foreach (string direction in parsedPath)
            ApplyMenuDirection(direction);


        /*switch (instance.gameState)
        {
            case GameState.MAIN_MENU:
                SceneManager.LoadScene(instance.inGameScene);
                break;

            case GameState.IN_GAME:
                PlayerManager.Instance.active = false;
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

                if (PlayerManager.Instance)
                    PlayerManager.Instance.active = true;
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

        instance.gameState = gameState;*/
    }

    public void SetMenuPath(string path)
    {
        ResetMenu();
        ApplyMenuPath(path);
    }

    public void StartTimer()
    {
        startTime = Time.time;
    }

    public void EndTimer()
    {
        finishTime = Time.time;
    }

    public string ReadTimer()
    {
        float elapsedTime = finishTime > startTime ? finishTime - startTime : Time.time - startTime;
        return TimeSpan.FromSeconds(elapsedTime).ToString(@"mm\:ss\.F");
    }

    void Start()
    {
        SetMenuPath(startMenuPath);   
    }

    void LateUpdate()
    {
        float timeScale = 1;
        foreach (float multiplier in timeMultipliers)
            timeScale *= multiplier;
        Time.timeScale = timeScale;
    }
}
