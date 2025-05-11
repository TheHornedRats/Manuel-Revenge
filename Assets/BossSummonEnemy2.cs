using UnityEngine;

public class BossSummonEnemy2 : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public float delayBeforeSummon = 0.1f;

    private void Start()
    {
        Invoke(nameof(Summon), delayBeforeSummon);
    }

    void Summon()
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

        GameObject newEnemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);

        EnemyFollow follow = newEnemy.GetComponent<EnemyFollow>();
        if (follow != null)
        {
            follow.player = playerObj.transform;
        }

        Destroy(gameObject); // El objeto invocador ya no es necesario
    }
}
