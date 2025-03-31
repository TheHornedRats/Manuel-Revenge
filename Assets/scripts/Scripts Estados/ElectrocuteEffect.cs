using UnityEngine;

public class ElectrocuteEffect : StatusEffect
{
    public float chainDamage = 10f;
    public float chainRadius = 3f;
    public LayerMask enemyLayer;

    protected override void CreateParticleSystem()
    {
        base.CreateParticleSystem();
        if (effectParticles != null)
        {
            var main = effectParticles.main;
            main.startColor = new Color(0.3f, 0.8f, 1f); // Azul eléctrico
            main.startSize = 0.2f;
            main.startSpeed = 1.5f;
            main.startLifetime = 0.4f;

            var emission = effectParticles.emission;
            emission.rateOverTime = 30f;

            var shape = effectParticles.shape;
            shape.shapeType = ParticleSystemShapeType.Circle;
            shape.radius = 0.2f;
            shape.arcMode = ParticleSystemShapeMultiModeValue.Random;

            var lights = effectParticles.lights;
            lights.enabled = true;
            lights.intensityMultiplier = 1f;
            lights.rangeMultiplier = 0.5f;

            var trails = effectParticles.trails;
            trails.enabled = true;
            trails.ribbonCount = 1;
            trails.lifetime = 0.3f;

            var colorOverLifetime = effectParticles.colorOverLifetime;
            colorOverLifetime.enabled = true;
            Gradient grad = new Gradient();
            grad.SetKeys(
                new GradientColorKey[] {
                new GradientColorKey(new Color(0.5f, 1f, 1f), 0.0f),
                new GradientColorKey(Color.clear, 1.0f)
                },
                new GradientAlphaKey[] {
                new GradientAlphaKey(1f, 0f),
                new GradientAlphaKey(0f, 1f)
                }
            );
            colorOverLifetime.color = grad;
        }
    }


    protected override void OnEffectStart()
    {
        Debug.Log("ElectrocuteEffect: OnEffectStart called.");
        // Detecta enemigos en el radio de propagación
        Collider2D[] enemies = Physics2D.OverlapCircleAll(enemyHealth.transform.position, chainRadius, enemyLayer);

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.gameObject != enemyHealth.gameObject) // Evitar auto-daño
            {
                EnemyHealth targetHealth = enemy.GetComponent<EnemyHealth>();
                if (targetHealth != null)
                {
                    targetHealth.TakeDamage(Mathf.RoundToInt(chainDamage));
                    Debug.Log($"{enemy.name} recibió daño por electrocución!");

                    // Propagar efecto de electrocución a otros enemigos
                    if (enemy.GetComponent<ElectrocuteEffect>() == null)
                    {
                        ElectrocuteEffect newElectrocute = enemy.gameObject.AddComponent<ElectrocuteEffect>();
                        newElectrocute.duration = duration;
                        newElectrocute.chainDamage = chainDamage * 0.7f; // Reduce el daño en cada propagación
                        newElectrocute.chainRadius = chainRadius;
                        newElectrocute.enemyLayer = enemyLayer;
                        // Llama a ApplyEffect para que el nuevo efecto cree sus partículas
                        newElectrocute.ApplyEffect(enemy.GetComponent<EnemyHealth>());
                    }
                }
            }
        }
    }
}
