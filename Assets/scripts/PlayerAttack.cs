using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons; // Importamos el namespace donde están las armas

public class PlayerAttack : MonoBehaviour
{
    [Header("Lista de Armas")]

    [Header("Ajustes de Ataque a Distancia")]
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float baseFireballSpeed = 5f;
    public int baseFireballDamage = 10;
    public float baseFireballExplosionRadius = 2f;
    public LayerMask enemyLayer;
    public GameObject explosionEffect;

    [Header("Cooldown del Ataque")]
    WeaponHandler weaponHandler;
    public int playerLevel = 1;

    private void Start()
    {
        weaponHandler = GetComponent<WeaponHandler>();
    }

    void Update()
    {
       
    }

    public void LevelUp(int newLevel)
    {
        playerLevel = newLevel;
        Debug.Log("Nuevo nivel alcanzado: " + playerLevel);
    }
}
