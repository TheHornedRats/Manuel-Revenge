using UnityEngine;

public class ElectrocuteEffect : StatusEffect
{
    public float chainDamage = 10f;
    public float chainRadius = 3f;
    public LayerMask enemyLayer;

    protected override void OnEffectStart()
    {
        // Detectar enemigos en el radio de propagaci�n
        Collider2D[] enemies = Physics2D.OverlapCircleAll(enemyHealth.transform.position, chainRadius, enemyLayer);

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.gameObject != enemyHealth.gameObject) // Evitar auto-da�o
            {
                EnemyHealth targetHealth = enemy.GetComponent<EnemyHealth>();
                if (targetHealth != null)
                {
                    targetHealth.TakeDamage(Mathf.RoundToInt(chainDamage));
                    Debug.Log($"{enemy.name} recibi� da�o por electrocuci�n!");

                    // Propagar efecto de electrocuci�n a otros enemigos
                    if (enemy.GetComponent<ElectrocuteEffect>() == null)
                    {
                        ElectrocuteEffect newElectrocute = enemy.gameObject.AddComponent<ElectrocuteEffect>();
                        newElectrocute.duration = duration;
                        newElectrocute.chainDamage = chainDamage * 0.7f; // El da�o se reduce en cada propagaci�n
                        newElectrocute.chainRadius = chainRadius;
                        newElectrocute.enemyLayer = enemyLayer;
                    }
                }
            }
        }
    }
}
