using UnityEngine;
using System.Collections.Generic;

public class ElectrocuteEffect : StatusEffect
{
    public float chainDamage = 10f;
    public float chainRadius = 3f;
    public LayerMask enemyLayer;
    public GameObject electricArcPrefab; // Prefab con LineRenderer para el arco eléctrico

    //protected override void CreateParticleSystem()
    //{
    //    base.CreateParticleSystem();
    //    var main = effectParticles.main;
    //    main.startColor = Color.blue;
    //    Debug.Log("ElectrocuteEffect: Particle system created with blue color.");

    //    ParticleSystemRenderer renderer = effectParticles.GetComponent<ParticleSystemRenderer>();
    //    if (renderer != null && renderer.material == null)
    //    {
    //        renderer.material = new Material(Shader.Find("Particles/Standard Unlit"));
    //    }
    //}

    protected override void OnEffectStart()
    {
        Debug.Log("ElectrocuteEffect: OnEffectStart called.");
        Collider2D[] enemies = Physics2D.OverlapCircleAll(enemyHealth.transform.position, chainRadius, enemyLayer);

        int maxChains = 5;
        int chainCount = 0;

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.gameObject != enemyHealth.gameObject)
            {
                EnemyHealth targetHealth = enemy.GetComponent<EnemyHealth>();
                if (targetHealth != null)
                {
                    targetHealth.TakeDamage(Mathf.RoundToInt(chainDamage));
                    Debug.Log($"{enemy.name} recibió daño por electrocución!");

                    // Crear rayo visual entre este enemigo y el objetivo
                    CreateElectricArc(enemy.transform);

                    if (enemy.GetComponent<ElectrocuteEffect>() == null)
                    {
                        ElectrocuteEffect newElectrocute = enemy.gameObject.AddComponent<ElectrocuteEffect>();
                        newElectrocute.duration = duration;
                        newElectrocute.chainDamage = chainDamage * 0.7f;
                        newElectrocute.chainRadius = chainRadius;
                        newElectrocute.enemyLayer = enemyLayer;
                        newElectrocute.electricArcPrefab = electricArcPrefab;
                        newElectrocute.ApplyEffect(enemy.GetComponent<EnemyHealth>());
                    }

                    chainCount++;
                    if (chainCount >= maxChains)
                        break;
                }
            }
        }
    }

    private void CreateElectricArc(Transform target)
    {
        if (electricArcPrefab != null)
        {
            GameObject arc = Instantiate(electricArcPrefab);
            LineRenderer lr = arc.GetComponent<LineRenderer>();
            if (lr != null)
            {
                lr.SetPosition(0, enemyHealth.transform.position);
                lr.SetPosition(1, target.position);
            }

            // Destruir el arco después de un corto tiempo
            Destroy(arc, 0.3f);
        }
    }
}
