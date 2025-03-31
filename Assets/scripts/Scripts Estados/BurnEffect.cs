using UnityEngine;

public class BurnEffect : StatusEffect
{
    protected override void CreateParticleSystem()
    {
        base.CreateParticleSystem();
        if (effectParticles != null)
        {
            var main = effectParticles.main;
            main.startColor = new Color(1f, 0.4f, 0f); // Naranja fuego

            var shape = effectParticles.shape;
            shape.shapeType = ParticleSystemShapeType.Cone;
            shape.angle = 25;
            shape.radius = 0.15f;

            var sizeOverLifetime = effectParticles.sizeOverLifetime;
            sizeOverLifetime.enabled = true;
            AnimationCurve curve = new AnimationCurve();
            curve.AddKey(0.0f, 1f);
            curve.AddKey(1.0f, 0f);
            sizeOverLifetime.size = new ParticleSystem.MinMaxCurve(1f, curve);
        }
    }


    public GameObject fireEffectPrefab;
    private GameObject fireEffectInstance;

    public float damagePerSecond = 3f;
    public int tickCount = 5;
    public float spreadRadius = 1.5f;
    public float spreadChance = 1f;

    private int ticksApplied = 0;
    private float tickInterval;

    protected override void OnEffectStart()
    {
        if (fireEffectPrefab != null)
        {
            fireEffectInstance = Instantiate(fireEffectPrefab, enemyHealth.transform.position, Quaternion.identity, enemyHealth.transform);
        }

        // Validar `duration` para evitar que sea 0 o negativo
        if (duration <= 0)
        {
            Debug.LogWarning($"WARNING: `duration` es {duration}. Se establecer� a 2s por defecto.");
            duration = 2f;
        }

        // Validar `tickCount` para evitar divisiones por cero
        if (tickCount <= 0)
        {
            Debug.LogWarning($"WARNING: `tickCount` es {tickCount}. Se establecer� a 1 por defecto.");
            tickCount = 1;
        }

        tickInterval = duration / tickCount;

        Debug.Log($"{enemyHealth.name} est� en llamas por {duration} segundos. Tick cada {tickInterval}s.");
    }


    protected override void OnEffectUpdate()
    {
        if (enemyHealth == null) return;

        if (elapsedTime >= tickInterval * (ticksApplied + 1) && ticksApplied < tickCount)
        {
            int damageAmount = Mathf.RoundToInt(damagePerSecond);
            enemyHealth.TakeDamage(damageAmount);
            ticksApplied++;

            Debug.Log($" [FUEGO] {enemyHealth.name} recibe {damageAmount} de da�o. Vida restante: {enemyHealth.GetHealth()}");

            // Instanciar un nuevo sistema de part�culas en cada tick
            if (fireEffectPrefab != null)
            {
                GameObject fireInstance = Instantiate(fireEffectPrefab, enemyHealth.transform.position, Quaternion.identity, enemyHealth.transform);

                // Asegurar que el sistema de part�culas se detiene y se destruye despu�s de su duraci�n
                ParticleSystem ps = fireInstance.GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    var main = ps.main;
                    main.loop = false; // Asegurar que no se repita
                    ps.Play();
                    Destroy(fireInstance, main.duration);

                }
                else
                {
                    Debug.LogWarning(" El prefab de fuego no tiene un ParticleSystem.");
                }
            }

            if (ticksApplied >= tickCount)
            {
                Debug.Log($" {enemyHealth.name} ya no est� en llamas.");
                Destroy(this);
            }
        }
    }


    protected override void OnEffectEnd()
    {
        if (fireEffectInstance != null)
        {
            Destroy(fireEffectInstance);
        }
        Debug.Log($" {enemyHealth.name} ya no est� en llamas.");
    }
}
