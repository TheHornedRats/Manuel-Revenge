using UnityEngine;
using System.Collections;

public class BleedEffect : StatusEffect
{
    public float damagePerSecond; // Daño aplicado por segundo
    public int tickCount; // Número de veces que se aplicará el daño
    private float tickInterval; // Intervalo entre cada aplicación de daño

    private void Start()
    {
        tickInterval = duration / tickCount;
    }

    protected override IEnumerator EffectCoroutine()
    {
        Debug.Log("Efecto de Sangrado aplicado a " + enemyHealth.name);
        for (int i = 0; i < tickCount; i++)
        {
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage((int)damagePerSecond);
                Debug.Log("Sangrado: " + damagePerSecond + " de daño aplicado a " + enemyHealth.name);
            }
            yield return new WaitForSeconds(tickInterval);
        }
        Debug.Log("Efecto de Sangrado finalizado en " + enemyHealth.name);
        Destroy(this); // Elimina el efecto después de completar los ticks
    }
}