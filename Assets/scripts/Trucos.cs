using UnityEngine;
using UnityEngine.UI;

public class Trucos : MonoBehaviour
{
    public Button changeHealthButton;
    public Button addScoreButton;
    public Button spawnEnemyButton;
    public Button slowTimeButton;
    public GameObject enemigoPrefab;
    public Transform playerTransform;

    bool timeReduced; // Esta variable mantiene el estado del tiempo

    private ScoreManager scoreManager;

    public float minSpawnDistance = 3f;
    public float maxSpawnDistance = 6f;

    void Start()
    {
        changeHealthButton.onClick.AddListener(TogglePlayerCollider);
        addScoreButton.onClick.AddListener(OnAddScoreButtonClick);
        spawnEnemyButton.onClick.AddListener(OnSpawnEnemyButtonClick);
        slowTimeButton.onClick.AddListener(SlowDownTime);

        scoreManager = ScoreManager.instance;
    }

    void TogglePlayerCollider()
    {
        BoxCollider2D boxCollider = playerTransform.GetComponent<BoxCollider2D>();

        if (boxCollider != null)
        {
            boxCollider.enabled = !boxCollider.enabled;
            Debug.Log("BoxCollider2D del jugador: " + (boxCollider.enabled ? "Activado" : "Desactivado"));
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
            Debug.Log("Se han añadido 1000 puntos.");
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
            Debug.Log("¡Enemigo 4 spawneado lejos del jugador!");
        }
        else
        {
            Debug.LogError("No se ha asignado el prefab del enemigo o el Transform del jugador.");
        }
    }

    Vector2 GetSpawnPosition()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float distance = Random.Range(minSpawnDistance, maxSpawnDistance);

        Vector2 spawnOffset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
        return (Vector2)playerTransform.position + spawnOffset;
    }

    // Nuevo método para alternar la velocidad del tiempo
    void SlowDownTime()
    {
        if (!timeReduced)
        {
            Time.timeScale = 0.5f; // Ralentiza el tiempo a la mitad
            timeReduced = true;
            Debug.Log("El tiempo ahora pasa a la mitad de velocidad.");
        }
        else
        {
            Time.timeScale = 1f; // Restablece la velocidad normal
            timeReduced = false;
            Debug.Log("El tiempo ha vuelto a la velocidad normal.");
        }
    }
}
