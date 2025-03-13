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
    private float burnElapsedTime = 0f;

    private void Start()
    {
        if (fireEffectPrefab != null)
        {
            fireEffectInstance = Instantiate(fireEffectPrefab, transform.position, Quaternion.identity, transform);
        }

        tickInterval = duration / tickCount;
    }

    protected override void Update()
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
            Debug.Log($"{enemyHealth.name} recibe da�o de fuego. Vida restante: {enemyHealth.GetHealth()}");
            burnElapsedTime = 0f;
        }

        if (ticksApplied >= tickCount)
        {
            Destroy(fireEffectInstance);
            Destroy(this);
        }
    }
}