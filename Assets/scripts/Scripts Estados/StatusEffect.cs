using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    public float duration; // Duración total del efecto
    protected EnemyHealth enemyHealth;
    protected float elapsedTime = 0f; // Tiempo transcurrido desde que se aplicó el efecto

    // Sistema de partículas generado dinámicamente
    protected ParticleSystem effectParticles;

    public void ApplyEffect(EnemyHealth target)
    {
        enemyHealth = target;
        OnEffectStart();
        CreateParticleSystem();
    }

    protected virtual void CreateParticleSystem()
    {
        // Creamos un GameObject para las partículas y lo hacemos hijo del enemigo
        GameObject psObject = new GameObject($"{this.GetType().Name}_Particles");
        psObject.transform.SetParent(enemyHealth.transform, false);

        // Añadir componente ParticleSystem y su renderer
        effectParticles = psObject.AddComponent<ParticleSystem>();
        psObject.AddComponent<ParticleSystemRenderer>(); // Asegura que se vea

        // Configuración básica
        var mainModule = effectParticles.main;
        mainModule.duration = duration;
        mainModule.loop = false;
        mainModule.playOnAwake = false;
        mainModule.startColor = Color.white;
        mainModule.startSize = 0.5f;

        var emissionModule = effectParticles.emission;
        emissionModule.rateOverTime = 10f;

        // Asignar un material temporal para evitar el magenta
        var renderer = effectParticles.GetComponent<ParticleSystemRenderer>();
        if (renderer != null)
        {
            Shader particleShader = Shader.Find("Particles/Standard Unlit");
            if (particleShader != null)
            {
                Material defaultMat = new Material(particleShader);
                defaultMat.SetColor("_Color", Color.white); // Color base
                renderer.material = defaultMat;
            }
            else
            {
                Debug.LogWarning("No se encontró el shader 'Particles/Standard Unlit'.");
            }
        }

        // Iniciar la emisión
        effectParticles.Play();
    }

    protected virtual void Update()
    {
        elapsedTime += Time.deltaTime;
        OnEffectUpdate();

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

    // Métodos sobrescribibles
    protected virtual void OnEffectStart() { }
    protected virtual void OnEffectUpdate() { }
    protected virtual void OnEffectEnd() { }
}
