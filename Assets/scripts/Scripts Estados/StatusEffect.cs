using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    public float duration;            // Duración total del efecto
    protected EnemyHealth enemyHealth;
    protected float elapsedTime = 0f; // Tiempo transcurrido desde que se aplicó el efecto

    // Variable para almacenar el ParticleSystem creado por código
    protected ParticleSystem effectParticles;

    public void ApplyEffect(EnemyHealth target)
    {
        enemyHealth = target;
        OnEffectStart();       // Lógica inicial del efecto

        // Crear el sistema de partículas de forma dinámica
        CreateParticleSystem();
    }

    protected virtual void CreateParticleSystem()
    {
        // Creamos un GameObject para las partículas y lo hacemos hijo del enemigo
        GameObject psObject = new GameObject($"{this.GetType().Name}_Particles");
        psObject.transform.SetParent(enemyHealth.transform, false);

        // Agregamos un ParticleSystem
        effectParticles = psObject.AddComponent<ParticleSystem>();

        // Configuramos el módulo Main del ParticleSystem
        var mainModule = effectParticles.main;
        mainModule.duration = duration;
        mainModule.loop = false;             // No se repite indefinidamente
        mainModule.playOnAwake = false;      // No comienza automáticamente
        mainModule.startColor = Color.white; // Color base (puedes cambiarlo o sobrescribirlo en cada efecto)
        mainModule.startSize = 0.5f;         // Tamaño de las partículas

        // Opcional: configura la emisión
        var emissionModule = effectParticles.emission;
        emissionModule.rateOverTime = 10f;

        // Inicia la emisión
        effectParticles.Play();
    }

    protected virtual void Update()
    {
        elapsedTime += Time.deltaTime;
        OnEffectUpdate();

        // Si el efecto ha durado lo suficiente, finaliza y destruye el ParticleSystem
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

    // Métodos virtuales para que cada efecto pueda sobrescribir su comportamiento
    protected virtual void OnEffectStart() { }
    protected virtual void OnEffectUpdate() { }
    protected virtual void OnEffectEnd() { }
}
