using UnityEngine;

public class BurnEffect : StatusEffect
{
    public GameObject fireEffectPrefab;
    private GameObject fireEffectInstance;

    public float damagePerSecond = 3f;
    public int tickCount = 5;
    public float spreadRadius = 1.5f;
    public float spreadChance = 1f;

    private int ticksApplied = 0;
    private float tickInterval;
    private float burnElapsedTime = 0f; // Renombrado para evitar conflictos con StatusEffect

    private void Start()
    {
        if (fireEffectPrefab != null)
        {
            fireEffectInstance = Instantiate(fireEffectPrefab, transform.position, Quaternion.identity, transform);
        }

        tickInterval = duration / tickCount;
    }

    protected override void Update() // Se añade override correctamente
    {
        if (enemyHealth == null)
        {
            Destroy(this);
            return;
        }

        burnElapsedTime += Time.deltaTime;

        if (burnElapsedTime >= tickInterval && ticksApplied < tickCount)
        {
            enemyHealth.TakeDamage(Mathf.RoundToInt(damagePerSecond));
            ticksApplied++;
            Debug.Log($"{enemyHealth.name} recibe daño de fuego. Vida restante: {enemyHealth.GetCurrentHealth()}");

            SpreadFire();
            burnElapsedTime = 0f; // Reiniciar el temporizador
        }

        if (ticksApplied >= tickCount)
        {
            Destroy(fireEffectInstance);
            Destroy(this);
        }
    }

    private void SpreadFire()
    {
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, spreadRadius);

        foreach (Collider2D col in nearbyEnemies)
        {
            if (col.CompareTag("Enemy") && col.gameObject != gameObject)
            {
                if (Random.value < spreadChance)
                {
                    if (col.GetComponent<BurnEffect>() == null)
                    {
                        BurnEffect newBurn = col.gameObject.AddComponent<BurnEffect>();
                        newBurn.damagePerSecond = damagePerSecond;
                        newBurn.tickCount = tickCount;
                        newBurn.duration = duration;
                        Debug.Log($"{enemyHealth.name} ha propagado el fuego a {col.name}!");
                    }
                }
            }
        }
    }
}
