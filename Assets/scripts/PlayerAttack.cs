using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Ajustes de Ataque")]
    public GameObject fireballPrefab; // Prefab de la bola de fuego
    public Transform firePoint; // Punto de aparición del ataque
    public float fireballSpeed = 5f; // Velocidad de la bola de fuego
    public int fireballDamage = 10; // Daño de la bola de fuego
    public float fireballExplosionRadius = 2f; // Radio de la explosión
    public LayerMask enemyLayer; // Capa de los enemigos
    public GameObject explosionEffect; // Prefab de la explosión

    [Header("Cooldown del Ataque")]
    public float attackInterval = 2f; // Tiempo entre ataques
    private float timeSinceLastAttack = 0f;
    private Vector2 lastDirection = Vector2.right; // Dirección inicial del disparo

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
            attack.SetDirection(lastDirection); // Asegurar que Attack tiene este método
            attack.speed = fireballSpeed;
            attack.damage = fireballDamage;
            attack.explosionRadius = fireballExplosionRadius;
            attack.enemyLayer = enemyLayer;
            attack.explosionEffect = explosionEffect;
        }
    }
}
