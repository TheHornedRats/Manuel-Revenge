using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public float baseDamage;
    public float baseCooldown;
    public bool appliesStatusEffect;
    public string statusEffect;
    public int maxLevel = 5;
    public GameObject attackPrefab;

    [Header("Level Up Modifiers")]
    public float damageIncreasePerLevel = 1.2f;
    public float cooldownReductionPerLevel = 0.9f;

    private float weaponCooldown = 0;
    private float currentCooldown = 0;

    private Vector3 playerPos;

    public void InitWeapon(int weaponLevel)
    {
        weaponCooldown = baseCooldown * Mathf.Pow(cooldownReductionPerLevel, weaponLevel - 1);
        Debug.Log($"{weaponName} inicializado con cooldown: {weaponCooldown}");
    }

    public void UpdateWeapon(Vector3 playerPosition)
    {
        playerPos = playerPosition;
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }

        if (currentCooldown <= 0)
        {
            PerformAttack();
            currentCooldown = weaponCooldown; // Reiniciar cooldown tras el ataque
        }
    }

    void PerformAttack()
    {
        Debug.Log("ATACA " + weaponName);

        if (attackPrefab != null)
        {
            GameObject attack = Instantiate(attackPrefab, playerPos, Quaternion.identity);
            EspadonAttack attackAttributes = attack.GetComponent<EspadonAttack>();
            if (attackAttributes != null)
            {
                attackAttributes.damage = baseDamage;

            }
        }
        else
        {
            Debug.LogWarning("No hay prefab asignado para " + weaponName);
        }
    }
}
