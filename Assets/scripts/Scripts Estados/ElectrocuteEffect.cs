using UnityEngine;

public class ElectrocuteEffect : StatusEffect
{
    public float chainDamage = 10f;
    public float chainRadius = 3f;
    public LayerMask enemyLayer;

  

    protected override void OnEffectStart()
    {
        Debug.Log("ElectrocuteEffect: OnEffectStart called.");
        // Detecta enemigos en el radio de propagación
        Collider2D[] enemies = Physics2D.OverlapCircleAll(enemyHealth.transform.position, chainRadius, enemyLayer);

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.gameObject != enemyHealth.gameObject) // Evitar auto-daño
            {
                EnemyHealth targetHealth = enemy.GetComponent<EnemyHealth>();
                if (targetHealth != null)
                {
                    targetHealth.TakeDamage(Mathf.RoundToInt(chainDamage));
                    Debug.Log($"{enemy.name} recibió daño por electrocución!");

                    // Propagar efecto de electrocución a otros enemigos
                    if (enemy.GetComponent<ElectrocuteEffect>() == null)
                    {
                        ElectrocuteEffect newElectrocute = enemy.gameObject.AddComponent<ElectrocuteEffect>();
                        newElectrocute.duration = duration;
                        newElectrocute.chainDamage = chainDamage * 0.7f; // Reduce el daño en cada propagación
                        newElectrocute.chainRadius = chainRadius;
                        newElectrocute.enemyLayer = enemyLayer;
                        // Llama a ApplyEffect para que el nuevo efecto cree sus partículas
                        newElectrocute.ApplyEffect(enemy.GetComponent<EnemyHealth>());
                    }
                }
            }
        }
    }
}
