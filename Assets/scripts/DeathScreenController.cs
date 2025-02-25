using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cargar escenas

public class DeathScreenController : MonoBehaviour
{
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;

#if UNITY_EDITOR //Esto para el juego en Unity
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); //Y esto para el juego en builds
#endif
    }