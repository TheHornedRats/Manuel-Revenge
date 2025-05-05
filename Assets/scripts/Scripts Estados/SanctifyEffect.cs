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
            Debug.Log($"{enemyHealth.name} ha sido santificado. Da�o reducido a {enemyAttack.damage}");
        }
        else
        {
            Debug.LogWarning($"[Santificaci�n] {enemyHealth.name} NO tiene EnemyAttack.");
        }

        if (sanctifyEffectPrefab != null)
        {
            sanctifyEffectInstance = Instantiate(sanctifyEffectPrefab, enemyHealth.transform.position, Quaternion.identity, enemyHealth.transform);
            Debug.Log("[Santificaci�n] Prefab de part�culas instanciado.");
        }
        else
        {
            Debug.LogWarning("[Santificaci�n] No se ha asignado un prefab visual para la santificaci�n.");
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
        EnemyAttack enemyAttack = enemyHealth.GetComponent<EnemyAttack>();
        if (enemyAttack != null && originalDamage > 0)
        {
            enemyAttack.damage = originalDamage;
            Debug.Log($"{enemyHealth.name} ha recuperado su da�o original: {originalDamage}");
        }

        if (sanctifyEffectInstance != null)
        {
            Destroy(sanctifyEffectInstance);
        }
    }
}
