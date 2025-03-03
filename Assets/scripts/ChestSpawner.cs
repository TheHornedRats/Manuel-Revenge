using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    public GameObject chestPrefab;  // Prefab del cofre
    public Transform player;        // Referencia al transform del jugador
    public float spawnInterval = 10f; // Intervalo de aparición en segundos
    public float minDistance = 30f;  // Distancia mínima para spawnear el cofre
    public float maxDistance = 60f;  // Distancia máxima para spawnear el cofre

    private void Start()
    {
        // Iniciar la aparición automática de cofres
        InvokeRepeating(nameof(SpawnChest), 0f, spawnInterval);
    }

    public void SpawnChest()
    {
        if (chestPrefab == null || player == null)
        {
            Debug.LogWarning("Falta asignar el prefab del cofre o la referencia al jugador.");
            return;
        }

        // Generar una posición aleatoria en un rango definido desde la posición del jugador
        float distance = Random.Range(minDistance, maxDistance);
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 spawnPosition = player.position + (Vector3)(randomDirection * distance);

        // Instanciar el cofre en la posición calculada
        Instantiate(chestPrefab, spawnPosition, Quaternion.identity);
    }
}


