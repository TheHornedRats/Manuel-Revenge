using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class InfiniteMap : MonoBehaviour
{
    public GameObject chunkPrefab;  // Prefab del Chunk
    public Camera mainCamera;       // Referencia a la cámara principal
    public float chunkSize = 10f;   // Tamaño de cada Chunk
    public float despawnDelay = 3f; // Tiempo antes de eliminar un chunk fuera de la vista

    private Vector2Int lastChunkPosition;
    private Dictionary<Vector2Int, GameObject> spawnedChunks = new Dictionary<Vector2Int, GameObject>(); // Chunks activos
    private Dictionary<Vector2Int, float> chunksToDelete = new Dictionary<Vector2Int, float>(); // Chunks en proceso de eliminación

    void Start()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        lastChunkPosition = GetChunkPosition(mainCamera.transform.position);
        GenerateChunksAroundCamera();
    }

    void LateUpdate()
    {
        Vector2Int currentChunk = GetChunkPosition(mainCamera.transform.position);

        if (currentChunk != lastChunkPosition)
        {
            lastChunkPosition = currentChunk;
            GenerateChunksAroundCamera();
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

    void GenerateChunksAroundCamera()
    {
        float camHeight = 2f * mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        int chunksX = Mathf.CeilToInt(camWidth / chunkSize) + 3;
        int chunksY = Mathf.CeilToInt(camHeight / chunkSize) + 3;

        Vector2Int centerChunk = GetChunkPosition(mainCamera.transform.position);

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
        GameObject newChunk = Instantiate(chunkPrefab, worldPosition, Quaternion.identity);
        spawnedChunks.Add(position, newChunk);
    }

    void ScheduleChunkRemoval()
    {
        float camHeight = 2f * mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        int chunksX = Mathf.CeilToInt(camWidth / chunkSize) + 5;
        int chunksY = Mathf.CeilToInt(camHeight / chunkSize) + 5;

        Vector2Int centerChunk = GetChunkPosition(mainCamera.transform.position);

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
                spawnedChunks[chunkPos].SetActive(false); // Desactiva en lugar de destruir
                spawnedChunks.Remove(chunkPos);
            }
            chunksToDelete.Remove(chunkPos);
        }
    }
}
