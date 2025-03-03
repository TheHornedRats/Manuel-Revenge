using UnityEngine;
using System.Collections;
using Weapons;

public class Espadon : Weapon
{
    private Collider2D hitbox;
    public float attackDuration = 0.3f;
    public GameObject bleedEffectPrefab; // Prefab del efecto de sangrado

    private void Start()
    {
        if (weaponData == null)
        {
            Debug.LogError("WeaponData no asignado en " + gameObject.name);
            return;
        }
        hitbox = GetComponent<Collider2D>();
        if (hitbox != null)
        {
            hitbox.enabled = false;
        }
    }

    protected override void PerformAttack()
    {
        Debug.Log(weaponData.weaponName + " ha atacado automáticamente.");
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
                enemyHealth.TakeDamage((int)weaponData.baseDamage);
                Debug.Log(weaponData.weaponName + " golpeó a " + collision.name);

                // Aplicar efecto de sangrado si está habilitado en WeaponData
                if (weaponData.appliesStatusEffect && weaponData.statusEffect == "Sangrado")
                {
                    Debug.Log("Aplicando sangrado a " + collision.name);
                    BleedEffect bleedEffect = collision.gameObject.AddComponent<BleedEffect>();
                    bleedEffect.duration = 3f;
                    bleedEffect.damagePerSecond = weaponData.baseDamage * 0.2f;
                    bleedEffect.tickCount = 3;
                    bleedEffect.ApplyEffect(enemyHealth);
                }
            }
        }
    }
}