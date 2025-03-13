using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    public WeaponData weaponData;

    public void Setup(WeaponData data)
    {
        if (data == null)
        {
            Debug.LogError($"WeaponData es NULL en {gameObject.name}. Asegúrate de asignarlo en el prefab.");
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
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            Rigidbody2D enemyRb = collision.GetComponent<Rigidbody2D>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(Mathf.RoundToInt(weaponData.baseDamage));
                ApplyStatusEffect(enemyHealth);

                // Si es una jabalina, empuja al enemigo hacia atrás
                if (weaponData.weaponType == WeaponType.Javelin && enemyRb != null)
                {
                    Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                    enemyRb.AddForce(knockbackDirection * weaponData.knockbackForce, ForceMode2D.Impulse);
                    Debug.Log($"{collision.name} fue empalado y empujado!");
                }
            }

            if (weaponData.weaponType == WeaponType.Javelin || weaponData.weaponType == WeaponType.Projectile)
            {
                Explode();
            }
        }
    }

    private void Explode()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, weaponData.explosionRadius);

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(Mathf.RoundToInt(weaponData.baseDamage));
                    ApplyStatusEffect(enemyHealth);
                }
            }
        }

        if (weaponData.explosionEffectPrefab != null)
        {
            Instantiate(weaponData.explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    private void ApplyStatusEffect(EnemyHealth enemy)
    {
        if (!weaponData.appliesStatusEffect) return;

        Debug.Log($"Intentando aplicar efecto {weaponData.statusEffect} a {enemy.name}");

        switch (weaponData.statusEffect)
        {
            case "Sangrado":
                if (enemy.GetComponent<BleedEffect>() == null)
                {
                    BleedEffect bleedEffect = enemy.gameObject.AddComponent<BleedEffect>();
                    bleedEffect.duration = weaponData.statusEffectDuration;
                    bleedEffect.damagePerSecond = weaponData.statusEffectDamage;
                    bleedEffect.tickCount = weaponData.statusEffectTicks;
                    Debug.Log($"{enemy.name} ha sido afectado por Sangrado.");
                }
                break;

            case "Fuego":
                if (enemy.GetComponent<BurnEffect>() == null)
                {
                    BurnEffect burnEffect = enemy.gameObject.AddComponent<BurnEffect>();
                    burnEffect.damagePerSecond = weaponData.statusEffectDamage;
                    burnEffect.tickCount = weaponData.statusEffectTicks;
                    burnEffect.duration = weaponData.statusEffectDuration;
                    burnEffect.spreadChance = 0.3f;
                    burnEffect.spreadRadius = 1.5f;
                    Debug.Log($"{enemy.name} ahora está en llamas!");
                }
                break;

            case "Santificación":
                if (enemy.GetComponent<SanctifyEffect>() == null)
                {
                    SanctifyEffect sanctifyEffect = enemy.gameObject.AddComponent<SanctifyEffect>();
                    sanctifyEffect.duration = weaponData.statusEffectDuration;
                    Debug.Log($"{enemy.name} ha sido afectado por Santificación.");
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
                    Debug.Log($"{enemy.name} ha sido electrocutado y puede propagar el daño!");
                }
                break;
        }
    }
}
