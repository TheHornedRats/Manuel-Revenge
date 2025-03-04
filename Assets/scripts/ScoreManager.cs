using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance { get; private set; }

    public int score = 0;
    public int level = 1;
    public int pointsPerLevel = 1000;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public PlayerAttack playerAttack;

    // Evento para notificar cambios en el score
    public delegate void ScoreChanged(int newScore);
    public event ScoreChanged OnScoreChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        int previousLevel = level;
        score += amount;
        CheckLevelUp(previousLevel);
        UpdateUI();

        // Disparar evento si hay suscriptores
        OnScoreChanged?.Invoke(score);
    }

    private void CheckLevelUp(int previousLevel)
    {
        level = (score / pointsPerLevel) + 1;

        if (level > previousLevel)
        {
            Debug.Log("�Subiste de nivel! Nuevo nivel: " + level);
            if (playerAttack != null)
            {
                playerAttack.LevelUp(level);
            }
        }
    }

    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Puntuaci�n: " + score;
        else
            Debug.LogWarning("ScoreText no asignado en el Inspector.");

        if (levelText != null)
            levelText.text = "Nivel: " + level;
        else
            Debug.LogWarning("LevelText no asignado en el Inspector.");
    }
}
