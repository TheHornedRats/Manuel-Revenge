using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Ajustes de Ataque")]
    public GameObject fireballPrefab; // Prefab de la bola de fuego
    public Transform firePoint; // Punto de aparición del ataque
    public float baseFireballSpeed = 5f; // Velocidad base de la bola de fuego
    public int baseFireballDamage = 10; // Daño base de la bola de fuego
    public float baseFireballExplosionRadius = 2f; // Radio base de la explosión
    public LayerMask enemyLayer; // Capa de los enemigos
    public GameObject explosionEffect; // Prefab de la explosión

    [Header("Cooldown del Ataque")]
    public float attackInterval = 2f; // Tiempo entre ataques
    private float timeSinceLastAttack = 0f;
    private Vector2 lastDirection = Vector2.right; // Dirección inicial del disparo
    public int playerLevel = 1; // Nivel del jugador

    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        // Obtener la dirección en la que se mueve el jugador
        Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (movementInput != Vector2.zero)
        {
            lastDirection = movementInput.normalized;
        }

        if (timeSinceLastAttack >= attackInterval)
        {
            Shoot();
            timeSinceLastAttack = 0f;
        }
    }

    void Shoot()
    {
        if (fireballPrefab == null || firePoint == null)
        {
            Debug.LogError("Fireball Prefab o FirePoint no asignado en PlayerAttack!");
            return;
        }

        Debug.Log("Disparando bola de fuego!");
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
        Attack attack = fireball.GetComponent<Attack>();
        if (attack != null)
        {
            attack.SetDirection(lastDirection);
            attack.speed = baseFireballSpeed;
            attack.damage = baseFireballDamage + (playerLevel * 5); // Aumenta el daño con el nivel
            attack.explosionRadius = baseFireballExplosionRadius + (playerLevel * 0.5f); // Aumenta el radio de la explosión con el nivel
            attack.enemyLayer = enemyLayer;
            attack.explosionEffect = explosionEffect;
        }
    }

    public void LevelUp(int newLevel)
    {
        playerLevel = newLevel;
        Debug.Log("Nuevo nivel alcanzado: " + playerLevel);
    }
}
