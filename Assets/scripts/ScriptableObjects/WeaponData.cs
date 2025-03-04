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
        weaponCooldown = weaponLevel * cooldownReductionPerLevel * baseCooldown;
    }

    public void UpdateWeapon(Vector3 playerPosition){
        currentCooldown += Time.deltaTime;
        if(currentCooldown >= weaponCooldown)
        {
            currentCooldown = 0;
            PerformAttack();
        }
    }

    void PerformAttack()
    {
        Debug.Log("ATACA " + weaponName);
        if(attackPrefab != null)
        {
            Instantiate(attackPrefab, playerPos, Quaternion.identity);
        }
    }
}
