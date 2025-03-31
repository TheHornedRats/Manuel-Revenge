using UnityEngine;

public class BleedEffect : StatusEffect
{
    protected override void CreateParticleSystem()
    {
        base.CreateParticleSystem();
        if (effectParticles != null)
        {
            var main = effectParticles.main;
            main.startColor = Color.red;

            var shape = effectParticles.shape;
            shape.shapeType = ParticleSystemShapeType.Hemisphere;

            var emission = effectParticles.emission;
            emission.rateOverTime = 25f;
        }
    }


    public float damagePerSecond;
    public int tickCount;
    private float tickInterval;
    private int ticksApplied = 0;

    protected override void OnEffectStart()
    {
        if (tickCount <= 0) tickCount = 1; // Evita divisiones por cero
        tickInterval = duration / tickCount;
        Debug.Log($" {enemyHealth.name} comienza a sangrar por {duration} segundos. Tick cada {tickInterval}s.");
    }

    protected override void OnEffectUpdate()
    {
        if (enemyHealth == null) return;

        if (elapsedTime >= tickInterval * (ticksApplied + 1) && ticksApplied < tickCount)
        {
            int damageAmount = Mathf.RoundToInt(damagePerSecond);
            enemyHealth.TakeDamage(damageAmount);
            ticksApplied++;

            Debug.Log($" [SANGRADO] {enemyHealth.name} recibe {damageAmount} de daño. Salud restante: {enemyHealth.GetHealth()}");

            // Si el sangrado ha aplicado todos sus ticks, termina el efecto
            if (ticksApplied >= tickCount)
            {
                Debug.Log($" {enemyHealth.name} ha dejado de sangrar.");
                Destroy(this);
            }
        }
    }
}
