using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public Button quitButton;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GeneralScene", LoadSceneMode.Single); //Cargar la escena principal y cerrar la otra
    }

    public void QuitGame()
    {
#if UNITY_EDITOR //Esto para el juego en Unity
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); //Y esto para el juego en builds
#endif
    }
}
