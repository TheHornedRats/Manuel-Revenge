using UnityEngine;

public class WeaponUnlock : MonoBehaviour
{
    public GameObject swordPrefab;
    public GameObject fireballPrefab;
    public GameObject crucifixPrefab;
    public GameObject javelinPrefab;
    public GameObject weapon5Prefab;

    private GameObject currentWeapon;

    public void ActivateWeapon(GameObject weaponPrefab)
    {
        if (weaponPrefab == null)
        {
            Debug.LogError("Intentaste activar un arma nula.");
            return;
        }

        // Si el arma ya estaba activa, simplemente mejora su nivel
        if (weaponPrefab.activeSelf)
        {
            Debug.Log("Mejorando arma: " + weaponPrefab.name);
        }
        else
        {
            weaponPrefab.SetActive(true);
            Debug.Log("Activando nueva arma: " + weaponPrefab.name);
        }

        currentWeapon = weaponPrefab;

        WeaponHandler handler = weaponPrefab.GetComponent<WeaponHandler>();
        if (handler != null && handler.weaponData != null)
        {
            int nivel = ScoreManager.instance != null ? ScoreManager.instance.level : 1;
            handler.weaponData.InitWeapon(nivel);
        }
        else
        {
            Debug.LogWarning("El arma no tiene WeaponHandler o WeaponData asignado.");
        }
    }

    public void DeactivateAllWeapons()
    {
        if (swordPrefab != null) swordPrefab.SetActive(false);
        if (fireballPrefab != null) fireballPrefab.SetActive(false);
        if (crucifixPrefab != null) crucifixPrefab.SetActive(false);
        if (javelinPrefab != null) javelinPrefab.SetActive(false);
        if (weapon5Prefab != null) weapon5Prefab.SetActive(false);
    }

    void Start()
    {
        DeactivateAllWeapons();
        ActivateWeapon(swordPrefab); // El personaje empieza siempre con la espada
    }
}
