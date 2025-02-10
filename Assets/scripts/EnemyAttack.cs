using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage;
    public float damageInterval = 1f; // Tiempo en segundos entre cada da�o
    private bool isAttacking = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(DamageOverTime(collision.GetComponent<PlayerHealth>()));
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
}
