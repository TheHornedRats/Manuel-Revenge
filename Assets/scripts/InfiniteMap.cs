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
        lastChunkPosition = GetChunkPosition(mainCamera.transform.position);
        GenerateChunksAroundCamera();
    }

    void FixedUpdate()
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

    // Convierte la posición de la cámara en coordenadas de Chunk
    Vector2Int GetChunkPosition(Vector3 position)
    {
        return new Vector2Int(
            Mathf.FloorToInt(position.x / chunkSize),
            Mathf.FloorToInt(position.y / chunkSize)
        );
    }

    // Genera los chunks en TODAS direcciones alrededor de la cámara
    void GenerateChunksAroundCamera()
    {
        float camHeight = 10f * mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        int chunksX = Mathf.CeilToInt(camWidth / chunkSize) + 10;
        int chunksY = Mathf.CeilToInt(camHeight / chunkSize) + 10;

        Vector2Int centerChunk = GetChunkPosition(mainCamera.transform.position);

        for (int x = -chunksX / 2; x <= chunksX / 2; x++)
        {
            for (int y = -chunksY / 2; y <= chunksY / 2; y++)
            {
                Vector2Int newChunkPos = new Vector2Int(centerChunk.x + x, centerChunk.y + y);

                if (!spawnedChunks.ContainsKey(newChunkPos)) // Solo generar si no existe
                {
                    SpawnChunk(newChunkPos);
                }

                // Si un chunk estaba marcado para eliminar pero sigue visible, lo quitamos de la lista de eliminación
                if (chunksToDelete.ContainsKey(newChunkPos))
                {
                    chunksToDelete.Remove(newChunkPos);
                }
            }
        }
    }

    // Instancia un nuevo Chunk en la posición indicada
    void SpawnChunk(Vector2Int position)
    {
        Vector3 worldPosition = new Vector3(position.x * chunkSize, position.y * chunkSize, 0);
        GameObject newChunk = Instantiate(chunkPrefab, worldPosition, Quaternion.identity);
        spawnedChunks.Add(position, newChunk);
    }

    // Programa la eliminación de chunks que ya no están en la vista de la cámara
    void ScheduleChunkRemoval()
    {
        float camHeight = 2f * mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        int chunksX = Mathf.CeilToInt(camWidth / chunkSize) + 2;
        int chunksY = Mathf.CeilToInt(camHeight / chunkSize) + 2;

        Vector2Int centerChunk = GetChunkPosition(mainCamera.transform.position);

        List<Vector2Int> toRemove = new List<Vector2Int>();

        foreach (var chunk in spawnedChunks)
        {
            Vector2Int chunkPos = chunk.Key;

            // Si el chunk está fuera del área visible extendida, programamos su eliminación
            if (chunkPos.x < centerChunk.x - chunksX / 2 || chunkPos.x > centerChunk.x + chunksX / 2 ||
                chunkPos.y < centerChunk.y - chunksY / 2 || chunkPos.y > centerChunk.y + chunksY / 2)
            {
                toRemove.Add(chunkPos);
            }
        }

        // Añadir chunks a la lista de eliminación con un temporizador
        foreach (Vector2Int chunkPos in toRemove)
        {
            if (!chunksToDelete.ContainsKey(chunkPos)) // Evita marcar un chunk varias veces
            {
                chunksToDelete.Add(chunkPos, Time.time + despawnDelay); // Guardamos el tiempo de eliminación
            }
        }
    }

    // Procesa la eliminación de chunks que ya pasaron el tiempo de espera
    void ProcessChunkRemoval()
    {
        List<Vector2Int> toDelete = new List<Vector2Int>();

        foreach (var chunk in chunksToDelete)
        {
            if (Time.time >= chunk.Value) // Si ya pasó el tiempo de despawn
            {
                toDelete.Add(chunk.Key);
            }
        }

        foreach (Vector2Int chunkPos in toDelete)
        {
            if (spawnedChunks.ContainsKey(chunkPos))
            {
                Destroy(spawnedChunks[chunkPos]);
                spawnedChunks.Remove(chunkPos);
            }
            chunksToDelete.Remove(chunkPos);
        }
    }
}
