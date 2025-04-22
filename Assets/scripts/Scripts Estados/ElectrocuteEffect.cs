using UnityEngine;

public class ElectrocuteEffect : StatusEffect
{
    public float chainDamage = 10f;
    public float chainRadius = 3f;
    public LayerMask enemyLayer;

    protected override void CreateParticleSystem()
    {
        // Llama al método base para crear el ParticleSystem
        base.CreateParticleSystem();
        // Cambia el color a azul para representar electrocución
        var main = effectParticles.main;
        main.startColor = Color.blue;
        Debug.Log("ElectrocuteEffect: Particle system created with blue color.");

        // Opcional: asigna un material válido para partículas, si es necesario
        ParticleSystemRenderer renderer = effectParticles.GetComponent<ParticleSystemRenderer>();
        if (renderer != null && renderer.material == null)
        {
            // Asegúrate de tener un material en Resources o asigna uno por defecto
            renderer.material = new Material(Shader.Find("Particles/Standard Unlit"));
        }
    }

    protected override void OnEffectStart()
    {
        Debug.Log("ElectrocuteEffect: OnEffectStart called.");
        // Detecta enemigos en el radio de propagación
        Collider2D[] enemies = Physics2D.OverlapCircleAll(enemyHealth.transform.position, chainRadius, enemyLayer);

        int maxChains = 5;
        int chainCount = 0;

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

                    chainCount++;
                    if (chainCount >= maxChains)
                        break; // Detiene la propagación si se alcanzó el límite
                }
            }
        }
    }
}
