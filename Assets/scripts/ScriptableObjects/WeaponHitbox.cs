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
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(Mathf.RoundToInt(weaponData.baseDamage)); // Conversión correcta
                ApplyStatusEffect(enemyHealth);
            }
        }
    }


    private void ApplyStatusEffect(EnemyHealth enemy)
    {
        if (weaponData.appliesStatusEffect)
        {
            Debug.Log($"Intentando aplicar efecto {weaponData.statusEffect} a {enemy.name}");

            if (weaponData.statusEffect == "Sangrado" && enemy.GetComponent<BleedEffect>() == null)
            {
                BleedEffect bleedEffect = enemy.gameObject.AddComponent<BleedEffect>();
                bleedEffect.duration = weaponData.statusEffectDuration;
                bleedEffect.damagePerSecond = weaponData.statusEffectDamage;
                bleedEffect.tickCount = weaponData.statusEffectTicks;
                Debug.Log($"{enemy.name} ha sido afectado por Sangrado.");
            }
            if (weaponData.statusEffect == "Fuego" && enemy.GetComponent<BurnEffect>() == null)
            {
                BurnEffect burnEffect = enemy.gameObject.AddComponent<BurnEffect>();
                burnEffect.damagePerSecond = weaponData.statusEffectDamage;
                burnEffect.tickCount = weaponData.statusEffectTicks;
                burnEffect.duration = weaponData.statusEffectDuration;
                burnEffect.spreadChance = 0.3f; // O ajusta según el arma
                burnEffect.spreadRadius = 1.5f;
                Debug.Log($"{enemy.name} ahora está en llamas!");
            }

            if (weaponData.statusEffect == "Santificación" && enemy.GetComponent<SanctifyEffect>() == null)
            {
                SanctifyEffect sanctifyEffect = enemy.gameObject.AddComponent<SanctifyEffect>();
                sanctifyEffect.duration = weaponData.statusEffectDuration;
                sanctifyEffect.ApplyEffect(enemy);
                Debug.Log($"{enemy.name} ha sido afectado por Santificación.");
            }
        }
    }
}
