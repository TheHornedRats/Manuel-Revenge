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
        trucosMenuUI.SetActive(false);
        isOpen = false;
        isPaused = false;
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ToggleMenu();
        }
    }

    // Método público para usar desde el botón UI
    public void ToggleMenu()
    {
        if (isOpen)
        {
            trucosMenuUI.SetActive(false);
            isOpen = false;
            Time.timeScale = 1f;
            isPaused = false;
        }
        else
        {
            trucosMenuUI.SetActive(true);
            isOpen = true;
            Time.timeScale = 0f;
            isPaused = true;
        }
    }
}
