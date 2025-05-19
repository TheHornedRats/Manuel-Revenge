using UnityEngine;

public class FireElementalBoss : BossEnemy
{
    [Header("Fire Elemental AOE Abilities")]
    public GameObject meteorShotPrefab;        // Ataque 1: meteorito rápido
    public GameObject fireRingPrefab;          // Ataque 2: anillo de fuego
    public GameObject fireTornadoPrefab;       // Ataque 3: pilares móviles

    [Header("Meteor Shot Settings")]
    public float meteorSpeed = 20f;
    public int meteorDamage = 20;

    public Transform player;

    protected override void UseRandomAbility()
    {
        isAttacking = true;

        if (rb != null)
        {
            storedVelocity = rb.velocity;
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }

        int abilityIndex = Random.Range(0, 3); // Solo 3 habilidades ahora

        switch (abilityIndex)
        {
            case 0: // Meteor Shot
                animator?.SetTrigger("Attack1");
                ShootMeteor();
                Invoke(nameof(ResetAttackState), 1.0f);
                break;

            case 1: // Fire Ring (sin cambios por ahora)
                animator?.SetTrigger("Attack2");
                Instantiate(fireRingPrefab, transform.position, Quaternion.identity);
                Invoke(nameof(ResetAttackState), 1.0f);
                break;

            case 2: // Fire Tornadoes
                animator?.SetTrigger("Attack3");
                SpawnTornadoes();
                Invoke(nameof(ResetAttackState), 1.0f);
                break;
        }
    }

    private void ShootMeteor()
    {
        if (meteorShotPrefab == null || player == null) return;

        GameObject meteor = Instantiate(meteorShotPrefab, transform.position, Quaternion.identity);
        Vector2 dir = (player.position - transform.position).normalized;

        meteor.GetComponent<MeteorShot>()?.Initialize(dir);
    }

    private void SpawnTornadoes()
    {
        int count = 5;
        for (int i = 0; i < count; i++)
        {
            Vector3 randomPos = transform.position + (Vector3)(Random.insideUnitCircle * 4f);
            Instantiate(fireTornadoPrefab, randomPos, Quaternion.identity);
        }
    }
}
