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

    public GameObject enemigoPrefab;
    public Transform playerTransform;

    public TMP_Text healthText;
    public TMP_Text scoreText;
    public TMP_Text enemyText;
    public TMP_Text timeText;

    public TMP_InputField commandInput; // <- NUEVO: consola de comandos

    public float minSpawnDistance = 3f;
    public float maxSpawnDistance = 6f;

    private bool timeReduced;
    private ScoreManager scoreManager;
    private PlayerHealth playerHealth;

    void Start()
    {
        changeHealthButton.onClick.AddListener(TogglePlayerCollider);
        addScoreButton.onClick.AddListener(OnAddScoreButtonClick);
        spawnEnemyButton.onClick.AddListener(OnSpawnEnemyButtonClick);
        slowTimeButton.onClick.AddListener(SlowDownTime);

        scoreManager = ScoreManager.instance;

        if (playerTransform != null)
        {
            playerHealth = playerTransform.GetComponent<PlayerHealth>();
            if (playerHealth == null)
                Debug.LogError("No se encontró el componente PlayerHealth en el jugador.");
        }

        if (commandInput != null)
        {
            commandInput.onSubmit.AddListener(HandleCommand); // <- NUEVO
        }

        healthText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        enemyText.gameObject.SetActive(false);
        timeText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)) OnAddScoreButtonClick();
        if (Input.GetKeyDown(KeyCode.U)) TogglePlayerCollider();
        if (Input.GetKeyDown(KeyCode.I)) OnSpawnEnemyButtonClick();
        if (Input.GetKeyDown(KeyCode.O)) SlowDownTime();
    }

    void TogglePlayerCollider()
    {
        BoxCollider2D boxCollider = playerTransform.GetComponent<BoxCollider2D>();

        if (boxCollider != null)
        {
            boxCollider.enabled = !boxCollider.enabled;

            if (!boxCollider.enabled)
            {
                healthText.text = "Colisión DESACTIVADA";
                healthText.gameObject.SetActive(true);

                if (playerHealth != null)
                {
                    playerHealth.Heal(4);
                }
                else
                {
                    Debug.LogError("playerHealth no está asignado.");
                }
            }
            else
            {
                healthText.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.LogError("No se encontró un BoxCollider2D en el jugador.");
        }
    }

    void OnAddScoreButtonClick()
    {
        if (scoreManager != null)
        {
            scoreManager.AddScore(1000);
            scoreText.text = "¡Puntos añadidos!";
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
            enemyText.text = "¡Enemigo spawneado!";
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
            timeText.text = "Tiempo LENTO";
            timeText.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            timeText.gameObject.SetActive(false);
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

    // NUEVO: Procesador de comandos
    void HandleCommand(string command)
    {
        command = command.ToLower().Trim();

        switch (command)
        {
            case "vida":
                if (playerHealth != null)
                {
                    playerHealth.Heal(10);
                    healthText.text = "¡Vida restaurada!";
                    healthText.gameObject.SetActive(true);
                    StartCoroutine(HideTextAfterSeconds(healthText, 2f));
                }
                break;

            case "score":
                OnAddScoreButtonClick();
                break;

            case "enemigo":
                OnSpawnEnemyButtonClick();
                break;

            case "tiempo":
                SlowDownTime();
                break;

            default:
                Debug.Log("Comando no reconocido: " + command);
                break;
        }

        commandInput.text = "";
        commandInput.ActivateInputField(); // Reactivar para siguiente input
    }
}
