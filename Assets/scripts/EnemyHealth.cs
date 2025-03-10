using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public float damageMultiplier = 1f;
    public int xpReward = 10; // XP que da este enemigo al morir

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        int finalDamage = Mathf.RoundToInt(Mathf.Clamp(damage * damageMultiplier, 1, maxHealth));
        currentHealth -= finalDamage;

        Debug.Log($"[DAÑO] {name} recibió {finalDamage} de daño. Vida restante: {currentHealth}");

        // Si el enemigo tiene Santificación, curar al jugador
        SanctifyEffect sanctifyEffect = GetComponent<SanctifyEffect>();
        if (sanctifyEffect != null)
        {
            HealPlayer(finalDamage);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    private void Die()
    {
        Debug.Log(name + " ha sido derrotado.");

        // Agregar XP al ScoreManager
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.AddXP(xpReward);
        }
        else
        {
            Debug.LogError("ScoreManager no encontrado en la escena.");
        }

        Destroy(gameObject);
    }

    private void HealPlayer(int damageDealt)
    {
        float healPercentage = 0.1f;
        int healAmount = Mathf.RoundToInt(damageDealt * healPercentage);

        if (healAmount > 0)
        {
            PlayerHealth player = FindObjectOfType<PlayerHealth>();
            if (player != null)
            {
                player.Heal(healAmount);
                Debug.Log($"El jugador se cura {healAmount} de vida gracias a la Santificación.");
            }
        }
    }
}
