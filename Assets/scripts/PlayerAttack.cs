using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons; // Importamos el namespace donde están las armas

public class PlayerAttack : MonoBehaviour
{
    [Header("Lista de Armas")]
    public List<Weapon> weapons = new List<Weapon>(); // Lista de todas las armas equipadas

    [Header("Ajustes de Ataque a Distancia")]
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float baseFireballSpeed = 5f;
    public int baseFireballDamage = 10;
    public float baseFireballExplosionRadius = 2f;
    public LayerMask enemyLayer;
    public GameObject explosionEffect;

    [Header("Cooldown del Ataque")]
    public int playerLevel = 1;

    void Update()
    {
        foreach (Weapon weapon in weapons)
        {
            weapon.TryAttack();
        }
    }

    public void AddWeapon(Weapon newWeapon)
    {
        weapons.Add(newWeapon);
        Debug.Log("Nueva arma añadida: " + newWeapon.weaponName);
    }

    public void LevelUp(int newLevel)
    {
        playerLevel = newLevel;
        Debug.Log("Nuevo nivel alcanzado: " + playerLevel);
    }
}
