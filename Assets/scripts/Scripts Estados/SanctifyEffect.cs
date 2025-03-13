using UnityEngine;

public class SanctifyEffect : StatusEffect
{
    // Multiplicador para reducir el daño del enemigo (70% del original)
    private float reductionMultiplier = 0.7f;
    // Porcentaje de curación para el jugador (10% del daño recibido)
    private float healPercentage = 0.1f;
    // Almacenamos el daño original para restaurarlo al finalizar el efecto
    private int originalDamage = -1;

    protected override void OnEffectStart()
    {
        if (enemyHealth == null)
        {
            Debug.LogError("[Santificación] enemyHealth es NULL al iniciar el efecto.");
            Destroy(this);
            return;
        }

        Debug.Log($"[Santificación] Aplicando efecto en {enemyHealth.name}");

        EnemyAttack enemyAttack = enemyHealth.GetComponent<EnemyAttack>();
        if (enemyAttack != null)
        {
            // Almacenamos el daño original solo la primera vez
            if (originalDamage < 0)
            {
                originalDamage = enemyAttack.damage;
            }
            enemyAttack.damage = Mathf.RoundToInt(originalDamage * reductionMultiplier);
            Debug.Log($"{enemyHealth.name} ha sido santificado. Daño reducido a {enemyAttack.damage}");
        }
        else
        {
            Debug.LogWarning($"[Santificación] {enemyHealth.name} NO tiene EnemyAttack.");
        }
    }

    // Se invoca desde EnemyHealth cada vez que el enemigo recibe daño
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
        if (enemyHealth == null)
        {
            Debug.LogError("[Santificación] enemyHealth es NULL al finalizar el efecto.");
            return;
        }

        Debug.Log($"[Santificación] Finalizando efecto en {enemyHealth.name}");

        EnemyAttack enemyAttack = enemyHealth.GetComponent<EnemyAttack>();
        if (enemyAttack != null && originalDamage > 0)
        {
            enemyAttack.damage = originalDamage;
            Debug.Log($"{enemyHealth.name} ha recuperado su fuerza a {originalDamage} de daño.");
        }
    }
}
