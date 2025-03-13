using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public int puntuacion;
    public GameObject xp5Prefab;
    public GameObject xp10Prefab;
    public GameObject xp20Prefab;
    public float dropRange = 1.0f;
    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"[DAÑO] {name} sufrió {damage} de daño. Salud restante: {currentHealth}");

        // Si el enemigo tiene Santificación, curar al jugador
        SanctifyEffect sanctifyEffect = GetComponent<SanctifyEffect>();
        if (sanctifyEffect != null)
        {
            HealPlayer(damage);
        }

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    public int GetCurrentHealth() // Se renombra para que coincida con el llamado en BurnEffect.cs
    {
        return currentHealth;
    }

    private void Die()
    {
        Debug.Log(name + " ha muerto.");
        isDead = true;

        DropXPItems();
        Debug.Log("Intentando destruir " + name);

        Destroy(gameObject);
        ScoreManager.instance.AddScore(5);
    }

    private void DropXPItems()
    {
        Vector3 dropPosition1 = transform.position + new Vector3(Random.Range(-dropRange, dropRange), Random.Range(-dropRange, dropRange), 0);
        Vector3 dropPosition2 = transform.position + new Vector3(Random.Range(-dropRange, dropRange), Random.Range(-dropRange, dropRange), 0.1f);
        Vector3 dropPosition3 = transform.position + new Vector3(Random.Range(-dropRange, dropRange), Random.Range(-dropRange, dropRange), -0.1f);

        Instantiate(xp5Prefab, dropPosition1, Quaternion.identity);
        Instantiate(xp10Prefab, dropPosition2, Quaternion.identity);
        Instantiate(xp20Prefab, dropPosition3, Quaternion.identity);
    }

    private void HealPlayer(int damageDealt)
    {
        float healPercentage = 0.1f;
        int healAmount = Mathf.RoundToInt(damageDealt * healPercentage);

        if (healAmount > 0)
        {
            PlayerHealth player = FindObjectOfType<PlayerHealth>();
            if (player != null)
            {
                player.Heal(healAmount);
                Debug.Log($"El jugador se cura {healAmount} de vida gracias a la Santificación.");
            }
        }
    }
}
