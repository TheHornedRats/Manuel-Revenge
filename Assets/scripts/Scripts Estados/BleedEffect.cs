using UnityEngine;

public class BleedEffect : StatusEffect
{
   
        protected override void CreateParticleSystem()
        {
            // Llama al método base para crear el sistema
            base.CreateParticleSystem();
            // Cambia el color a rojo
            var main = effectParticles.main;
            main.startColor = Color.magenta;
        }
  

    public float damagePerSecond;
    public int tickCount;
    private float tickInterval;
    private int ticksApplied = 0;

    protected override void OnEffectStart()
    {
        tickInterval = duration / tickCount;
    }

    protected override void OnEffectUpdate()
    {
        if (enemyHealth == null) return;

        if (elapsedTime >= tickInterval * (ticksApplied + 1) && ticksApplied < tickCount)
        {
            enemyHealth.TakeDamage((int)damagePerSecond);
            ticksApplied++;
            Debug.Log(" Sangrado: " + damagePerSecond + " de daño aplicado a " + enemyHealth.name);
        }
    }
}
