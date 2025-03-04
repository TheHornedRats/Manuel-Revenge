using UnityEngine;

public class EspadonAttack : MonoBehaviour
{
    private Collider2D hitbox;
    public float attackDuration = 0.3f;
    public float damage;
    public bool appliesStatusEffect;
    public string statusEffect;

    private void Start()
    {
        hitbox = GetComponent<Collider2D>();
        if (hitbox != null)
        {
            hitbox.enabled = true;
            Invoke("DisableHitbox", attackDuration);
        }
    }

    private void DisableHitbox()
    {
        if (hitbox != null)
        {
            hitbox.enabled = false;
        }
        Destroy(gameObject); // Se destruye el prefab después del ataque
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage((int)damage);
                Debug.Log("Espadón golpeó a " + collision.name);

                // Aplicar efecto de sangrado si está habilitado
                if (appliesStatusEffect && statusEffect == "Sangrado")
                {
                    Debug.Log("Aplicando sangrado a " + collision.name);
                    BleedEffect bleedEffect = collision.gameObject.AddComponent<BleedEffect>();
                    bleedEffect.duration = 3f;
                    bleedEffect.damagePerSecond = damage * 0.2f;
                    bleedEffect.tickCount = 3;
                    bleedEffect.ApplyEffect(enemyHealth);
                }
            }
        }
    }
}