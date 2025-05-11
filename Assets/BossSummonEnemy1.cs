using UnityEngine;

public class BossSummonEnemy1 : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public int numberOfEnemies = 10;
    public float spawnRadius = 3f;
    public float delayBeforeSummon = 0.1f;

    private void Start()
    {
        Invoke(nameof(SummonEnemies), delayBeforeSummon);
    }

    void SummonEnemies()
    {
        if (enemyToSpawn == null)
        {
            Debug.LogWarning("No se ha asignado un prefab de enemigo para invocar.");
            return;
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null)
        {
            Debug.LogWarning("No se ha encontrado el jugador con tag 'Player'.");
            return;
        }

        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector2 offset = Random.insideUnitCircle.normalized * spawnRadius;
            Vector3 spawnPosition = transform.position + new Vector3(offset.x, offset.y, 0f);

            GameObject newEnemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);

            EnemyFollow follow = newEnemy.GetComponent<EnemyFollow>();
            if (follow != null)
            {
                follow.player = playerObj.transform;
            }
        }

        Destroy(gameObject); // El invocador ya no es necesario
    }
}
