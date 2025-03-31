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
        if (target == null)
        {
            Debug.LogError(" ERROR: Intento de aplicar efecto a un EnemyHealth NULL.");
            return;
        }

        enemyHealth = target;
        Debug.Log($" {this.GetType().Name} aplicado correctamente a {enemyHealth.name}");

        OnEffectStart();
        CreateParticleSystem();
    }


    protected virtual void CreateParticleSystem()
    {
        GameObject psObject = new GameObject($"{this.GetType().Name}_Particles");
        psObject.transform.SetParent(enemyHealth.transform, false);

        effectParticles = psObject.AddComponent<ParticleSystem>();

        var main = effectParticles.main;
        main.duration = duration;
        main.loop = true;
        main.startLifetime = 0.5f;
        main.startSize = 0.3f;
        main.startSpeed = 0.1f;
        main.gravityModifier = 0;
        main.simulationSpace = ParticleSystemSimulationSpace.World;
        main.startColor = Color.white;

        var emission = effectParticles.emission;
        emission.rateOverTime = 15f;

        var shape = effectParticles.shape;
        shape.shapeType = ParticleSystemShapeType.Sphere;
        shape.radius = 0.1f;

        var colorOverLifetime = effectParticles.colorOverLifetime;
        colorOverLifetime.enabled = true;
        Gradient grad = new Gradient();
        grad.SetKeys(
            new GradientColorKey[] {
            new GradientColorKey(main.startColor.color, 0.0f),
            new GradientColorKey(Color.clear, 1.0f)
            },
            new GradientAlphaKey[] {
            new GradientAlphaKey(1.0f, 0.0f),
            new GradientAlphaKey(0.0f, 1.0f)
            }
        );
        colorOverLifetime.color = grad;

        effectParticles.Play();
    }


    protected virtual void Update()
    {
        elapsedTime += Time.deltaTime;


        OnEffectUpdate();

        // Si el efecto tiene ticks de daño, aplica en intervalos
        if (this is DamageOverTimeEffect dotEffect)
        {
            dotEffect.ApplyDamageTick();
        }

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
