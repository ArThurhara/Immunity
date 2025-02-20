using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject[] enemiesPrefabs;
    [SerializeField] private Transform[] spawnPoints;

    private int currentStage = 0;
    private int numberOfEnemies = 0;
    private float spawnRate = 1.5f;
    private Coroutine spawnCoroutine;

    public void SetCurrentStage(int stage) {
        currentStage = stage;
    }

    public void SetNumberOfEnemies(int number) {
        numberOfEnemies = number;
        if (number > 0 && !spawnStarted) {
            RestartSpawning();
        }
    }

    public void SetEnemiesPrefabs(GameObject[] enemies) {
        enemiesPrefabs = enemies;
    }

    public int GetCurrentStage() {
        return currentStage;
    }

    public int GetNumberOfEnemies() {
        return numberOfEnemies;
    }

    public bool spawnOnTrigger = false;
    private bool spawnStarted = false;

    private void Start() {
        if (GameManager.Instance != null && GameManager.Instance.CurrentLevel() != null)
        {
            enemiesPrefabs = GameManager.Instance.CurrentLevel().enemiesPrefabs;
        }
        if (!spawnOnTrigger) {
            RestartSpawning();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log("Inside Spawn Zone");
        if (spawnOnTrigger) {
            if (collider.CompareTag("Player")) {
                RestartSpawning();
            }
        }
    }

    private void RestartSpawning() {
        if (spawnCoroutine != null) {
            StopCoroutine(spawnCoroutine);
        }
        
        spawnStarted = true;
        spawnCoroutine = StartCoroutine(Spawn());
    }

    IEnumerator Spawn() {
        Debug.Log("Number of Enemies: " + numberOfEnemies);
        while (spawnStarted && numberOfEnemies > 0) {
            int randomIndex = Math.Clamp(UnityEngine.Random.Range(0, currentStage + 1), 0, enemiesPrefabs.Length - 1); 
            int randomSpawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
            Instantiate(enemiesPrefabs[randomIndex], spawnPoints[randomSpawnPointIndex].position, Quaternion.identity);
            numberOfEnemies--;
            yield return new WaitForSeconds(spawnRate);
        }
        spawnStarted = false;
        spawnCoroutine = null;
    }
}