using UnityEngine;
using UnityEngine.UI;

public class AbrirMenus : MonoBehaviour
{
    public Button boton;            // Referencia al botón
    public Button botonCerrar;      // Boton para cerrar
    public GameObject uiElement;    // Elemento de UI a mostrar/ocultar

    void Start()
    {
        if (boton != null)
            boton.onClick.AddListener(ToggleUI);

        if (botonCerrar != null)
            botonCerrar.onClick.AddListener(CerrarUI);
    }

    void ToggleUI()
    {
        if (uiElement != null)
            uiElement.SetActive(!uiElement.activeSelf);
    }

    void CerrarUI()
    {
        if (uiElement != null)
            uiElement.SetActive(false);
    }
}
