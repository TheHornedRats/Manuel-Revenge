using UnityEngine;

public class Enemy2Spawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject enemyPrefab; // Prefab del enemigo
    public Transform player; // Referencia al jugador
    public float spawnRadius = 20f; // Radio en el que aparecerán los enemigos fuera del mapa
    public float initialSpawnInterval = 5f; // Intervalo inicial entre apariciones (en segundos)
    public float minSpawnInterval = 0.5f; // Tiempo mínimo entre spawns (evita que sea demasiado rápido)
    public float accelerationRate = 0.95f; // Factor de reducción del tiempo de spawn (0.95 = se reduce un 5% cada spawn)
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
            spawnTimer = currentSpawnInterval; // Reinicia el temporizador

            // Reduce el intervalo de spawn para acelerar la generación de enemigos
            currentSpawnInterval *= accelerationRate;
            currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval); // Evita que sea menor al mínimo
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
