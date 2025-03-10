using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public List<WeaponData> weapons = new List<WeaponData>();

    private void Start()
    {
        if (weapons.Count == 0)
        {
            Debug.LogError("No hay armas en WeaponHandler.");
            return;
        }

        foreach (WeaponData weapon in weapons)
        {
            if (weapon != null)
            {
                Debug.Log("Arma añadida: " + weapon.weaponName);
                weapon.InitWeapon(1);
            }
            else
            {
                Debug.LogError("Hay un espacio vacío en la lista de armas.");
            }
        }
    }

    private void Update()
    {
        foreach (WeaponData weapon in weapons)
        {
            if (weapon != null)
            {
                weapon.UpdateWeapon(transform.position + Vector3.right);
            }
        }
    }
}
