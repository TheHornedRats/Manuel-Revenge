using UnityEngine;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour
{
    public List<Weapon> weapons = new List<Weapon>();

    void Update()
    {
        Vector3 playerPosition = transform.position;
        foreach (Weapon weapon in weapons)
        {
            if (weapon.weaponData != null)
            {
                weapon.TryAttack(playerPosition); // Ahora cada arma maneja su propio cooldown
            }
        }
    }

    public void LevelUp(int newLevel)
    {
        foreach (Weapon weapon in weapons)
        {
            weapon.UpgradeWeapon(); // Mejora todas las armas cuando el jugador sube de nivel
        }
    }
}
