using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    public WeaponData weaponData;

    public void Setup(WeaponData data)
    {
        if (data == null)
        {
            Debug.LogError($"WeaponData es NULL en {gameObject.name}.");
            return;
        }
        weaponData = data;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (weaponData == null)
        {
            Debug.LogError($"WeaponData es NULL al intentar da�ar {collision.name}");
            return;
        }

        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            Rigidbody2D enemyRb = collision.GetComponent<Rigidbody2D>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(Mathf.RoundToInt(weaponData.baseDamage));
                ApplyStatusEffect(enemyHealth);
            }

            // Para la jabalina, aplicar un empuje mayor
            if (weaponData.weaponType == WeaponType.Javelin && enemyRb != null)
            {
                Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                enemyRb.AddForce(knockbackDirection * weaponData.knockbackForce * 5f, ForceMode2D.Impulse);
                Debug.Log($"{collision.name} fue empalado y fuertemente empujado! Fuerza aplicada: {weaponData.knockbackForce * 5f}");
            }

            // Para proyectiles con efecto de explosi�n
            if (weaponData.weaponType == WeaponType.Projectile && weaponData.explosionEffectPrefab != null)
            {
                Instantiate(weaponData.explosionEffectPrefab, transform.position, Quaternion.identity);
            }

            // Destruimos el proyectil al final, despu�s de todo
            Destroy(gameObject);
        }
    }

    private void ApplyStatusEffect(EnemyHealth enemy)
    {
        if (!weaponData.appliesStatusEffect) return;

        Debug.Log($"Aplicando efecto {weaponData.statusEffect} a {enemy.name}");

        switch (weaponData.statusEffect)
        {
            case "Sangrado":
                {
                    BleedEffect bleedEffect = enemy.GetComponent<BleedEffect>();
                    if (bleedEffect == null)
                    {
                        bleedEffect = enemy.gameObject.AddComponent<BleedEffect>();
                    }

                    bleedEffect.duration = weaponData.statusEffectDuration;
                    bleedEffect.damagePerSecond = weaponData.statusEffectDamage;
                    bleedEffect.tickCount = weaponData.statusEffectTicks;
                }
                break;

            case "Fuego":
                {
                    BurnEffect burnEffect = enemy.GetComponent<BurnEffect>();
                    if (burnEffect == null)
                    {
                        burnEffect = enemy.gameObject.AddComponent<BurnEffect>();
                    }

                    burnEffect.duration = weaponData.statusEffectDuration;
                    burnEffect.damagePerSecond = weaponData.statusEffectDamage;
                    burnEffect.tickCount = weaponData.statusEffectTicks;
                    burnEffect.spreadChance = 0.3f;
                    burnEffect.spreadRadius = 1.5f;

                    burnEffect.fireEffectPrefab = weaponData.burnParticlesPrefab;
                    burnEffect.ApplyEffect(enemy);
                }
                break;

            case "Electrocuci�n":
                if (enemy.GetComponent<ElectrocuteEffect>() == null)
                {
                    ElectrocuteEffect electrocute = enemy.gameObject.AddComponent<ElectrocuteEffect>();
                    electrocute.duration = weaponData.statusEffectDuration;
                    electrocute.chainDamage = weaponData.statusEffectDamage;
                    electrocute.chainRadius = 3f;
                    electrocute.enemyLayer = LayerMask.GetMask("Enemy");

                    electrocute.electrocuteEffectPrefab = weaponData.electrocuteParticlesPrefab;

                    electrocute.ApplyEffect(enemy);
                }
                break;


            case "Santificaci�n":
                {
                    SanctifyEffect existingEffect = enemy.GetComponent<SanctifyEffect>();
                    if (existingEffect == null)
                    {
                        existingEffect = enemy.gameObject.AddComponent<SanctifyEffect>();
                        existingEffect.duration = weaponData.statusEffectDuration;
                        existingEffect.sanctifyEffectPrefab = weaponData.sanctifyParticlesPrefab;
                        existingEffect.ApplyEffect(enemy);
                    }
                }
                break;
        }
    }
}
