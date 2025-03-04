using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public List<WeaponData> weapons = new List<WeaponData>();
    public WeaponData currentWeapon;
    public string weaponName;
    private float damage;
    private float cooldown;
    private int level = 1;
    private float lastAttackTime;

    private void Start()
    {
       
      

        foreach (WeaponData weaponData in weapons)
        {
            if (weaponData != null) { 
                weaponData.InitWeapon(1);
            }
        }
    }

    private void Update()
    {
        foreach (WeaponData weaponData in weapons)
        {
            if(weaponData != null)
            {
                weaponData.UpdateWeapon(transform.position);
            }
            
        }
    }

    public void UpgradeWeapon(int index)
    {
        currentWeapon = weapons[index];
        if (level < currentWeapon.maxLevel)
        {
            level++;
            damage *= currentWeapon.damageIncreasePerLevel;
            cooldown *= currentWeapon.cooldownReductionPerLevel;
            Debug.Log(currentWeapon.weaponName + " mejorado al nivel " + level);
        }
    }
}

