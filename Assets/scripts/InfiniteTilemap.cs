using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InfiniteTilemap : MonoBehaviour
{
    public Tilemap tilemap; // El Tilemap donde colocaremos los tiles
    public Tile tile; // El tile que se generará
    public int tileSize = 1; // Tamaño del tile
    public int renderDistance = 5; // Cuántos tiles generar alrededor del jugador

    private HashSet<Vector2Int> spawnedTiles = new HashSet<Vector2Int>();
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Encuentra al jugador por su Tag
        StartCoroutine(UpdateTiles());
    }

    IEnumerator UpdateTiles()
    {
        while (true)
        {
            Vector2Int playerPos = new Vector2Int(
                Mathf.RoundToInt(player.position.x / tileSize),
                Mathf.RoundToInt(player.position.y / tileSize)
            );

            for (int x = -renderDistance; x <= renderDistance; x++)
            {
                for (int y = -renderDistance; y <= renderDistance; y++)
                {
                    Vector2Int tileCoord = new Vector2Int(playerPos.x + x, playerPos.y + y);
                    if (!spawnedTiles.Contains(tileCoord))
                    {
                        SpawnTile(tileCoord);
                    }
                }
            }

            yield return new WaitForSeconds(0.5f); // Espera 0.5 segundos antes de actualizar
        }
    }

    void SpawnTile(Vector2Int coord)
    {
        tilemap.SetTile((Vector3Int)coord, tile);
        spawnedTiles.Add(coord);
    }
}