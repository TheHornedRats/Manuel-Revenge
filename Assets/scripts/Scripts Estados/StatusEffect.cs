using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    public float duration;            // Duración total del efecto
    protected EnemyHealth enemyHealth;
    protected float elapsedTime = 0f; // Tiempo transcurrido desde que el estado se aplicó

    // Referencia al sistema de partículas creado dinámicamente
    protected ParticleSystem effectParticles;

    public void ApplyEffect(EnemyHealth target)
    {
        enemyHealth = target;
        OnEffectStart();       // Lógica inicial del efecto

        // Crear el sistema de partículas por defecto
        CreateParticleSystem();
    }

    protected virtual void Update()
    {
        elapsedTime += Time.deltaTime;
        OnEffectUpdate(); // Lógica que corre cada frame

        // Si el efecto finaliza, destruimos el sistema de partículas y este componente
        if (elapsedTime >= duration)
        {
            OnEffectEnd();
            if (effectParticles != null)
            {
                Destroy(effectParticles.gameObject);
            }
            Destroy(this);
        }
    }

    /// <summary>
    /// Crea y configura un sistema de partículas genérico.
    /// Se puede sobrescribir en cada efecto para personalizarlo.
    /// </summary>
    protected virtual void CreateParticleSystem()
    {
        // Creamos un GameObject para las partículas y lo hacemos hijo del enemigo
        GameObject psObject = new GameObject($"{this.GetType().Name}_Particles");
        psObject.transform.SetParent(enemyHealth.transform, false);

        // Agregamos el componente ParticleSystem
        effectParticles = psObject.AddComponent<ParticleSystem>();

        // Configuración básica del módulo "Main"
        var mainModule = effectParticles.main;
        mainModule.duration = duration;       // Dura lo mismo que el efecto
        mainModule.loop = false;             // No se repite indefinidamente
        mainModule.playOnAwake = false;      // No empieza hasta que llamemos a Play()
        mainModule.startColor = Color.red; // Color base (puedes ajustarlo)
        mainModule.startSize = 1f;         // Tamaño inicial de las partículas

        // Llamamos a Play() para que empiece a emitir
        effectParticles.Play();
    }

    // Métodos virtuales que los efectos específicos pueden sobrescribir
    protected virtual void OnEffectStart() { }
    protected virtual void OnEffectUpdate() { }
    protected virtual void OnEffectEnd() { }
}
