using UnityEngine;

public abstract class DamageOverTimeEffect : StatusEffect
{
    public float damagePerSecond;
    public int tickCount;

    private int ticksApplied = 0;
    private float tickInterval;

    protected override void OnEffectStart()
    {
        tickInterval = duration / tickCount;
        Debug.Log($" {enemyHealth.name} afectado por {this.GetType().Name} durante {duration} segundos. Tick cada {tickInterval}s.");
    }

    public void ApplyDamageTick()
    {
        if (elapsedTime >= tickInterval * (ticksApplied + 1) && ticksApplied < tickCount)
        {
            enemyHealth.TakeDamage(Mathf.RoundToInt(damagePerSecond));
            ticksApplied++;
            Debug.Log($" {enemyHealth.name} recibe {damagePerSecond} de daño por {this.GetType().Name}. Vida restante: {enemyHealth.GetHealth()}");
        }
    }
}
