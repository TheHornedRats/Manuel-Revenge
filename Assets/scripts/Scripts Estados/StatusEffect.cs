using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    public float duration;            // Duraci�n total del efecto
    protected EnemyHealth enemyHealth;
    protected float elapsedTime = 0f; // Tiempo transcurrido desde que se aplic� el efecto

  

    public void ApplyEffect(EnemyHealth target)
    {
        if (target == null)
        {
            Debug.LogError(" ERROR: Intento de aplicar efecto a un EnemyHealth NULL.");
            return;
        }

        enemyHealth = target;
        Debug.Log($" {this.GetType().Name} aplicado correctamente a {enemyHealth.name}");

    
    }


   
       

       
    protected virtual void Update()
    {
        elapsedTime += Time.deltaTime;


        OnEffectUpdate();

        // Si el efecto tiene ticks de da�o, aplica en intervalos
        if (this is DamageOverTimeEffect dotEffect)
        {
            dotEffect.ApplyDamageTick();
        }

       
    }


    // M�todos virtuales para que cada efecto pueda sobrescribir su comportamiento
    protected virtual void OnEffectStart() { }
    protected virtual void OnEffectUpdate() { }
    protected virtual void OnEffectEnd() { }
}
