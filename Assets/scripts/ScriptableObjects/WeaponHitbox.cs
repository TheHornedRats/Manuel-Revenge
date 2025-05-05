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
            Debug.LogError($"WeaponData es NULL al intentar dañar {collision.name}");
            return;
        }

        if (collision.CompareTag("Enemy"))
        {
            // Destruir el proyectil inmediatamente al colisionar
            Destroy(gameObject);

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

            // Para proyectiles con efecto de explosión, instanciar la explosión
            if (weaponData.weaponType == WeaponType.Projectile && weaponData.explosionEffectPrefab != null)
            {
                Instantiate(weaponData.explosionEffectPrefab, transform.position, Quaternion.identity);
            }
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
                    // Actualizamos (o asignamos) las propiedades del efecto
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

                    //  Asignación del prefab de partículas desde WeaponData
                    burnEffect.fireEffectPrefab = weaponData.burnParticlesPrefab;

                    burnEffect.ApplyEffect(enemy); // ¡IMPORTANTE! Siempre después de configurar
                }
                break;


            case "Electrocución":
                if (enemy.GetComponent<ElectrocuteEffect>() == null)
                {
                    ElectrocuteEffect electrocute = enemy.gameObject.AddComponent<ElectrocuteEffect>();
                    electrocute.duration = weaponData.statusEffectDuration;
                    electrocute.chainDamage = weaponData.statusEffectDamage;
                    electrocute.chainRadius = 3f;
                    electrocute.enemyLayer = LayerMask.GetMask("Enemy");
                    electrocute.ApplyEffect(enemy); // ¡Importante para crear el ParticleSystem!
                }
                break;


            case "Santificación":
                {
                    SanctifyEffect existingEffect = enemy.GetComponent<SanctifyEffect>();
                    if (existingEffect == null)
                    {
                        existingEffect = enemy.gameObject.AddComponent<SanctifyEffect>();
                        existingEffect.duration = weaponData.statusEffectDuration;
                        existingEffect.ApplyEffect(enemy);
                    }
                }
                break;

        }
    }

}
