using UnityEngine;
using System.Collections.Generic;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;
    private List<StatusEffect> activeEffects = new List<StatusEffect>();

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(name + " tomó " + damage + " de daño. Salud restante: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    public void ApplyEffect(StatusEffect effect)
    {
        StatusEffect newEffect = gameObject.AddComponent(effect.GetType()) as StatusEffect;
        if (newEffect != null)
        {
            newEffect.duration = effect.duration;
            newEffect.ApplyEffect(this);
            activeEffects.Add(newEffect);
            Debug.Log(name + " recibió efecto de " + effect.GetType().Name);
        }
    }

    private void Die()
    {
        Debug.Log(name + " ha muerto.");
        Destroy(gameObject);
    }
}
