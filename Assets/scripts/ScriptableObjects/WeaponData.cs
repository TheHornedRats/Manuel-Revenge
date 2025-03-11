using UnityEngine;

public enum WeaponType { Melee, Projectile, ExpandingWave, Javelin }

[CreateAssetMenu(fileName = "NewWeapon", menuName = "ScriptableObjects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("Información del Arma")]
    public string weaponName;
    public WeaponType weaponType;
    public GameObject attackPrefab;

    [Header("Estadísticas del Arma")]
    public float baseDamage = 10f;
    public float baseCooldown = 1f;
    public float projectileSpeed = 8f;

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
    public float knockbackForce = 5f; // Fuerza de retroceso para la jabalina

    private float weaponCooldown;
    private float currentCooldown;
    private Vector3 lastMovementInput = Vector3.right;

    public void InitWeapon(int weaponLevel)
    {
        float damageFactor = Mathf.Pow(damageIncreasePerLevel, weaponLevel - 1);
        float cooldownFactor = Mathf.Pow(cooldownReductionPerLevel, weaponLevel - 1);

        baseDamage *= damageFactor;
        baseCooldown *= cooldownFactor;

        weaponCooldown = baseCooldown;
        currentCooldown = 0; // Se asegura que el cooldown comienza en 0 al nivel inicial
    }

    public void UpdateWeapon(Vector3 playerPosition)
    {
        if (weaponType == WeaponType.Javelin)
        {
            if (Input.GetMouseButtonDown(0) && CanAttack()) // Disparar con click izquierdo
            {
                PerformAttack(playerPosition);
            }
        }
        else
        {
            if (currentCooldown > 0)
            {
                currentCooldown -= Time.deltaTime;
                return;
            }
            PerformAttack(playerPosition);
        }
    }

    public bool CanAttack()
    {
        return currentCooldown <= 0;
    }

    public void PerformAttack(Vector3 playerPos)
    {
        if (currentCooldown > 0) return;

        Vector3 attackDirection = lastMovementInput.normalized; // Dirección predeterminada (última dirección del jugador)

        // Obtener la dirección hacia el mouse si es un proyectil o jabalina
        if (weaponType == WeaponType.Projectile || weaponType == WeaponType.Javelin)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            attackDirection = (mousePosition - playerPos).normalized;
            attackDirection.z = 0;
        }

        // Calcular la rotación para que apunte en la dirección correcta
        float angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;

        // Instanciar el ataque con la rotación correcta
        GameObject attack = Instantiate(attackPrefab, playerPos, Quaternion.Euler(0, 0, angle));
        Debug.Log(weaponName + " atacó en dirección " + attackDirection);

        switch (weaponType)
        {
            case WeaponType.Melee:
                Destroy(attack, 0.3f);
                break;

            case WeaponType.Projectile:
                Rigidbody2D rb = attack.GetComponent<Rigidbody2D>();
                if (rb != null) rb.velocity = attackDirection * projectileSpeed;
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
                    Debug.LogError("ExpandingWave no encontrado en el Crucifijo.");
                }
                break;

            case WeaponType.Javelin: // Lanza Relámpago
                Rigidbody2D javelinRb = attack.GetComponent<Rigidbody2D>();
                if (javelinRb != null)
                {
                    javelinRb.velocity = attackDirection * projectileSpeed;
                }
                attack.transform.right = attackDirection; // Apunta correctamente
                Destroy(attack, 4f);
                break;
        }

        currentCooldown = baseCooldown; // Reiniciar cooldown
    }
}
