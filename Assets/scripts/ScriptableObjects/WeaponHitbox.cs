using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    private WeaponData weaponData;

    public void Setup(WeaponData data)
    {
        weaponData = data;
    }

    private void Start()
    {
        if (weaponData == null)
        {
            Debug.LogError("WeaponData es NULL en WeaponHitbox.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (weaponData == null)
        {
            Debug.LogError("WeaponData es NULL en WeaponHitbox.");
            return;
        }

        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage((int)weaponData.baseDamage);
                Debug.Log(weaponData.weaponName + " golpeó a " + collision.name);
            }
        }
    }
}
