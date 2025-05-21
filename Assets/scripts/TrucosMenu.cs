using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrucosMenu : MonoBehaviour
{
    public GameObject trucosMenuUI;

    private bool isOpen = false;
    private bool isPaused = false;

    void Start()
    {
        trucosMenuUI.SetActive(false); // Asegurar que el menú de pausa esté oculto al inicio
        isOpen = false;
        isPaused = false;
        Time.timeScale = 1f; // Asegurar que el juego comienza en tiempo normal
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (isOpen)
                {trucosMenuUI.SetActive(false);
                isOpen = false;
                Time.timeScale = 1f;
                isPaused = false;
            }
            else
                {trucosMenuUI.SetActive(true);
                isOpen = true;
                Time.timeScale = 0f;
                isPaused = true;
            }
        }
    }
}
