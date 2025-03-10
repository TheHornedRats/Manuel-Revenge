using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    public float duration; // Duraci�n total del efecto
    protected EnemyHealth enemyHealth;
    protected float elapsedTime = 0f; // Tiempo transcurrido desde que el estado se aplic�

    public void ApplyEffect(EnemyHealth target)
    {
        enemyHealth = target;
        OnEffectStart(); // Ejecutar l�gica de inicio del efecto
    }

    protected virtual void Update()
    {
        elapsedTime += Time.deltaTime;
        OnEffectUpdate(); // Ejecutar l�gica del efecto en tiempo real

        // Si el efecto ha durado el tiempo completo, eliminarlo
        if (elapsedTime >= duration)
        {
            OnEffectEnd();
            Destroy(this);
        }
    }

    // M�todos virtuales que cada estado puede sobrescribir
    protected virtual void OnEffectStart() { }  // Se ejecuta cuando el estado comienza
    protected virtual void OnEffectUpdate() { } // Se ejecuta cada frame
    protected virtual void OnEffectEnd() { }    // Se ejecuta cuando el efecto termina
}
