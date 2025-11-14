using UnityEngine;
using UnityEngine.UI;

public class MobileToogle : MonoBehaviour
{
    public Toggle toggle;
    public GameObject panelToToggle;

    void Start()
    {
        if (toggle != null && panelToToggle != null)
        {
            // Al inicio desactivamos panel y toggle (toggle desmarcado)
            toggle.isOn = false;           // desmarca el toggle
            panelToToggle.SetActive(false); // oculta el panel

            // Añadimos el listener para que cambie cuando el usuario interactúe
            toggle.onValueChanged.AddListener(OnToggleChanged);
        }
        else
        {
            Debug.LogError("Faltan referencias en ToggleCanvasVisibility.");
        }
    }

    void OnToggleChanged(bool isOn)
    {
        panelToToggle.SetActive(isOn);
    }

    void OnDestroy()
    {
        if (toggle != null)
            toggle.onValueChanged.RemoveListener(OnToggleChanged);
    }
}
