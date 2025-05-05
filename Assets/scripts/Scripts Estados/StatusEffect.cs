using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    public float duration;
    protected EnemyHealth enemyHealth;
    protected float elapsedTime = 0f;

    public void ApplyEffect(EnemyHealth target)
    {
        if (target == null)
        {
            Debug.LogError("ERROR: Intento de aplicar efecto a un EnemyHealth NULL.");
            return;
        }

        enemyHealth = target;
        Debug.Log($"{this.GetType().Name} aplicado correctamente a {enemyHealth.name}");

        //  Ahora se llama el inicio real del efecto
        OnEffectStart();
    }

    protected virtual void Update()
    {
        elapsedTime += Time.deltaTime;

        OnEffectUpdate();

        if (this is DamageOverTimeEffect dotEffect)
        {
            dotEffect.ApplyDamageTick();
        }
    }

    // Métodos sobrescribibles
    protected virtual void OnEffectStart() { }
    protected virtual void OnEffectUpdate() { }
    protected virtual void OnEffectEnd() { }
}
