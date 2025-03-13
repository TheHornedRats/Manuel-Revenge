using UnityEngine;
using UnityEngine.UI;

public class Trucos : MonoBehaviour
{
    public Button changeHealthButton;
    public Button addScoreButton;
    public GameObject manuel;

    private ScoreManager scoreManager;

    void Start()
    {
        // Asegúrate de que el botón para desactivar el BoxCollider tenga un evento asignado
        changeHealthButton.onClick.AddListener(OnButtonClick);

        // Asegúrate de que el botón para añadir puntuación tenga un evento asignado
        addScoreButton.onClick.AddListener(OnAddScoreButtonClick);

        // Obtener la referencia del ScoreManager
        scoreManager = ScoreManager.instance;
    }

    void OnButtonClick()
    {
        // Obtiene el componente BoxCollider2D del GameObject Manuel
        BoxCollider2D boxCollider = manuel.GetComponent<BoxCollider2D>();

        if (boxCollider != null)
        {
            if (boxCollider.enabled)
            {
                // Desactivar el BoxCollider2D
                boxCollider.enabled = false;
                Debug.Log("BoxCollider2D de Manuel desactivado.");
            } else
            {
                boxCollider.enabled = true;
            }
        }
        else
        {
            Debug.LogError("No se encontró un BoxCollider2D en Manuel.");
        }
    }

    void OnAddScoreButtonClick()
    {
        // Verifica si el ScoreManager está asignado
        if (scoreManager != null)
        {
            // Sumar 1000 puntos al score
            scoreManager.AddScore(1000);
            Debug.Log("Se han añadido 1000 puntos.");
        }
        else
        {
            Debug.LogError("No se ha encontrado el ScoreManager.");
        }
    }
}