
namespace Weapons
{
    using UnityEngine;

    public class Weapon : MonoBehaviour
    {
        public WeaponData weaponData;
        public string weaponName;
        private float damage;
        private float cooldown;
        private int level = 1;
        private float lastAttackTime;

        private void Start()
        {
            Debug.Log("Hola");

            if (weaponData != null)
            {
                weaponName = weaponData.weaponName;
                damage = weaponData.baseDamage;
                cooldown = weaponData.baseCooldown;

                Debug.Log($"WeaponData asignado: {weaponData.weaponName}, Cooldown: {weaponData.baseCooldown}");

                // Si el cooldown es 0, forzar un valor correcto
                if (cooldown <= 0)
                {
                    Debug.LogWarning($"El cooldown del arma {weaponName} estaba en {cooldown}. Se fuerza a 1.5");
                    cooldown = 1.5f;
                }
            }
            else
            {
                Debug.LogError("WeaponData NO asignado en " + gameObject.name);
            }
        }



        public void TryAttack()
        {
            float tiempoActual = Time.time;
            float tiempoSiguienteAtaque = lastAttackTime + cooldown;

            Debug.Log($"Intentando atacar... Tiempo actual: {tiempoActual}, Último ataque: {lastAttackTime}, Cooldown: {cooldown}, Siguiente ataque permitido: {tiempoSiguienteAtaque}");

            if (tiempoActual >= tiempoSiguienteAtaque)
            {
                PerformAttack();
                lastAttackTime = tiempoActual; // Se actualiza el tiempo del último ataque
                Debug.Log("Ataque realizado. Nuevo tiempo de ataque registrado: " + lastAttackTime);
            }
            else
            {
                Debug.Log("Ataque bloqueado por cooldown.");
            }
        }


        protected void PerformAttack(){}

        public void UpgradeWeapon()
        {
            if (level < weaponData.maxLevel)
            {
                level++;
                damage *= weaponData.damageIncreasePerLevel;
                cooldown *= weaponData.cooldownReductionPerLevel;
                Debug.Log(weaponData.weaponName + " mejorado al nivel " + level);
            }
        }
    }
}