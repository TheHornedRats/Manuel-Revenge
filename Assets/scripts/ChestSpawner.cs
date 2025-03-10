using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChestSpawner : MonoBehaviour
{
    public GameObject chestPrefab;              // Prefab del cofre
    public Transform player;                   // Referencia al transform del jugador
    public float spawnInterval = 10f;          // Intervalo de aparición en segundos
    public float minDistance = 30f;            // Distancia mínima para spawnear el cofre
    public float maxDistance = 60f;            // Distancia máxima para spawnear el cofre

    public Buffs buffsManager;                 // Referencia directa al script Buffs

    public GameObject pauseMenuUI;             // Panel para mostrar los botones
    public Button[] optionButtons;             // Botones para seleccionar buffs
    public TextMeshProUGUI[] optionTexts;      // Textos para los botones de buffs

    private bool isPaused = false;
    private Collider2D currentChest;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnChest), 0f, spawnInterval);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentChest != null)
            {
                Debug.Log("Has abierto el cofre");
                PauseGameWithOptions();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Chest"))
        {
            currentChest = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == currentChest)
        {
            currentChest = null;
        }
    }

    public void SpawnChest()
    {
        if (chestPrefab == null || player == null)
        {
            Debug.LogWarning("Falta asignar el prefab del cofre o la referencia al jugador.");
            return;
        }

        float distance = Random.Range(minDistance, maxDistance);
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 spawnPosition = player.position + (Vector3)(randomDirection * distance);

        Instantiate(chestPrefab, spawnPosition, Quaternion.identity);
    }

    void PauseGameWithOptions()
    {
        if (buffsManager == null)
        {
            Debug.LogWarning("El script Buffs no está asignado. Arrástralo directamente al Inspector.");
            return;
        }

        Time.timeScale = 0f;
        isPaused = true;
        pauseMenuUI.SetActive(true);

        buffsManager.ShowBuffOptions();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenuUI.SetActive(false);
    }
}


