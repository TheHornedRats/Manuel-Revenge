using UnityEngine;
using System.Linq;

public class ElectrocuteEffect : StatusEffect
{
    public float chainDamage = 10f;
    public float chainRadius = 3f;
    public LayerMask enemyLayer;
    public int maxChains = 5; // Limitar a 5 enemigos

    protected override void CreateParticleSystem()
    {
        base.CreateParticleSystem();
        if (effectParticles != null)
        {
            var main = effectParticles.main;
            main.startColor = new Color(0.3f, 0.8f, 1f); // Azul eléctrico
        }
    }


    protected override void OnEffectStart()
    {
        Debug.Log("ElectrocuteEffect: OnEffectStart called.");

        Collider2D[] enemies = Physics2D.OverlapCircleAll(
            enemyHealth.transform.position,
            chainRadius,
            enemyLayer
        );

        int chainsDone = 0;

        foreach (Collider2D enemy in enemies)
        {
            if (chainsDone >= maxChains)
                break;

            if (enemy.gameObject != enemyHealth.gameObject)
            {
                EnemyHealth targetHealth = enemy.GetComponent<EnemyHealth>();
                if (targetHealth != null)
                {
                    targetHealth.TakeDamage(Mathf.RoundToInt(chainDamage));
                    Debug.Log($"{enemy.name} recibió daño por electrocución!");

                    if (enemy.GetComponent<ElectrocuteEffect>() == null)
                    {
                        ElectrocuteEffect newElectrocute = enemy.gameObject.AddComponent<ElectrocuteEffect>();
                        newElectrocute.duration = duration;
                        newElectrocute.chainDamage = chainDamage * 0.7f;
                        newElectrocute.chainRadius = chainRadius;
                        newElectrocute.enemyLayer = enemyLayer;
                        newElectrocute.maxChains = maxChains;
                        newElectrocute.ApplyEffect(targetHealth);
                    }

                    chainsDone++;
                }
            }
        }
    }
}
