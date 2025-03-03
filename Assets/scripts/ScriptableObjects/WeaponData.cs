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

    [Header("Level Up Modifiers")]
    public float damageIncreasePerLevel = 1.2f;
    public float cooldownReductionPerLevel = 0.9f;
}
