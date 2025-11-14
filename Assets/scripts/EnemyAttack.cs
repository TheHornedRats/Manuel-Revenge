using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage;
    public float damageInterval = 1f; // Tiempo en segundos entre cada daño
    private bool isAttacking = false;
    private bool canAttack = true;  // Nuevo flag para controlar si puede atacar

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canAttack) return;  // Si no puede atacar, ignorar

        if (collision.CompareTag("Player"))
        {
            PlayerHealth ph = collision.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                StartCoroutine(DamageOverTime(ph));
            }
            else
            {
                Debug.LogWarning("El objeto con tag 'Player' no tiene PlayerHealth asignado.");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isAttacking = false;
        }
    }

    private IEnumerator DamageOverTime(PlayerHealth playerHealth)
    {
        isAttacking = true;
        while (isAttacking && playerHealth != null)
        {
            playerHealth.TakeDamagePlayer(damage);
            yield return new WaitForSeconds(damageInterval);
        }
    }

    // Nuevo método para desactivar el ataque
    public void DisableAttack()
    {
        canAttack = false;
        isAttacking = false; // Parar cualquier daño en curso
        StopAllCoroutines(); // Parar posibles corutinas activas
    }
}