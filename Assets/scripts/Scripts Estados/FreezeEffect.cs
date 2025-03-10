using UnityEngine;

public class FreezeEffect : StatusEffect
{
    public float slowAmount = 0.5f; // Reducción de velocidad (50%)
    private EnemyFollow enemyFollow;

    protected override void OnEffectStart()
    {
        enemyFollow = enemyHealth.GetComponent<EnemyFollow>();
        if (enemyFollow != null)
        {
            enemyFollow.speed *= slowAmount;
            Debug.Log(enemyHealth.name + " ha sido congelado, velocidad reducida.");
        }
    }

    protected override void OnEffectEnd()
    {
        if (enemyFollow != null)
        {
            enemyFollow.speed /= slowAmount; // Restaurar velocidad
            Debug.Log(enemyHealth.name + " ya no está congelado.");
        }
    }
}
