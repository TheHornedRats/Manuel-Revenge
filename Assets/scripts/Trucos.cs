using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Trucos : MonoBehaviour
{
    public Button changeHealthButton;
    public Button addScoreButton;
    public Button spawnBoss1Button;
    public Button spawnBoss2Button;
    public Button spawnBoss3Button;

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
    private PlayerMovement playerMovement;

    public GameObject boss1Prefab;
    public GameObject boss2Prefab;
    public GameObject boss3Prefab;

    void Start()
    {
        changeHealthButton.onClick.AddListener(AddHealth);
        addScoreButton.onClick.AddListener(AddXP);
        spawnBoss1Button.onClick.AddListener(SpawnBoss1);
        spawnBoss2Button.onClick.AddListener(SpawnBoss2);
        spawnBoss3Button.onClick.AddListener(SpawnBoss3);

        scoreManager = ScoreManager.instance;

        if (playerTransform != null)
        {
            playerHealth = playerTransform.GetComponent<PlayerHealth>();
            playerMovement = playerTransform.GetComponent<PlayerMovement>();

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

    public void AddHealth()
    {
        if (playerHealth != null)
        {
            playerHealth.Heal(100);
            healthText.text = "¡Added Health!";
            healthText.gameObject.SetActive(true);
            StartCoroutine(HideTextAfterSeconds(healthText, 2f));
        }
    }

    public void AddSpeed()
    {
        if (playerMovement != null)
        {
            playerMovement.speed += 2f;
            timeText.text = $"¡Speed increased to {playerMovement.speed}!";
            timeText.gameObject.SetActive(true);
            StartCoroutine(HideTextAfterSeconds(timeText, 2f));
        }
    }


    public void AddXP()
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
            enemyText.text = "¡Enemy Spawned!";
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
        float angle = Random.Range(75f, 500f) * Mathf.Deg2Rad;
        float distance = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector2 spawnOffset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
        return (Vector2)playerTransform.position + spawnOffset;
    }

    IEnumerator HideTextAfterSeconds(TMP_Text textElement, float delay)
    {
        yield return new WaitForSeconds(delay);
        textElement.gameObject.SetActive(false);
    }

    public void SpawnBoss1()
    {
        if (boss1Prefab != null && playerTransform != null)
        {
            Vector2 spawnPosition = GetSpawnPosition();
            Instantiate(boss1Prefab, spawnPosition, Quaternion.identity);
            enemyText.text = "¡Boss 1 Spawned!";
            enemyText.gameObject.SetActive(true);
            StartCoroutine(HideTextAfterSeconds(enemyText, 2f));
        }
        else
        {
            Debug.LogError("Boss1 Prefab o PlayerTransform no asignado.");
        }
    }

    public void SpawnBoss2()
    {
        if (boss2Prefab != null && playerTransform != null)
        {
            Vector2 spawnPosition = GetSpawnPosition();
            Instantiate(boss2Prefab, spawnPosition, Quaternion.identity);
            enemyText.text = "¡Boss 2 Spawned!";
            enemyText.gameObject.SetActive(true);
            StartCoroutine(HideTextAfterSeconds(enemyText, 2f));
        }
        else
        {
            Debug.LogError("Boss2 Prefab o PlayerTransform no asignado.");
        }
    }

    public void SpawnBoss3()
    {
        if (boss3Prefab != null && playerTransform != null)
        {
            Vector2 spawnPosition = GetSpawnPosition();
            Instantiate(boss3Prefab, spawnPosition, Quaternion.identity);
            enemyText.text = "¡Boss 3 Spawned!";
            enemyText.gameObject.SetActive(true);
            StartCoroutine(HideTextAfterSeconds(enemyText, 2f));
        }
        else
        {
            Debug.LogError("Boss3 Prefab o PlayerTransform no asignado.");
        }
    }

    void HandleCommand(string command)
    {
        command = command.ToLower().Trim();

        if (command.StartsWith("add_health_"))
        {
            string valueStr = command.Substring("add_health_".Length);
            if (int.TryParse(valueStr, out int amount) && playerHealth != null)
            {
                playerHealth.Heal(amount);
                healthText.text = $"¡Added {amount} Health!";
                healthText.gameObject.SetActive(true);
                StartCoroutine(HideTextAfterSeconds(healthText, 2f));
            }
        }
        else if (command.StartsWith("max_health_"))
        {
            string valueStr = command.Substring("max_health_".Length);
            if (int.TryParse(valueStr, out int newMax) && playerHealth != null)
            {
                playerHealth.maxHealth = newMax;
                playerHealth.Heal(0); // para actualizar UI
                healthText.text = $"¡Max Health set to {newMax}!";
                healthText.gameObject.SetActive(true);
                StartCoroutine(HideTextAfterSeconds(healthText, 2f));
            }
        }
        else if (command.StartsWith("set_speed_"))
        {
            string valueStr = command.Substring("set_speed_".Length);
            if (float.TryParse(valueStr, out float newSpeed) && playerMovement != null)
            {
                playerMovement.speed = newSpeed;
                timeText.text = $"¡Speed set to {newSpeed}!";
                timeText.gameObject.SetActive(true);
                StartCoroutine(HideTextAfterSeconds(timeText, 2f));
            }
        }
        else if (command.StartsWith("add_xp_"))
        {
            string valueStr = command.Substring("add_xp_".Length);
            if (int.TryParse(valueStr, out int xpAmount) && scoreManager != null)
            {
                scoreManager.AddScore(xpAmount);
                scoreText.text = $"¡XP +{xpAmount}!";
                scoreText.gameObject.SetActive(true);
                StartCoroutine(HideTextAfterSeconds(scoreText, 2f));
            }
        }
        else
        {
            switch (command)
            {
                case "health":
                    AddHealth();
                    break;
                case "score":
                    AddXP();
                    break;
                case "speed":
                    AddSpeed();
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
                case "boss1":
                    SpawnBoss1();
                    break;
                case "boss2":
                    SpawnBoss2();
                    break;
                case "boss3":
                    SpawnBoss3();
                    break;
                default:
                    Debug.Log("Comando no reconocido: " + command);
                    break;
            }
        }

        commandInput.text = "";
        commandInput.ActivateInputField();
    }
}
