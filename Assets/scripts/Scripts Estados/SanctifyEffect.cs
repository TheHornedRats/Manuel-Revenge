using UnityEngine;

public class SanctifyEffect : StatusEffect
{
    // Multiplicador para reducir el da�o del enemigo
    private float reductionMultiplier = 0.7f;
    // Porcentaje de curaci�n para el jugador
    private float healPercentage = 0.1f;
    // Almacenamos el da�o original para restaurarlo al finalizar el efecto
    private int originalDamage = -1;




    protected override void OnEffectStart()
    {
        // L�gica de reducci�n de da�o del enemigo
        EnemyAttack enemyAttack = enemyHealth.GetComponent<EnemyAttack>();
        if (enemyAttack != null)
        {
            if (originalDamage < 0)
                originalDamage = enemyAttack.damage;

            enemyAttack.damage = Mathf.RoundToInt(originalDamage * reductionMultiplier);
            Debug.Log($"{enemyHealth.name} ha sido santificado. Da�o reducido a {enemyAttack.damage}");
        }
        else
        {
            Debug.LogWarning($"[Santificaci�n] {enemyHealth.name} NO tiene EnemyAttack.");
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
                Debug.Log($"El jugador se cura {healAmount} de vida gracias a la Santificaci�n.");
            }
            else
            {
                Debug.LogWarning("[Santificaci�n] No se encontr� un objeto PlayerHealth en la escena.");
            }
        }
    }

    protected override void OnEffectEnd()
    {
        // Restaurar el da�o original
        EnemyAttack enemyAttack = enemyHealth.GetComponent<EnemyAttack>();
        if (enemyAttack != null && originalDamage > 0)
        {
            enemyAttack.damage = originalDamage;
            Debug.Log($"{enemyHealth.name} ha recuperado su da�o original: {originalDamage}");
        }
    }
}
