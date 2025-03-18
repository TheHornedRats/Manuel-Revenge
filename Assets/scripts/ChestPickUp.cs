using UnityEngine;

public class ChestPickUp : MonoBehaviour
{
    [SerializeField] private GameObject panelUI; // Panel de la UI que se activará
    [SerializeField] private GameObject chest;   // Prefab del objeto que colisionará con el Chest

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto que colisiona es el que asignaste como prefab de Chest
        if (collision.gameObject == chest)
        {
            if (panelUI != null)
            {
                panelUI.SetActive(true); // Activa el panel UI
            }
            else
            {
                Debug.LogWarning("Panel UI no asignado en " + gameObject.name);
            }
        }
    }
}
