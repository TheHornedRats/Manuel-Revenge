using UnityEngine;
using UnityEngine.UI;

public class UIActivator : MonoBehaviour
{
    public Button boton;            // Referencia al bot�n
    public GameObject uiElement;    // Elemento de UI a mostrar/ocultar

    void Start()
    {
        // Suscribe el m�todo ToggleUI al evento onClick del bot�n
        if (boton != null)
            boton.onClick.AddListener(ToggleUI);
    }

    void ToggleUI()
    {
        if (uiElement != null)
            uiElement.SetActive(!uiElement.activeSelf);
    }
}
