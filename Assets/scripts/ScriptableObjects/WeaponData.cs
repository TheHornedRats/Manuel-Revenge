using UnityEngine;

public enum WeaponType { Melee, Projectile, ExpandingWave }

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

    public void InitWeapon(int weaponLevel)
    {
        float damageFactor = Mathf.Pow(damageIncreasePerLevel, weaponLevel - 1);
        float cooldownFactor = Mathf.Pow(cooldownReductionPerLevel, weaponLevel - 1);

        baseDamage *= damageFactor;
        baseCooldown *= cooldownFactor;

        weaponCooldown = baseCooldown;
        currentCooldown = 0; //  Se asegura que el cooldown comienza en 0 al nivel inicial
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
        if (currentCooldown > 0) return; // Evita ataques múltiples sin cooldown

        GameObject attack = Instantiate(attackPrefab, playerPos, Quaternion.identity);
        Debug.Log(weaponName + " atacó.");

        switch (weaponType)
        {
            case WeaponType.Melee:
                Destroy(attack, 0.3f);
                break;

            case WeaponType.Projectile:
                Rigidbody2D rb = attack.GetComponent<Rigidbody2D>();
                if (rb != null) rb.velocity = Vector2.right * 5f;
                Destroy(attack, 3f);
                break;

            case WeaponType.ExpandingWave: // Crucifijo (Onda expansiva)
                ExpandingWave expandingWave = attack.GetComponent<ExpandingWave>();
                if (expandingWave != null)
                {
                    Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerPos).normalized;
                    expandingWave.Initialize(direction);
                }
                else
                {
                    Debug.LogError("ExpandingWave no encontrado en el Crucifijo.");
                }
                break;
        }

        currentCooldown = baseCooldown; // Reiniciar cooldown
    }
}
