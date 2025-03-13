using UnityEngine;

public class WeaponUnlock : MonoBehaviour
{
    // Referencias a los prefabs de las armas
    public GameObject fireballPrefab;
    public GameObject javelinPrefab;
    public GameObject crucifixPrefab;

    private GameObject currentWeapon;

    void Start()
    {
        // Asegura que las armas estén desactivadas al inicio
        DeactivateAllWeapons();
    }

    void Update()
    {
        // Detecta las teclas numéricas 1, 2 y 3
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateWeapon(fireballPrefab);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateWeapon(javelinPrefab);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActivateWeapon(crucifixPrefab);
        }
    }

    // Cambié esto a public para que sea accesible desde otros scripts
    public void ActivateWeapon(GameObject weaponPrefab)
    {
        // Desactiva la arma actual si hay alguna activa
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(false);
        }

        // Si la nueva arma está desactivada, la activa
        if (weaponPrefab != null && !weaponPrefab.activeSelf)
        {
            weaponPrefab.SetActive(true);
            currentWeapon = weaponPrefab;  // Establece la nueva arma como la actual
        }
    }

    void DeactivateAllWeapons()
    {
        // Desactiva todas las armas al inicio
        if (fireballPrefab != null) fireballPrefab.SetActive(false);
        if (javelinPrefab != null) javelinPrefab.SetActive(false);
        if (crucifixPrefab != null) crucifixPrefab.SetActive(false);
    }
}
