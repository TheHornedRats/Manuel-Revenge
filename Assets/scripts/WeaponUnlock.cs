using UnityEngine;

public class WeaponActivator : MonoBehaviour
{
    public GameObject fireballPrefab;
    public GameObject javelinPrefab;
    public GameObject crucifixPrefab;

    void Start()
    {
        DeactivateAllWeapons(); // Asegura que todas las armas estén desactivadas al principio
    }

    // Método para desactivar todas las armas
    public void DeactivateAllWeapons()
    {
        if (fireballPrefab != null) fireballPrefab.SetActive(false);
        if (javelinPrefab != null) javelinPrefab.SetActive(false);
        if (crucifixPrefab != null) crucifixPrefab.SetActive(false);
    }

    // Método para activar una arma en función del índice
    public void ActivateWeapon(int index)
    {
        DeactivateAllWeapons(); // Desactiva todas las armas primero

        // Activa la arma correspondiente según el índice
        switch (index)
        {
            case 0:
                if (fireballPrefab != null) fireballPrefab.SetActive(true);
                break;
            case 1:
                if (javelinPrefab != null) javelinPrefab.SetActive(true);
                break;
            case 2:
                if (crucifixPrefab != null) crucifixPrefab.SetActive(true);
                break;
            default:
                Debug.LogWarning("Índice de arma no válido.");
                break;
        }
    }
}
