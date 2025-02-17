using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0;
    public int level = 1;
    public int pointsPerLevel = 1000;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public PlayerAttack playerAttack; // Referencia al script PlayerAttack

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        int previousLevel = level; // Guardamos el nivel antes de sumar la puntuación
        score += amount;
        CheckLevelUp(previousLevel);
        UpdateUI();
    }

    private void CheckLevelUp(int previousLevel)
    {
        // Calculamos el nuevo nivel basado en los puntos alcanzados
        level = (score / pointsPerLevel) + 1;

        // Si el nivel ha cambiado, aplicamos cambios al ataque
        if (level > previousLevel)
        {
            Debug.Log("¡Subiste de nivel! Nuevo nivel: " + level);
            if (playerAttack != null)
            {
                playerAttack.LevelUp(level);
            }
        }
    }

    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Puntuación: " + score;
        else
            Debug.LogWarning("ScoreText no asignado en el Inspector.");

        if (levelText != null)
            levelText.text = "Nivel: " + level;
        else
            Debug.LogWarning("LevelText no asignado en el Inspector.");
    }
}
