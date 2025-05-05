using UnityEngine;
using UnityEngine.UI;

public class UIActivator : MonoBehaviour
{
    public Button boton;            // Referencia al botón
    public GameObject uiElement;    // Elemento de UI a mostrar/ocultar

    void Start()
    {
        // Suscribe el método ToggleUI al evento onClick del botón
        if (boton != null)
            boton.onClick.AddListener(ToggleUI);
    }

    void ToggleUI()
    {
        if (uiElement != null)
            uiElement.SetActive(!uiElement.activeSelf);
    }
}
