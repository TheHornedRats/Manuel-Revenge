using UnityEngine;

public class SanctifyEffect : StatusEffect
{
    private float reductionMultiplier = 0.7f; // Reduce el da�o del enemigo
    private float healPercentage = 0.1f; // Cura el 10% del da�o infligido

    protected override void Update() // Se a�ade override correctamente si StatusEffect tiene un Update
    {
        if (enemyHealth == null)
        {
            Debug.Log("[Santificaci�n] El enemigo ha muerto, eliminando efecto.");
            Destroy(this);
        }
    }

    protected override void OnEffectStart()
    {
        if (enemyHealth == null)
        {
            Debug.LogWarning("[Santificaci�n] enemyHealth es NULL al iniciar el efecto.");
            return;
        }

        Debug.Log($"[Santificaci�n] Aplicando efecto en {enemyHealth.name}");

        EnemyAttack enemyAttack = enemyHealth.GetComponent<EnemyAttack>();
        if (enemyAttack != null)
        {
            enemyAttack.damage = Mathf.RoundToInt(enemyAttack.damage * reductionMultiplier);
            Debug.Log($"{enemyHealth.name} ha sido santificado. Da�o reducido a {enemyAttack.damage}");
        }
        else
        {
            Debug.LogWarning($"[Santificaci�n] {enemyHealth.name} NO tiene EnemyAttack.");
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
                Debug.Log($" El jugador se cura {healAmount} de vida gracias a la Santificaci�n.");
            }
        }
    }

    protected override void OnEffectEnd()
    {
        if (enemyHealth == null)
        {
            Debug.LogWarning("[Santificaci�n] enemyHealth es NULL al finalizar el efecto.");
            return;
        }

        Debug.Log($"[Santificaci�n] Finalizando efecto en {enemyHealth.name}");

        EnemyAttack enemyAttack = enemyHealth.GetComponent<EnemyAttack>();
        if (enemyAttack != null)
        {
            enemyAttack.damage = Mathf.RoundToInt(enemyAttack.damage / reductionMultiplier);
            Debug.Log($"{enemyHealth.name} ha recuperado su fuerza.");
        }
    }
}
