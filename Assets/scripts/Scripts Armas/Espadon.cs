using UnityEngine;
using System.Collections;
using Weapons;

public class Espadon : Weapon
{
    private Collider2D hitbox;
    public float attackDuration = 0.3f;
    public GameObject bleedEffectPrefab; // Prefab del efecto de sangrado
    public float bleedDuration = 3f;
    public float bleedDamagePerSecond = 5f;
    public int bleedTicks = 3;

    private void Start()
    {
        weaponName = "Espadón";
        damage = 50f;
        cooldown = 1.5f;
        effect = "Sangrado";

        hitbox = GetComponent<Collider2D>();
        if (hitbox != null)
        {
            hitbox.enabled = false;
        }
    }

    protected override void PerformAttack()
    {
        Debug.Log(weaponName + " ha atacado automáticamente.");
        StartCoroutine(ActivateHitbox());
    }

    private IEnumerator ActivateHitbox()
    {
        if (hitbox != null)
        {
            hitbox.enabled = true;
            yield return new WaitForSeconds(attackDuration);
            hitbox.enabled = false;
        }
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

                // Aplicar efecto de sangrado
                if (bleedEffectPrefab != null)
                {
                    Debug.Log("Aplicando sangrado a " + collision.name);
                    BleedEffect bleedEffect = collision.gameObject.AddComponent<BleedEffect>();
                    bleedEffect.duration = bleedDuration;
                    bleedEffect.damagePerSecond = bleedDamagePerSecond;
                    bleedEffect.tickCount = bleedTicks;
                    bleedEffect.ApplyEffect(enemyHealth);
                }
            }
        }
    }
}
