using UnityEngine;

public enum WeaponType { Melee, Projectile, ExpandingWave, Javelin }

[CreateAssetMenu(fileName = "NewWeapon", menuName = "ScriptableObjects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("Información del Arma")]
    public string weaponName;
    public WeaponType weaponType;
    public GameObject attackPrefab;
    public GameObject explosionEffectPrefab;

    [Header("Estadísticas del Arma")]
    public float baseDamage = 10f;
    public float baseCooldown = 1f;
    public float projectileSpeed = 8f;
    public float explosionRadius = 2f;

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
    public float knockbackForce = 5f;

    private float currentCooldown = 0f;

    // Valores originales para evitar acumulaciones incorrectas
    [SerializeField] private float originalBaseDamage;
    [SerializeField] private float originalBaseCooldown;

    void OnEnable()
    {
        // Guardamos una vez los valores base al cargar el asset
        originalBaseDamage = baseDamage;
        originalBaseCooldown = baseCooldown;
    }

    public void InitWeapon(int weaponLevel)
    {
        float damageFactor = Mathf.Pow(damageIncreasePerLevel, weaponLevel - 1);
        float cooldownFactor = Mathf.Pow(cooldownReductionPerLevel, weaponLevel - 1);

        baseDamage = originalBaseDamage * damageFactor;
        baseCooldown = originalBaseCooldown * cooldownFactor;
        currentCooldown = 0f;
    }

    public bool CanAttack()
    {
        return currentCooldown <= 0f;
    }

    public void UpdateWeapon(Vector3 playerPosition)
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }

        if (!CanAttack()) return;

        PerformAttack(playerPosition);
    }

    public void PerformAttack(Vector3 playerPos)
    {
        Vector3 attackDirection = Vector3.right;

        if (weaponName == "Crucifijo")
        {
            attackDirection = Random.insideUnitCircle.normalized;
        }
        else
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            attackDirection = (mousePos - playerPos).normalized;
        }

        GameObject attack = Instantiate(attackPrefab, playerPos, Quaternion.identity);
        Debug.Log($"{weaponName} atacó.");

        switch (weaponType)
        {
            case WeaponType.Melee:
                Destroy(attack, 0.3f);
                break;

            case WeaponType.Projectile:
                Rigidbody2D rb = attack.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = attackDirection * projectileSpeed;
                    float angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;
                    attack.transform.rotation = Quaternion.Euler(0, 0, angle);
                }
                Destroy(attack, 3f);
                break;

            case WeaponType.ExpandingWave:
                ExpandingWave wave = attack.GetComponent<ExpandingWave>();
                if (wave != null)
                {
                    wave.Initialize(attackDirection);
                }
                break;

            case WeaponType.Javelin:
                Rigidbody2D javelinRb = attack.GetComponent<Rigidbody2D>();
                if (javelinRb != null)
                {
                    javelinRb.velocity = attackDirection * projectileSpeed;
                    attack.transform.right = attackDirection;
                }
                Destroy(attack, 4f);
                break;
        }

        currentCooldown = baseCooldown;
    }
}
