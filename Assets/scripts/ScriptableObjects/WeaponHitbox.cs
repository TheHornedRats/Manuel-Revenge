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

            // Para proyectiles con efecto de explosi�n, instanciar la explosi�n
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
                if (enemy.GetComponent<BleedEffect>() == null)
                {
                    BleedEffect bleedEffect = enemy.gameObject.AddComponent<BleedEffect>();
                    bleedEffect.duration = weaponData.statusEffectDuration;
                    bleedEffect.damagePerSecond = weaponData.statusEffectDamage;
                    bleedEffect.tickCount = weaponData.statusEffectTicks;
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
                }
                break;

            case "Santificaci�n":
                if (enemy.GetComponent<SanctifyEffect>() == null)
                {
                    SanctifyEffect sanctifyEffect = enemy.gameObject.AddComponent<SanctifyEffect>();
                    sanctifyEffect.duration = weaponData.statusEffectDuration;
                    sanctifyEffect.ApplyEffect(enemy);
                }
                break;
        }
    }
}
