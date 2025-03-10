using UnityEngine;

public class ElectrocuteEffect : StatusEffect
{
    public float chainDamage = 10f;
    public float chainRadius = 3f;
    public LayerMask enemyLayer;

    protected override void OnEffectStart()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(enemyHealth.transform.position, chainRadius, enemyLayer);
        foreach (Collider2D enemy in enemies)
        {
            if (enemy != enemyHealth.gameObject) // No dañar al enemigo original
            {
                EnemyHealth targetHealth = enemy.GetComponent<EnemyHealth>();
                if (targetHealth != null)
                {
                    targetHealth.TakeDamage((int)chainDamage);
                    Debug.Log(enemy.name + " recibió daño por electrocución!");
                }
            }
        }
    }
}
