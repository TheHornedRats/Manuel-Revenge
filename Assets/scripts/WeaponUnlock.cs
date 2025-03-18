using UnityEngine;

public class WeaponUnlock : MonoBehaviour
{
    // Referencias a los prefabs de las armas
    public GameObject swordPrefab;    // Espada
    public GameObject fireballPrefab; // Fireball
    public GameObject crucifixPrefab; // Crucifijo
    public GameObject javelinPrefab;  // Javalina
    public GameObject weapon5Prefab;  // Test

    private GameObject currentWeapon;

    void Start()
    {
        // Asegura que todas las armas estén desactivadas al inicio
        DeactivateAllWeapons();
    }

    void Update()
    {
        // Detecta las teclas numéricas 1 a 5 para activar las armas
        if (Input.GetKeyDown(KeyCode.Alpha1)) ActivateWeapon(swordPrefab);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) ActivateWeapon(fireballPrefab);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) ActivateWeapon(crucifixPrefab);
        else if (Input.GetKeyDown(KeyCode.Alpha4)) ActivateWeapon(javelinPrefab);
        else if (Input.GetKeyDown(KeyCode.Alpha5)) ActivateWeapon(weapon5Prefab);
    }

    public void ActivateWeapon(GameObject weaponPrefab)
    {
        // Desactiva la arma actual si hay alguna activa
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(false);
        }

        // Activa la nueva arma si está disponible
        if (weaponPrefab != null && !weaponPrefab.activeSelf)
        {
            weaponPrefab.SetActive(true);
            currentWeapon = weaponPrefab;
        }
    }

    void DeactivateAllWeapons()
    {
        // Desactiva todas las armas al inicio
        if (swordPrefab != null) swordPrefab.SetActive(false);
        if (fireballPrefab != null) fireballPrefab.SetActive(false);
        if (crucifixPrefab != null) crucifixPrefab.SetActive(false);
        if (javelinPrefab != null) javelinPrefab.SetActive(false);
        if (weapon5Prefab != null) weapon5Prefab.SetActive(false);
    }
}
