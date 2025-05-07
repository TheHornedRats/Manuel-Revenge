using System.Collections.Generic;
using UnityEngine;

public class InfiniteMap : MonoBehaviour
{
    [Header("Variantes de Chunk")]
    public List<GameObject> chunkPrefabs;

    [Header("Obstáculos aleatorios")]
    public List<GameObject> obstaclePrefabs;
    public int minObstaclesPerChunk = 0;
    public int maxObstaclesPerChunk = 3;

    [Header("Cámara y tamaño")]
    public Camera mainCamera;
    public Transform player;
    public float chunkSize = 10f;
    public float despawnDelay = 3f;

    private Vector2Int lastChunkPosition;
    private Dictionary<Vector2Int, GameObject> spawnedChunks = new Dictionary<Vector2Int, GameObject>();
    private Dictionary<Vector2Int, float> chunksToDelete = new Dictionary<Vector2Int, float>();

    void Start()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        if (player == null) Debug.LogError(" No se ha asignado el jugador al InfiniteMap.");

        lastChunkPosition = GetChunkPosition(player.position);
        GenerateChunksAroundPlayer();
    }

    void LateUpdate()
    {
        Vector2Int currentChunk = GetChunkPosition(player.position);

        if (currentChunk != lastChunkPosition)
        {
            lastChunkPosition = currentChunk;
            GenerateChunksAroundPlayer();
            ScheduleChunkRemoval();
        }

        ProcessChunkRemoval();
    }

    Vector2Int GetChunkPosition(Vector3 position)
    {
        return new Vector2Int(
            Mathf.FloorToInt(position.x / chunkSize),
            Mathf.FloorToInt(position.y / chunkSize)
        );
    }

    void GenerateChunksAroundPlayer()
    {
        float camHeight = 2f * mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        int chunksX = Mathf.CeilToInt(camWidth / chunkSize) + 3;
        int chunksY = Mathf.CeilToInt(camHeight / chunkSize) + 3;

        Vector2Int centerChunk = GetChunkPosition(player.position);

        for (int x = -chunksX / 2 - 1; x <= chunksX / 2 + 1; x++)
        {
            for (int y = -chunksY / 2 - 1; y <= chunksY / 2 + 1; y++)
            {
                Vector2Int newChunkPos = new Vector2Int(centerChunk.x + x, centerChunk.y + y);

                if (!spawnedChunks.ContainsKey(newChunkPos))
                {
                    SpawnChunk(newChunkPos);
                }

                if (chunksToDelete.ContainsKey(newChunkPos))
                {
                    chunksToDelete.Remove(newChunkPos);
                }
            }
        }
    }

    void SpawnChunk(Vector2Int position)
    {
        Vector3 worldPosition = new Vector3(position.x * chunkSize, position.y * chunkSize, 0);

        // Seleccionar prefab aleatorio
        GameObject prefab = chunkPrefabs[Random.Range(0, chunkPrefabs.Count)];
        GameObject newChunk = Instantiate(prefab, worldPosition, Quaternion.identity);
        spawnedChunks.Add(position, newChunk);

        // Generar obstáculos aleatorios dentro del chunk
        if (obstaclePrefabs != null && obstaclePrefabs.Count > 0)
        {
            int count = Random.Range(minObstaclesPerChunk, maxObstaclesPerChunk + 1);

            for (int i = 0; i < count; i++)
            {
                Vector3 randomOffset = new Vector3(
                    Random.Range(-chunkSize / 2f + 1f, chunkSize / 2f - 1f),
                    Random.Range(-chunkSize / 2f + 1f, chunkSize / 2f - 1f),
                    0f
                );

                GameObject obstacle = Instantiate(
                    obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)],
                    worldPosition + randomOffset,
                    Quaternion.identity,
                    newChunk.transform
                );
            }
        }
    }

    void ScheduleChunkRemoval()
    {
        float camHeight = 2f * mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        int chunksX = Mathf.CeilToInt(camWidth / chunkSize) + 5;
        int chunksY = Mathf.CeilToInt(camHeight / chunkSize) + 5;

        Vector2Int centerChunk = GetChunkPosition(player.position);
        List<Vector2Int> toRemove = new List<Vector2Int>();

        foreach (var chunk in spawnedChunks)
        {
            Vector2Int chunkPos = chunk.Key;

            if (chunkPos.x < centerChunk.x - chunksX / 2 || chunkPos.x > centerChunk.x + chunksX / 2 ||
                chunkPos.y < centerChunk.y - chunksY / 2 || chunkPos.y > centerChunk.y + chunksY / 2)
            {
                toRemove.Add(chunkPos);
            }
        }

        foreach (Vector2Int chunkPos in toRemove)
        {
            if (!chunksToDelete.ContainsKey(chunkPos))
            {
                chunksToDelete.Add(chunkPos, Time.time + despawnDelay);
            }
        }
    }

    void ProcessChunkRemoval()
    {
        List<Vector2Int> toDelete = new List<Vector2Int>();

        foreach (var chunk in chunksToDelete)
        {
            if (Time.time >= chunk.Value)
            {
                toDelete.Add(chunk.Key);
            }
        }

        foreach (Vector2Int chunkPos in toDelete)
        {
            if (spawnedChunks.ContainsKey(chunkPos))
            {
                spawnedChunks[chunkPos].SetActive(false); // O Destroy si no quieres conservar memoria
                spawnedChunks.Remove(chunkPos);
            }
            chunksToDelete.Remove(chunkPos);
        }
    }
}
