using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Level currentLevel;
    private float timeRemaining = 0f;
    private int currentStage = 0;

    private int dnasCollected

    public int[] GetGameResult {
        int[] result = new int[2];

        return {, };
    }
    public Level CurrentLevel() {
        return currentLevel;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        if (currentLevel != null)
        {
            InitLevel(currentLevel);
            EnemySpawner[] spawners = FindObjectsOfType<EnemySpawner>();
            foreach (EnemySpawner spawner in spawners) {
                spawner.SetEnemiesPrefabs(currentLevel.enemiesPrefabs);
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (currentLevel == null) return;

        timeRemaining -= Time.deltaTime;
        UpdateGameStage();
    }

    public void InitLevel(Level level) {
        currentLevel = level;
        timeRemaining = level.gameDuration;
        currentStage = 0;
        UpdateEnemySpawners(level.enemiesPerStages[currentStage]);
        Debug.Log("Level " + level.name + " initialized");
        Debug.Log("Time Remaining " + timeRemaining);
    }

    private void UpdateEnemySpawners(int enemiesToSpawn) {
        EnemySpawner[] spawners = FindObjectsOfType<EnemySpawner>();
        foreach (EnemySpawner spawner in spawners) {
            spawner.SetNumberOfEnemies(enemiesToSpawn);
            spawner.SetCurrentStage(currentStage);
        }
    }

    private void updateGameStage() {
        float gameDuration = currentLevel.gameDuration;
        float completionPercent = 1 - (timeRemaining / gameDuration);

        for (int i = 0; i < currentLevel.completionStages.Length; i++) {
            if (completionPercent <= currentLevel.completionStages[i] && currentStage != i) {
                currentStage = i;
                UpdateEnemySpawners(currentLevel.enemiesPerStages[currentStage]);
                Debug.Log($"Entered Stage {currentStage}");
                // break;
            }
        }
    }

    private void UpdateGameStage()
    {
        float gameDuration = currentLevel.gameDuration;
        float completionPercent = 1 - (timeRemaining / gameDuration);

        for (int i = 0; i < currentLevel.completionStages.Length; i++)
        {
            if (completionPercent >= currentLevel.completionStages[i] && currentStage == i)
            {
                currentStage = i + 1;
                UpdateEnemySpawners(currentLevel.enemiesPerStages[currentStage]);
                Debug.Log($"Entered Stage {currentStage}");
                break;
            }
        }
    }

    private bool inRange(float value, float a, float b) {
        return value >= a && value <= b;
    }
}
