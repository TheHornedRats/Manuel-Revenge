using UnityEngine;
using System.Collections;

public class BleedEffect : StatusEffect
{
    public float damagePerSecond; // Da�o aplicado por segundo
    public int tickCount; // N�mero de veces que se aplicar� el da�o
    private float tickInterval; // Intervalo entre cada aplicaci�n de da�o

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
                Debug.Log("Sangrado: " + damagePerSecond + " de da�o aplicado a " + enemyHealth.name);
            }
            yield return new WaitForSeconds(tickInterval);
        }
        Debug.Log("Efecto de Sangrado finalizado en " + enemyHealth.name);
        Destroy(this); // Elimina el efecto despu�s de completar los ticks
    }
}