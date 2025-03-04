using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public WeaponData weaponData;
    public string weaponName;
    private float damage;
    private int level = 1;

    private void Start()
    {
        if (weaponData != null)
        {
            weaponName = weaponData.weaponName;
            damage = weaponData.baseDamage;
            weaponData.InitWeapon(level);
        }
    }

    public void TryAttack(Vector3 playerPosition)
    {
        if (weaponData == null)
        {
            Debug.LogError("WeaponData no asignado en " + gameObject.name);
            return;
        }

        weaponData.UpdateWeapon(playerPosition); // Deja que WeaponData maneje el cooldown y ataques
    }

    public void UpgradeWeapon()
    {
        if (level < weaponData.maxLevel)
        {
            level++;
            damage *= weaponData.damageIncreasePerLevel;
            weaponData.InitWeapon(level); // Reinicializar cooldown con nivel actualizado
            Debug.Log(weaponData.weaponName + " mejorado al nivel " + level);
        }
    }
}
