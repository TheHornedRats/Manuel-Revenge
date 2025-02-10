using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject enemyPrefab; // Prefab del enemigo
    public Transform player; // Referencia al jugador
    public float spawnRadius = 20f; // Radio en el que aparecer�n los enemigos fuera del mapa
    public float initialSpawnInterval = 5f; // Intervalo inicial entre apariciones (en segundos)
    public float minSpawnInterval = 0.5f; // Tiempo m�nimo entre spawns (evita que sea demasiado r�pido)
    public float accelerationRate = 0.95f; // Factor de reducci�n del tiempo de spawn (0.95 = se reduce un 5% cada spawn)

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
            SpawnEnemy();
            spawnTimer = currentSpawnInterval; // Reinicia el temporizador

            // Reduce el intervalo de spawn para acelerar la generaci�n de enemigos
            currentSpawnInterval *= accelerationRate;
            currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval); // Evita que sea menor al m�nimo
        }
    }

    private void SpawnEnemy()
    {
        // Genera una posici�n aleatoria en el c�rculo alrededor del mapa (sin aparecer en el centro)
        Vector2 randomPosition = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPosition = new Vector3(randomPosition.x + player.position.x, randomPosition.y + player.position.y, 0);

        // Instancia el enemigo y asigna al jugador como objetivo
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        EnemyFollow enemyScript = enemy.GetComponent<EnemyFollow>();
        if (enemyScript != null && player != null)
        {
            enemyScript.player = player; // Asigna el jugador como objetivo
        }
    }
}
