using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Trucos : MonoBehaviour
{
    public Button changeHealthButton;
    public Button addScoreButton;
    public Button spawnEnemyButton;
    public Button slowTimeButton;
    public Button immortalityButton;

    public GameObject enemigoPrefab;
    public Transform playerTransform;

    public TMP_Text healthText;
    public TMP_Text scoreText;
    public TMP_Text enemyText;
    public TMP_Text timeText;
    public TMP_Text immortalityText;

    public TMP_InputField commandInput;

    public float minSpawnDistance = 3f;
    public float maxSpawnDistance = 6f;

    private bool timeReduced;
    private bool isImmortal = false;
    private ScoreManager scoreManager;
    private PlayerHealth playerHealth;

    void Start()
    {
        changeHealthButton.onClick.AddListener(AddHealth);
        addScoreButton.onClick.AddListener(OnAddScoreButtonClick);
        spawnEnemyButton.onClick.AddListener(OnSpawnEnemyButtonClick);
        slowTimeButton.onClick.AddListener(SlowDownTime);
        immortalityButton.onClick.AddListener(ActivateImmortality);

        scoreManager = ScoreManager.instance;

        if (playerTransform != null)
        {
            playerHealth = playerTransform.GetComponent<PlayerHealth>();
            if (playerHealth == null)
                Debug.LogError("No se encontró el componente PlayerHealth en el jugador.");
        }

        if (commandInput != null)
        {
            commandInput.onSubmit.AddListener(HandleCommand);
        }

        healthText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        enemyText.gameObject.SetActive(false);
        timeText.gameObject.SetActive(false);
        immortalityText.gameObject.SetActive(false);
    }

    void AddHealth()
    {
        if (playerHealth != null)
        {
            playerHealth.Heal(100);
            healthText.text = "¡Added Health!";
            healthText.gameObject.SetActive(true);
            StartCoroutine(HideTextAfterSeconds(healthText, 2f));
        }
    }

    void OnAddScoreButtonClick()
    {
        if (scoreManager != null)
        {
            scoreManager.AddScore(1000);
            scoreText.text = "¡Added Score!";
            scoreText.gameObject.SetActive(true);
            StartCoroutine(HideTextAfterSeconds(scoreText, 2f));
        }
        else
        {
            Debug.LogError("No se ha encontrado el ScoreManager.");
        }
    }

    void OnSpawnEnemyButtonClick()
    {
        if (enemigoPrefab != null && playerTransform != null)
        {
            Vector2 spawnPosition = GetSpawnPosition();
            Instantiate(enemigoPrefab, spawnPosition, Quaternion.identity);
            enemyText.text = "¡Enemy Spanwed!";
            enemyText.gameObject.SetActive(true);
            StartCoroutine(HideTextAfterSeconds(enemyText, 2f));
        }
        else
        {
            Debug.LogError("No se ha asignado el prefab del enemigo o el Transform del jugador.");
        }
    }

    void SlowDownTime()
    {
        timeReduced = !timeReduced;

        if (timeReduced)
        {
            Time.timeScale = 0.5f;
            timeText.text = "SLOWED Time";
            timeText.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            timeText.gameObject.SetActive(false);
        }
    }

    void ActivateImmortality()
    {
        if (playerTransform != null && playerHealth != null)
        {
            BoxCollider2D boxCollider = playerTransform.GetComponent<BoxCollider2D>();
            if (boxCollider != null)
                boxCollider.enabled = !isImmortal;

            if (!isImmortal)
            {
                playerHealth.Heal(999);
                immortalityText.text = "¡Immortal mode activated!";
            }
            else
            {
                immortalityText.text = "¡Immortal mode off!";
            }

            immortalityText.gameObject.SetActive(true);
            StartCoroutine(HideTextAfterSeconds(immortalityText, 2f));

            isImmortal = !isImmortal;
        }
    }

    Vector2 GetSpawnPosition()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float distance = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector2 spawnOffset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
        return (Vector2)playerTransform.position + spawnOffset;
    }

    IEnumerator HideTextAfterSeconds(TMP_Text textElement, float delay)
    {
        yield return new WaitForSeconds(delay);
        textElement.gameObject.SetActive(false);
    }

    void HandleCommand(string command)
    {
        command = command.ToLower().Trim();

        switch (command)
        {
            case "health":
                AddHealth();
                break;

            case "score":
                OnAddScoreButtonClick();
                break;

            case "enemy":
                OnSpawnEnemyButtonClick();
                break;

            case "time":
                SlowDownTime();
                break;

            case "immortality":
                ActivateImmortality();
                break;

            default:
                Debug.Log("Comando no reconocido: " + command);
                break;
        }

        commandInput.text = "";
        commandInput.ActivateInputField();
    }
}
