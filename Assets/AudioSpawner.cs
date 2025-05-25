using UnityEngine;

public class AudioSpawner : MonoBehaviour
{
    public GameObject[] audioPrefabs;       
    public Transform player;                 
    public float spawnInterval = 8f;        
    public float spawnRadius = 12f;          

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnAudio();
            timer = 0f;
        }
    }

    void SpawnAudio()
    {
        if (audioPrefabs.Length == 0 || player == null) return;

        Vector2 randomOffset = Random.insideUnitCircle.normalized * Random.Range(5f, spawnRadius);
        Vector3 spawnPos = player.position + new Vector3(randomOffset.x, randomOffset.y, 0);

        GameObject prefab = audioPrefabs[Random.Range(0, audioPrefabs.Length)];
        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
}
