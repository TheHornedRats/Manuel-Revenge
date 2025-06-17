using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnEntry
{
    public GameObject prefab;
    public float cost = 1f;
    public float minSpawnTime = 0f;
    public float maxSpawnTime = 600f;
}

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawnable Enemies")]
    public List<EnemySpawnEntry> spawnableEnemies = new List<EnemySpawnEntry>();

    [Header("Spawner Settings")]
    public Transform player;
    public Camera mainCamera;

    public float spawnRadius = 20f;
    public float spawnThickness = 4f;

    public float maxGameTime = 900f;
    public float waveInterval = 3f;
    private float waveTimer;

    public AnimationCurve budgetCurve;
    public float baseWavePoints = 5f;
    public float budgetMultiplier = 1f;

    public AnimationCurve intervalCurve;
    public float minSpawnInterval = 0.5f;
    public float maxSpawnInterval = 5f;

    private void Start()
    {
        waveTimer = waveInterval;
    }

    private void Update()
    {
        waveTimer -= Time.deltaTime;

        if (waveTimer <= 0f)
        {
            SpawnEnemyWave();
            waveTimer = waveInterval;
        }
    }

    private void SpawnEnemyWave()
    {
        float gameTime = Time.time;
        float timePercent = Mathf.Clamp01(gameTime / maxGameTime);

        float intervalFactor = intervalCurve.Evaluate(timePercent);
        waveInterval = Mathf.Lerp(maxSpawnInterval, minSpawnInterval, intervalFactor);

        float budgetFactor = budgetCurve.Evaluate(timePercent);
        float currentBudget = baseWavePoints * budgetFactor * budgetMultiplier;

        List<GameObject> enemiesToSpawn = GenerateEnemyWave(currentBudget, gameTime);

        Vector3 baseSpawnPosition = GetSpawnPositionOutsideCamera();

        foreach (var enemyPrefab in enemiesToSpawn)
        {
            Vector3 spawnOffset = Random.insideUnitCircle.normalized * Random.Range(3f, 7f);
            Vector3 spawnPosition = baseSpawnPosition + spawnOffset;

            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            EnemyFollow enemyScript = enemy.GetComponent<EnemyFollow>();
            if (enemyScript != null && player != null)
            {
                enemyScript.player = player;
            }
        }
    }

    private List<GameObject> GenerateEnemyWave(float budget, float gameTime)
    {
        List<GameObject> wave = new List<GameObject>();
        List<EnemySpawnEntry> validEnemies = spawnableEnemies.FindAll(e => gameTime >= e.minSpawnTime && gameTime <= e.maxSpawnTime);

        while (budget > 0f && validEnemies.Count > 0)
        {
            EnemySpawnEntry entry = validEnemies[Random.Range(0, validEnemies.Count)];
            if (entry.cost <= budget)
            {
                wave.Add(entry.prefab);
                budget -= entry.cost;
            }
            else
            {
                validEnemies.Remove(entry);
            }
        }

        return wave;
    }

    private Vector3 GetSpawnPositionOutsideCamera()
    {
        Vector3 spawnPosition;
        do
        {
            Vector2 direction = Random.insideUnitCircle.normalized;
            float distance = Random.Range(spawnRadius, spawnRadius + spawnThickness);
            spawnPosition = player.position + (Vector3)(direction * distance);
        }
        while (IsPositionInCameraView(spawnPosition));

        return spawnPosition;
    }

    private bool IsPositionInCameraView(Vector3 position)
    {
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(position);
        return viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1;
    }
}
