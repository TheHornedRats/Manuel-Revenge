using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrucosMenu : MonoBehaviour
{
    public GameObject trucosMenuUI;

    private bool isOpen = false;

    void Start()
    {
        trucosMenuUI.SetActive(false); // Asegurar que el menú de pausa esté oculto al inicio
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (isOpen)
                {trucosMenuUI.SetActive(false);
                isOpen = false;
            }
            else
                {trucosMenuUI.SetActive(true);
                isOpen = true;
            }
        }
    }
}
