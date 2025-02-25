using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [Header("Weapon Stats")]
        public string weaponName;
        public int level = 1;
        public float damage;
        public float cooldown;
        public string effect; // Puede ser "Sangrado", "Fuego", "Veneno", etc.

        private float lastAttackTime;

        public void TryAttack()
        {
            if (Time.time >= lastAttackTime + cooldown)
            {
                PerformAttack();
                lastAttackTime = Time.time;
            }
        }

        protected abstract void PerformAttack();

        public void UpgradeWeapon()
        {
            level++;
            damage *= 1.2f; // Aumenta el daño un 20%
            cooldown *= 0.9f; // Reduce el cooldown un 10%
        }
    }
}
