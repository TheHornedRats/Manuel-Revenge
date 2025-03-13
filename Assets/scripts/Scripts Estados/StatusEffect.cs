using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    public float duration;            // Duraci�n total del efecto
    protected EnemyHealth enemyHealth;
    protected float elapsedTime = 0f; // Tiempo transcurrido desde que el estado se aplic�

    // Referencia al sistema de part�culas creado din�micamente
    protected ParticleSystem effectParticles;

    public void ApplyEffect(EnemyHealth target)
    {
        enemyHealth = target;
        OnEffectStart();       // L�gica inicial del efecto

        // Crear el sistema de part�culas por defecto
        CreateParticleSystem();
    }

    protected virtual void Update()
    {
        elapsedTime += Time.deltaTime;
        OnEffectUpdate(); // L�gica que corre cada frame

        // Si el efecto finaliza, destruimos el sistema de part�culas y este componente
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
    /// Crea y configura un sistema de part�culas gen�rico.
    /// Se puede sobrescribir en cada efecto para personalizarlo.
    /// </summary>
    protected virtual void CreateParticleSystem()
    {
        // Creamos un GameObject para las part�culas y lo hacemos hijo del enemigo
        GameObject psObject = new GameObject($"{this.GetType().Name}_Particles");
        psObject.transform.SetParent(enemyHealth.transform, false);

        // Agregamos el componente ParticleSystem
        effectParticles = psObject.AddComponent<ParticleSystem>();

        // Configuraci�n b�sica del m�dulo "Main"
        var mainModule = effectParticles.main;
        mainModule.duration = duration;       // Dura lo mismo que el efecto
        mainModule.loop = false;             // No se repite indefinidamente
        mainModule.playOnAwake = false;      // No empieza hasta que llamemos a Play()
        mainModule.startColor = Color.red; // Color base (puedes ajustarlo)
        mainModule.startSize = 1f;         // Tama�o inicial de las part�culas

        // Llamamos a Play() para que empiece a emitir
        effectParticles.Play();
    }

    // M�todos virtuales que los efectos espec�ficos pueden sobrescribir
    protected virtual void OnEffectStart() { }
    protected virtual void OnEffectUpdate() { }
    protected virtual void OnEffectEnd() { }
}
