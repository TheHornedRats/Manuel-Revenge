using UnityEngine;

public class ElectrocuteEffect : StatusEffect
{
    public float chainDamage = 10f;
    public float chainRadius = 3f;
    public LayerMask enemyLayer;

    protected override void CreateParticleSystem()
    {
        // Llama al m�todo base para crear el ParticleSystem
        base.CreateParticleSystem();
        // Cambia el color a azul para representar electrocuci�n
        var main = effectParticles.main;
        main.startColor = Color.blue;
        Debug.Log("ElectrocuteEffect: Particle system created with blue color.");

        // Opcional: asigna un material v�lido para part�culas, si es necesario
        ParticleSystemRenderer renderer = effectParticles.GetComponent<ParticleSystemRenderer>();
        if (renderer != null && renderer.material == null)
        {
            // Aseg�rate de tener un material en Resources o asigna uno por defecto
            renderer.material = new Material(Shader.Find("Particles/Standard Unlit"));
        }
    }

    protected override void OnEffectStart()
    {
        Debug.Log("ElectrocuteEffect: OnEffectStart called.");
        // Detecta enemigos en el radio de propagaci�n
        Collider2D[] enemies = Physics2D.OverlapCircleAll(enemyHealth.transform.position, chainRadius, enemyLayer);

        int maxChains = 5;
        int chainCount = 0;

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.gameObject != enemyHealth.gameObject) // Evitar auto-da�o
            {
                EnemyHealth targetHealth = enemy.GetComponent<EnemyHealth>();
                if (targetHealth != null)
                {
                    targetHealth.TakeDamage(Mathf.RoundToInt(chainDamage));
                    Debug.Log($"{enemy.name} recibi� da�o por electrocuci�n!");

                    // Propagar efecto de electrocuci�n a otros enemigos
                    if (enemy.GetComponent<ElectrocuteEffect>() == null)
                    {
                        ElectrocuteEffect newElectrocute = enemy.gameObject.AddComponent<ElectrocuteEffect>();
                        newElectrocute.duration = duration;
                        newElectrocute.chainDamage = chainDamage * 0.7f; // Reduce el da�o en cada propagaci�n
                        newElectrocute.chainRadius = chainRadius;
                        newElectrocute.enemyLayer = enemyLayer;
                        // Llama a ApplyEffect para que el nuevo efecto cree sus part�culas
                        newElectrocute.ApplyEffect(enemy.GetComponent<EnemyHealth>());
                    }

                    chainCount++;
                    if (chainCount >= maxChains)
                        break; // Detiene la propagaci�n si se alcanz� el l�mite
                }
            }
        }
    }
}
