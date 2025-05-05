using UnityEngine;

public class SanctifyEffect : StatusEffect
{
    private float reductionMultiplier = 0.7f;
    private float healPercentage = 0.1f;
    private int originalDamage = -1;

    public GameObject sanctifyEffectPrefab;
    private GameObject sanctifyEffectInstance;

    protected override void OnEffectStart()
    {
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

        if (sanctifyEffectPrefab != null)
        {
            sanctifyEffectInstance = Instantiate(sanctifyEffectPrefab, enemyHealth.transform.position, Quaternion.identity, enemyHealth.transform);
            Debug.Log("[Santificación] Prefab de partículas instanciado.");
        }
        else
        {
            Debug.LogWarning("[Santificación] No se ha asignado un prefab visual para la santificación.");
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
        EnemyAttack enemyAttack = enemyHealth.GetComponent<EnemyAttack>();
        if (enemyAttack != null && originalDamage > 0)
        {
            enemyAttack.damage = originalDamage;
            Debug.Log($"{enemyHealth.name} ha recuperado su daño original: {originalDamage}");
        }

        if (sanctifyEffectInstance != null)
        {
            Destroy(sanctifyEffectInstance);
        }
    }
}
