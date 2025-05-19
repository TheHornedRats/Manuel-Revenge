using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeathScreenController : MonoBehaviour
{
    public GameObject deathScreen;
    public RectTransform deathOverlayTransform; // El panel rojo que desciende
    public float descendSpeed = 800f;

    public Button restartButton;
    public Button menuButton;

    private bool shouldDescend = false;

    void Start()
    {
        deathScreen.SetActive(false);
        restartButton.onClick.AddListener(RestartGame);
        menuButton.onClick.AddListener(GoToMenu);
    }

    void Update()
    {
        if (shouldDescend)
        {
            deathOverlayTransform.anchoredPosition = Vector2.MoveTowards(
                deathOverlayTransform.anchoredPosition,
                Vector2.zero,
                descendSpeed * Time.unscaledDeltaTime
            );
        }
    }

    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
        deathOverlayTransform.anchoredPosition = new Vector2(0, Screen.height); // empieza arriba
        shouldDescend = true;
        StartCoroutine(FreezeWhenArrived());
    }

    private IEnumerator FreezeWhenArrived()
    {
        while (Vector2.Distance(deathOverlayTransform.anchoredPosition, Vector2.zero) > 0.1f)
        {
            yield return null;
        }

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
