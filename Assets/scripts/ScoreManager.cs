using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance { get; private set; }

    public int score = 0;
    public int level = 1;
    public int pointsPerLevel = 100;
    public float levelMultiplier = 1.4f;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI levelText;
    public PlayerAttack playerAttack;

    public Button UpgradeButton1;
    public Button UpgradeButton2;
    public Button UpgradeButton3;

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
        levelUpPanel.ClosePanel();
    }

    public void AddScore(int amount)
    {
        score += amount;
        CheckLevelUp();
        UpdateUI();

        // Disparar evento si hay suscriptores
        OnScoreChanged?.Invoke(score);
    }

    public LevelUpChoose levelUpPanel;

    private void CheckLevelUp()
    {
        while (score >= pointsPerLevel)
        {
            level++;
            score -= pointsPerLevel; // Restar el exceso para permitir subir múltiples niveles si se gana mucho puntaje de golpe
            pointsPerLevel = Mathf.CeilToInt(pointsPerLevel * levelMultiplier); // Aumenta el umbral
            Debug.Log("¡Subiste de nivel! Nuevo nivel: " + level);

            if (playerAttack != null)
            {
                playerAttack.LevelUp(level);
            }

            if (levelUpPanel != null)
            {
                levelUpPanel.ShowPanel();
                Time.timeScale = 0;
                UpgradeButton1.onClick.AddListener(levelUpPanel.ClosePanel);
                UpgradeButton2.onClick.AddListener(levelUpPanel.ClosePanel);
                UpgradeButton3.onClick.AddListener(levelUpPanel.ClosePanel);
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

        if (finalScoreText != null)
            finalScoreText.text = "Puntuación: " + score;
        else
            Debug.LogWarning("ScoreText no asignado en el Inspector.");
    }
}
