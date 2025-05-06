using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnEntry
{
    public GameObject prefab;
    public float minSpawnTime = 0f;  // en segundos
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

    public float initialSpawnInterval = 5f;
    public float minSpawnInterval = 0.5f;
    public float accelerationRate = 0.99f;

    public int minGroupSize = 2;
    public int maxGroupSize = 5;

    private float currentSpawnInterval;
    private float spawnTimer;

    private void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        spawnTimer = currentSpawnInterval;
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnEnemyGroup();
            spawnTimer = currentSpawnInterval;
            currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval * accelerationRate);
        }
    }

    private void SpawnEnemyGroup()
    {
        int groupSize = Random.Range(minGroupSize, maxGroupSize + 1);
        Vector3 baseSpawnPosition = GetSpawnPositionOutsideCamera();
        float gameTime = Time.time;

        for (int i = 0; i < groupSize; i++)
        {
            Vector3 spawnOffset = Random.insideUnitCircle.normalized * Random.Range(3f, 7f);
            Vector3 spawnPosition = baseSpawnPosition + spawnOffset;

            GameObject enemyPrefab = GetWeightedEnemy(gameTime);
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            EnemyFollow enemyScript = enemy.GetComponent<EnemyFollow>();
            if (enemyScript != null && player != null)
            {
                enemyScript.player = player;
            }
        }
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

    private GameObject GetWeightedEnemy(float gameTime)
    {
        List<float> weights = new List<float>();
        float totalWeight = 0f;

        foreach (var entry in spawnableEnemies)
        {
            if (gameTime >= entry.minSpawnTime && gameTime <= entry.maxSpawnTime)
            {
                float t = Mathf.InverseLerp(entry.minSpawnTime, entry.maxSpawnTime, gameTime);
                float weight = Mathf.Pow(t, 2); // Cuadrado para ponderación suave
                weights.Add(weight);
                totalWeight += weight;
            }
            else
            {
                weights.Add(0f);
            }
        }

        if (totalWeight == 0f && spawnableEnemies.Count > 0)
        {
            return spawnableEnemies[0].prefab;
        }

        float rand = Random.value * totalWeight;
        float cumulative = 0f;

        for (int i = 0; i < spawnableEnemies.Count; i++)
        {
            cumulative += weights[i];
            if (rand <= cumulative)
            {
                return spawnableEnemies[i].prefab;
            }
        }

        return spawnableEnemies[0].prefab; // Fallback por seguridad
    }
}
