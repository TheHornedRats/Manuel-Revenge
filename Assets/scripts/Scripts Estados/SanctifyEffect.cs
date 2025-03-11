using UnityEngine;

public class SanctifyEffect : StatusEffect
{
    private float reductionMultiplier = 0.7f; // Reduce el daño del enemigo
    private float healPercentage = 0.1f; // Cura el 10% del daño infligido

    protected override void Update() // Se añade override correctamente si StatusEffect tiene un Update
    {
        if (enemyHealth == null)
        {
            Debug.Log("[Santificación] El enemigo ha muerto, eliminando efecto.");
            Destroy(this);
        }
    }

    protected override void OnEffectStart()
    {
        if (enemyHealth == null)
        {
            Debug.LogWarning("[Santificación] enemyHealth es NULL al iniciar el efecto.");
            return;
        }

        Debug.Log($"[Santificación] Aplicando efecto en {enemyHealth.name}");

        EnemyAttack enemyAttack = enemyHealth.GetComponent<EnemyAttack>();
        if (enemyAttack != null)
        {
            enemyAttack.damage = Mathf.RoundToInt(enemyAttack.damage * reductionMultiplier);
            Debug.Log($"{enemyHealth.name} ha sido santificado. Daño reducido a {enemyAttack.damage}");
        }
        else
        {
            Debug.LogWarning($"[Santificación] {enemyHealth.name} NO tiene EnemyAttack.");
        }
    }

    public void HealPlayer(int damageDealt)
    {
        int healAmount = Mathf.RoundToInt(damageDealt * healPercentage);
        if (healAmount > 0)
        {
            PlayerHealth player = FindObjectOfType<PlayerHealth>();
            if (player != null)
            {
                player.Heal(healAmount);
                Debug.Log($" El jugador se cura {healAmount} de vida gracias a la Santificación.");
            }
        }
    }

    protected override void OnEffectEnd()
    {
        if (enemyHealth == null)
        {
            Debug.LogWarning("[Santificación] enemyHealth es NULL al finalizar el efecto.");
            return;
        }

        Debug.Log($"[Santificación] Finalizando efecto en {enemyHealth.name}");

        EnemyAttack enemyAttack = enemyHealth.GetComponent<EnemyAttack>();
        if (enemyAttack != null)
        {
            enemyAttack.damage = Mathf.RoundToInt(enemyAttack.damage / reductionMultiplier);
            Debug.Log($"{enemyHealth.name} ha recuperado su fuerza.");
        }
    }
}
