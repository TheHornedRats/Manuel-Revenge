using UnityEngine;
using System.Collections;

public abstract class StatusEffect : MonoBehaviour
{
    public float duration; // Duraci�n del efecto en segundos
    protected EnemyHealth enemyHealth;

    public void ApplyEffect(EnemyHealth target)
    {
        enemyHealth = target;
        StartCoroutine(EffectCoroutine());
    }

    protected abstract IEnumerator EffectCoroutine();
}
