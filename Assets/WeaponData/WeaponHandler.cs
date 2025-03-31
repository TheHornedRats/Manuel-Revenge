using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public List<WeaponData> weapons = new List<WeaponData>();
    public WeaponData weaponData;

    private Dictionary<string, int> weaponLevels = new Dictionary<string, int>();

    public void ActivateOrUpgradeWeapon(WeaponData weaponData)
    {
        if (weapons.Contains(weaponData))
        {
            //  Asegúrate de que existe en el diccionario antes de incrementar
            if (!weaponLevels.ContainsKey(weaponData.weaponName))
                weaponLevels[weaponData.weaponName] = 1;

            weaponLevels[weaponData.weaponName]++;
            int newLevel = weaponLevels[weaponData.weaponName];
            weaponData.InitWeapon(newLevel);
            Debug.Log($"{weaponData.weaponName} sube a nivel {newLevel}");
        }
        else
        {
            weapons.Add(weaponData);
            weaponLevels[weaponData.weaponName] = 1;
            weaponData.InitWeapon(1);
            Debug.Log($"{weaponData.weaponName} activada en nivel 1");
        }
    }


    private void Update()
    {
        foreach (WeaponData weaponData in weapons)
        {
            if (weaponData != null)
            {
                weaponData.UpdateWeapon(transform.position);
            }
        }
    }
}

