using UnityEngine;

public class ElectrocuteEffect : StatusEffect
{
    public float chainDamage = 10f;
    public float chainRadius = 3f;
    public LayerMask enemyLayer;

    public GameObject electrocuteEffectPrefab;
    private GameObject electrocuteEffectInstance;

    public int maxJumps = 3;

    protected override void OnEffectStart()
    {
        if (enemyHealth == null)
        {
            Debug.LogError("[Electrocución] EnemyHealth es null. Abortando.");
            return;
        }

        // Instanciar partículas eléctricas
        if (electrocuteEffectPrefab != null)
        {
            electrocuteEffectInstance = Instantiate(
                electrocuteEffectPrefab,
                enemyHealth.transform.position,
                Quaternion.identity,
                enemyHealth.transform
            );
            Debug.Log("[Electrocución] Partículas instanciadas.");
        }
        else
        {
            Debug.LogWarning("[Electrocución] Prefab de partículas no asignado.");
        }

        // Buscar enemigos cercanos para propagar
        Collider2D[] enemies = Physics2D.OverlapCircleAll(enemyHealth.transform.position, chainRadius, enemyLayer);
        int jumps = 0;

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.gameObject == enemyHealth.gameObject || jumps >= maxJumps)
                continue;

            EnemyHealth targetHealth = enemy.GetComponent<EnemyHealth>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(Mathf.RoundToInt(chainDamage));
                Debug.Log($"{enemy.name} recibió daño por electrocución.");
                jumps++;

                if (enemy.GetComponent<ElectrocuteEffect>() == null)
                {
                    ElectrocuteEffect newElectrocute = enemy.gameObject.AddComponent<ElectrocuteEffect>();
                    newElectrocute.duration = duration;
                    newElectrocute.chainDamage = chainDamage * 0.7f;
                    newElectrocute.chainRadius = chainRadius;
                    newElectrocute.enemyLayer = enemyLayer;
                    newElectrocute.electrocuteEffectPrefab = electrocuteEffectPrefab;
                    newElectrocute.maxJumps = maxJumps - 1;
                    newElectrocute.ApplyEffect(targetHealth);
                }
            }
        }
    }

    protected override void OnEffectEnd()
    {
        if (electrocuteEffectInstance != null)
        {
            Destroy(electrocuteEffectInstance);
        }
    }
}
