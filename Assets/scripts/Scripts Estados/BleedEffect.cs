using UnityEngine;

public class BleedEffect : StatusEffect
{
    public float damagePerSecond;
    public int tickCount;
    private float tickInterval;
    private int ticksApplied = 0;

    protected override void CreateParticleSystem()
    {
        base.CreateParticleSystem();
        if (effectParticles != null)
        {
            var main = effectParticles.main;
            main.startColor = Color.red;
        }
    }

    protected override void OnEffectStart()
    {
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
}
