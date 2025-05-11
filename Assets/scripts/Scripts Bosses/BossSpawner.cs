using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public Transform player;
    public GameObject[] bossPrefabs;
    public float spawnRadius = 15f;
    public float[] spawnTimes = { 300f, 600f, 900f }; // Se puede extender
    private bool[] hasSpawned;

    private void Start()
    {
        hasSpawned = new bool[spawnTimes.Length];
    }

    private void Update()
    {
        float currentTime = Time.time;

        for (int i = 0; i < spawnTimes.Length; i++)
        {
            if (!hasSpawned[i] && currentTime >= spawnTimes[i])
            {
                SpawnBoss(i);
                hasSpawned[i] = true;
            }
        }
    }

    void SpawnBoss(int index)
    {
        Vector2 spawnOffset = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPosition = player.position + new Vector3(spawnOffset.x, spawnOffset.y, 0);

        GameObject boss = Instantiate(bossPrefabs[index], spawnPosition, Quaternion.identity);

        //  Asigna el objetivo al seguir
        EnemyFollow follow = boss.GetComponent<EnemyFollow>();
        if (follow != null)
        {
            follow.player = player;
        }

        Debug.Log($" BOSS {index + 1} ha sido invocado en {spawnPosition}");
    }
}
