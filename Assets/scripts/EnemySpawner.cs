using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject enemyPrefab1; // Prefab del enemigo común (90%)
    public GameObject enemyPrefab2; // Prefab del enemigo raro (10%)
    public GameObject enemyPrefab3; // Prefab del enemigo raro (10%)
    public GameObject enemyPrefab4; // Prefab del enemigo raro (10%)

    public Transform player; // Referencia al jugador
    public float spawnRadius = 20f; // Radio en el que aparecerán los enemigos fuera del mapa
    public float initialSpawnInterval = 5f; // Intervalo inicial entre apariciones (en segundos)
    public float minSpawnInterval = 0.5f; // Tiempo mínimo entre spawns (evita que sea demasiado rápido)
    public float accelerationRate = 0.95f; // Factor de reducción del tiempo de spawn
    public int minGroupSize = 2; // Tamaño mínimo del grupo de enemigos
    public int maxGroupSize = 5; // Tamaño máximo del grupo de enemigos
    public Camera mainCamera; // Referencia a la cámara principal
    public float spawnOutsideMargin = 5f; // Margen para asegurar que los enemigos aparecen fuera de la cámara

    private float currentSpawnInterval; // Intervalo actual entre spawns
    private float spawnTimer; // Temporizador para controlar el spawn

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

        for (int i = 0; i < groupSize; i++)
        {
            Vector3 spawnOffset = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
            Vector3 spawnPosition = baseSpawnPosition + spawnOffset;

            float rand = Random.value;

            GameObject enemyPrefab = rand < 0.7f ? enemyPrefab1 : 
                                     rand < 0.87f ? enemyPrefab2:
                                     rand < 0.97f ? enemyPrefab3 : enemyPrefab4; // 70% para enemigo 1, 17% para enemigo 2, 10% para enemigo 3, 3% para enemigo 4
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            EnemyFollow enemyScript = enemy.GetComponent<EnemyFollow>();
            if (enemyScript != null && player != null)
            {
                enemyScript.player = player; // Asigna el jugador como objetivo
            }
        }
    }

    private Vector3 GetSpawnPositionOutsideCamera()
    {
        Vector3 spawnPosition;
        do
        {
            Vector2 randomPosition = Random.insideUnitCircle.normalized * spawnRadius;
            spawnPosition = new Vector3(randomPosition.x + player.position.x, randomPosition.y + player.position.y, 0);
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
