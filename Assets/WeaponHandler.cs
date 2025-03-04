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
        Debug.Log("Hola");

        if (currentWeapon != null)
        {
            weaponName = currentWeapon.weaponName;
            damage = currentWeapon.baseDamage;
            cooldown = currentWeapon.baseCooldown;

            Debug.Log($"WeaponData asignado: {currentWeapon.weaponName}, Cooldown: {currentWeapon.baseCooldown}");

            // Si el cooldown es 0, forzar un valor correcto
            if (cooldown <= 0)
            {
                Debug.LogWarning($"El cooldown del arma {weaponName} estaba en {cooldown}. Se fuerza a 1.5");
                cooldown = 1.5f;
            }

            currentWeapon.InitWeapon(1);
        }
        else
        {
            Debug.LogError("WeaponData NO asignado en " + gameObject.name);
        }

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

