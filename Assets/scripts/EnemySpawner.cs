using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo a spawnear
    public Transform player; // Referencia al jugador para que los enemigos puedan seguirlo
    public float spawnRadius = 20f; // Radio en el que aparecerán los enemigos fuera del mapa
    public float initialSpawnInterval = 120f; // Intervalo inicial entre apariciones (en frames)
    public float accelerationRate = 0.98f; // Tasa de aceleración (reduce el tiempo de spawn)
    private float currentSpawnInterval; // Intervalo actual entre spawns

    private int frameCounter = 0; // Contador de frames

    private void Start()
    {
        // Inicializa el intervalo de spawn con el valor inicial
        currentSpawnInterval = initialSpawnInterval;
    }

    private void Update()
    {
        frameCounter++;

        if (frameCounter >= currentSpawnInterval)
        {
            SpawnEnemy();
            frameCounter = 0; // Reinicia el contador de frames

            // Reduce el intervalo de spawn para acelerar la generación de enemigos
            currentSpawnInterval *= accelerationRate;
            currentSpawnInterval = Mathf.Max(10f, currentSpawnInterval); // Limita el mínimo intervalo
        }
    }

    private void SpawnEnemy()
    {
        // Genera una posición aleatoria en el círculo alrededor del mapa
        Vector2 randomPosition = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPosition = new Vector3(randomPosition.x,  randomPosition.y, 0);

        // Instancia el enemigo y configura su objetivo
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        EnemyFollow enemyScript = enemy.GetComponent<EnemyFollow>();
        if (enemyScript != null && player != null)
        {
            enemyScript.player = player; // Asigna al jugador como objetivo
        }
    }
}
