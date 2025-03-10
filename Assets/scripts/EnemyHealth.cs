using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public float damageMultiplier = 2f; 

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        int finalDamage = Mathf.RoundToInt(damage * damageMultiplier);
        Debug.Log($"[DAÑO] {name} recibió {finalDamage} de daño. (Base: {damage}, Multiplicador: {damageMultiplier}). Vida restante: {currentHealth - finalDamage}");

        currentHealth -= finalDamage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
        {
            ScoreManager.Instance.AddScore(10);
            Destroy(gameObject);
        }

    }

