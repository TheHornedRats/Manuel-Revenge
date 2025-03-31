using UnityEngine;

public class BurnEffect : StatusEffect
{
    public GameObject fireEffectPrefab;
    private GameObject fireEffectInstance;

    public float damagePerSecond = 3f;
    public int tickCount = 5;
    public float spreadRadius = 1.5f;
    public float spreadChance = 1f;

    private int ticksApplied = 0;
    private float tickInterval;

    protected override void CreateParticleSystem()
    {
        base.CreateParticleSystem();
        if (effectParticles != null)
        {
            var main = effectParticles.main;
            main.startColor = new Color(1f, 0.4f, 0f); // Naranja fuego
        }
    }

    protected override void OnEffectStart()
    {
        if (fireEffectPrefab != null)
        {
            fireEffectInstance = Instantiate(fireEffectPrefab, enemyHealth.transform.position, Quaternion.identity, enemyHealth.transform);
        }

        if (tickCount <= 0) tickCount = 1;
        tickInterval = duration / tickCount;
    }

    protected override void OnEffectUpdate()
    {
        if (enemyHealth == null) return;

        if (elapsedTime >= tickInterval * (ticksApplied + 1) && ticksApplied < tickCount)
        {
            enemyHealth.TakeDamage(Mathf.RoundToInt(damagePerSecond));
            ticksApplied++;
        }
    }

    protected override void OnEffectEnd()
    {
        if (fireEffectInstance != null)
        {
            Destroy(fireEffectInstance);
        }
    }
}
