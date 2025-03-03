using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    public GameObject chestPrefab;  // Prefab del cofre
    public Transform player;        // Referencia al transform del jugador
    public float spawnInterval = 10f; // Intervalo de aparici�n en segundos
    public float minDistance = 30f;  // Distancia m�nima para spawnear el cofre
    public float maxDistance = 60f;  // Distancia m�xima para spawnear el cofre

    private void Start()
    {
        // Iniciar la aparici�n autom�tica de cofres
        InvokeRepeating(nameof(SpawnChest), 0f, spawnInterval);
    }

    public void SpawnChest()
    {
        if (chestPrefab == null || player == null)
        {
            Debug.LogWarning("Falta asignar el prefab del cofre o la referencia al jugador.");
            return;
        }

        // Generar una posici�n aleatoria en un rango definido desde la posici�n del jugador
        float distance = Random.Range(minDistance, maxDistance);
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 spawnPosition = player.position + (Vector3)(randomDirection * distance);

        // Instanciar el cofre en la posici�n calculada
        Instantiate(chestPrefab, spawnPosition, Quaternion.identity);
    }
}


