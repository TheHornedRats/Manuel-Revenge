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
    
    [Header("Partículas de Efecto")]
    public GameObject burnParticlesPrefab;
    public GameObject sanctifyParticlesPrefab;
    public GameObject electrocuteParticlesPrefab;

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
        // Reducir el cooldown siempre, para que se pueda disparar de nuevo al alcanzar 0
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }

        // Para la Jabalina: Dispara al hacer clic si el cooldown ha finalizado.
        if (weaponType == WeaponType.Javelin)
        {
            if (Input.GetMouseButtonDown(0) && CanAttack())
            {
                PerformAttack(playerPosition);
            }
        }
        // Para el Crucifijo: Se dispara automáticamente cuando el cooldown llega a 0.
        else if (weaponName == "Crucifijo")
        {
            if (CanAttack())
            {
                PerformAttack(playerPosition);
            }
        }
        // Para el resto de armas (Melee, Projectile, ExpandingWave)
        else
        {
            if (currentCooldown <= 0)
            {
                PerformAttack(playerPosition);
            }
        }
    }

    public bool CanAttack()
    {
        return currentCooldown <= 0;
    }

    public void PerformAttack(Vector3 playerPos)
    {
        if (currentCooldown > 0) return;

        Vector3 attackDirection = Vector3.right;

        // Si es Crucifijo, se asigna una dirección aleatoria
        if (weaponName == "Crucifijo")
        {
            attackDirection = Random.insideUnitCircle.normalized;

        }
        else
        {
            // Para los demás, se apunta hacia el mouse
            if (Input.mousePosition != null)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                attackDirection = (mousePos - playerPos).normalized;
           
            }
        }

        GameObject attack = Instantiate(attackPrefab, playerPos, Quaternion.identity);
        Debug.Log(weaponName + " atacó.");

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
                ExpandingWave expandingWave = attack.GetComponent<ExpandingWave>();
                if (expandingWave != null)
                {
                    expandingWave.Initialize(attackDirection);
                }
                else
                {
                    Debug.LogError("ExpandingWave no encontrado.");
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

        // Reinicia el cooldown para permitir nuevos disparos
        currentCooldown = baseCooldown;
    }
}
