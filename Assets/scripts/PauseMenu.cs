using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject Settings;

    private RectTransform pauseRectTransform;
    private Vector2 hiddenPosition = new Vector2(-800f, 0); // Ajusta según tu resolución y UI
    private Vector2 visiblePosition = Vector2.zero;

    public float slideDuration = 0.3f;

    private bool isPaused = false;

    void Start()
    {
        pauseRectTransform = pauseMenuUI.GetComponent<RectTransform>();
        pauseRectTransform.anchoredPosition = hiddenPosition;
        pauseMenuUI.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        StartCoroutine(SlideOut());
        Settings.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        StartCoroutine(SlideIn());
        Time.timeScale = 0f;
        isPaused = true;
    }

    IEnumerator SlideIn()
    {
        float elapsed = 0f;
        Vector2 start = hiddenPosition;
        Vector2 end = visiblePosition;

        while (elapsed < slideDuration)
        {
            float t = elapsed / slideDuration;
            pauseRectTransform.anchoredPosition = Vector2.Lerp(start, end, t);
            elapsed += Time.unscaledDeltaTime; // usar deltaTime sin escalado
            yield return null;
        }

        pauseRectTransform.anchoredPosition = end;
    }

    IEnumerator SlideOut()
    {
        float elapsed = 0f;
        Vector2 start = pauseRectTransform.anchoredPosition;
        Vector2 end = hiddenPosition;

        while (elapsed < slideDuration)
        {
            float t = elapsed / slideDuration;
            pauseRectTransform.anchoredPosition = Vector2.Lerp(start, end, t);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        pauseRectTransform.anchoredPosition = end;
        pauseMenuUI.SetActive(false);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
