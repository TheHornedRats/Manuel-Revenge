using UnityEngine;

public class SanctifyEffect : StatusEffect
{
    // Multiplicador para reducir el daño del enemigo
    private float reductionMultiplier = 0.7f;
    // Porcentaje de curación para el jugador
    private float healPercentage = 0.1f;
    // Almacenamos el daño original para restaurarlo al finalizar el efecto
    private int originalDamage = -1;




    protected override void OnEffectStart()
    {
        // Lógica de reducción de daño del enemigo
        EnemyAttack enemyAttack = enemyHealth.GetComponent<EnemyAttack>();
        if (enemyAttack != null)
        {
            if (originalDamage < 0)
                originalDamage = enemyAttack.damage;

            enemyAttack.damage = Mathf.RoundToInt(originalDamage * reductionMultiplier);
            Debug.Log($"{enemyHealth.name} ha sido santificado. Daño reducido a {enemyAttack.damage}");
        }
        else
        {
            Debug.LogWarning($"[Santificación] {enemyHealth.name} NO tiene EnemyAttack.");
        }
    }

    
    public void HealPlayer(int damageDealt)
    {
        if (damageDealt <= 0) return;

        int healAmount = Mathf.RoundToInt(damageDealt * healPercentage);
        if (healAmount > 0)
        {
            PlayerHealth player = FindObjectOfType<PlayerHealth>();
            if (player != null)
            {
                player.Heal(healAmount);
                Debug.Log($"El jugador se cura {healAmount} de vida gracias a la Santificación.");
            }
            else
            {
                Debug.LogWarning("[Santificación] No se encontró un objeto PlayerHealth en la escena.");
            }
        }
    }

    protected override void OnEffectEnd()
    {
        // Restaurar el daño original
        EnemyAttack enemyAttack = enemyHealth.GetComponent<EnemyAttack>();
        if (enemyAttack != null && originalDamage > 0)
        {
            enemyAttack.damage = originalDamage;
            Debug.Log($"{enemyHealth.name} ha recuperado su daño original: {originalDamage}");
        }
    }
}
