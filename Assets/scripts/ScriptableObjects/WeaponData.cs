using UnityEngine;

public enum WeaponType { Melee, Projectile, ExpandingWave, Javelin }

[CreateAssetMenu(fileName = "NewWeapon", menuName = "ScriptableObjects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("Información del Arma")]
    public string weaponName;
    public WeaponType weaponType;
    public GameObject attackPrefab;
    public GameObject explosionEffectPrefab; // Efecto de explosión para Bola de Fuego

    [Header("Estadísticas del Arma")]
    public float baseDamage = 10f;
    public float baseCooldown = 1f;
    public float projectileSpeed = 8f;
    public float explosionRadius = 2f; // Radio de explosión

    [Header("Evolución del Arma")]
    public int maxLevel = 5;
    public float damageIncreasePerLevel = 1.2f;
    public float cooldownReductionPerLevel = 0.9f;

    [Header("Efectos de Estado")]
    public bool appliesStatusEffect;
    public string statusEffect;
    public float statusEffectDuration;
    public float statusEffectDamage;
    public int statusEffectTicks;
    public float knockbackForce = 5f; // Para jabalinas

    private float currentCooldown;
    private Vector3 lastMovementInput = Vector3.right;

    public void InitWeapon(int weaponLevel)
    {
        float damageFactor = Mathf.Pow(damageIncreasePerLevel, weaponLevel - 1);
        float cooldownFactor = Mathf.Pow(cooldownReductionPerLevel, weaponLevel - 1);

        baseDamage *= damageFactor;
        baseCooldown *= cooldownFactor;
        currentCooldown = 0;
    }

    public void UpdateWeapon(Vector3 playerPosition)
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
            return;
        }
        PerformAttack(playerPosition);
    }

    public bool CanAttack()
    {
        return currentCooldown <= 0;
    }

    public void PerformAttack(Vector3 playerPos)
    {
        if (currentCooldown > 0) return;

        Vector3 attackDirection = lastMovementInput.normalized;
        if (weaponType == WeaponType.Projectile || weaponType == WeaponType.Javelin)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            attackDirection = (mousePosition - playerPos).normalized;
            attackDirection.z = 0;
        }

        float angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;
        GameObject attack = Instantiate(attackPrefab, playerPos, Quaternion.Euler(0, 0, angle));

        switch (weaponType)
        {
            case WeaponType.Melee:
                Destroy(attack, 0.3f);
                break;

            case WeaponType.Projectile:
                Rigidbody2D rb = attack.GetComponent<Rigidbody2D>();
                if (rb != null) rb.velocity = attackDirection * projectileSpeed;
                attack.AddComponent<WeaponHitbox>().Setup(this); // Se asigna WeaponHitbox
                break;

            case WeaponType.ExpandingWave:
                ExpandingWave expandingWave = attack.GetComponent<ExpandingWave>();
                if (expandingWave != null) expandingWave.Initialize(attackDirection);
                break;

            case WeaponType.Javelin:
                Rigidbody2D javelinRb = attack.GetComponent<Rigidbody2D>();
                if (javelinRb != null) javelinRb.velocity = attackDirection * projectileSpeed;
                attack.transform.right = attackDirection;
                attack.AddComponent<WeaponHitbox>().Setup(this); // Se asigna WeaponHitbox
                break;
        }

        currentCooldown = baseCooldown;
    }
}