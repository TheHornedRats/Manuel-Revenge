using UnityEngine;

public enum WeaponType
{
    Melee,
    Projectile
}

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Información del Arma")]
    public string weaponName;
    public WeaponType weaponType;
    public float baseDamage;
    public float baseCooldown;
    public GameObject attackPrefab;

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

    private float weaponCooldown;
    private float currentCooldown;

    Vector3 lastMovementInput;

    public void InitWeapon(int weaponLevel)
    {
        baseDamage *= Mathf.Pow(damageIncreasePerLevel, weaponLevel - 1);
        baseCooldown *= Mathf.Pow(cooldownReductionPerLevel, weaponLevel - 1);
        weaponCooldown = baseCooldown;
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
        currentCooldown = weaponCooldown;
    }

    void PerformAttack(Vector3 playerPos)
    {
        if (attackPrefab == null)
        {
            Debug.LogWarning("No hay prefab asignado para " + weaponName);
            return;
        }

        // Obtener la dirección en la que se mueve el jugador
        Vector3 movementInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (movementInput != Vector3.zero)
        {
            lastMovementInput = movementInput;
        }

        GameObject attack = Instantiate(attackPrefab, playerPos + lastMovementInput.normalized, Quaternion.identity);
        WeaponHitbox hitbox = attack.GetComponent<WeaponHitbox>();

        if (hitbox != null)
        {
            hitbox.Setup(this);
        }
        else
        {
            Debug.LogError("WeaponHitbox no encontrado en el prefab de " + weaponName);
        }

        Debug.Log(weaponName + " atacó.");

        switch (weaponType)
        {
            case WeaponType.Melee:
                Destroy(attack, 0.3f);
                break;
            case WeaponType.Projectile:
                Rigidbody2D rb = attack.GetComponent<Rigidbody2D>();
                if (rb != null) rb.velocity = (Vector2)lastMovementInput.normalized * 5f;
                Destroy(attack, 3f);
                break;
        }
    }
}
