using UnityEngine;
using UnityEngine.UI;

public class LevelUpPanel : MonoBehaviour
{
    public GameObject panel;
    public Button closeButton;

    private void Start()
    {
        panel.SetActive(false); // Asegura que inicia desactivado
    }

    private void Update()
    {
        if (panel.activeSelf && Input.GetKeyDown(KeyCode.X))
        {
            HidePanel();
        }
    }

    public void ShowPanel()
    {
        panel.SetActive(true);
    }

    public void HidePanel()
    {
        panel.SetActive(false);
    }
}
