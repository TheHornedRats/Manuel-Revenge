using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public int puntuacion;
    public GameObject xpPrefab;
    public float dropRange = 1.0f;
    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"[DA�O] {name} sufri� {damage} de da�o. Salud restante: {currentHealth}");

        // Si el enemigo tiene Santificaci�n, curar al jugador
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

    public int GetHealth() // M�todo p�blico para acceder a la salud actual
    {
        return currentHealth;
    }

    private void Die()
    {
        Debug.Log(name + " ha muerto.");
        isDead = true;

        DropXPItems();
        Destroy(gameObject);
        ScoreManager.instance.AddScore(5);
    }

    private void DropXPItems()
    {
        Vector3 dropPosition = transform.position + new Vector3(Random.Range(-dropRange, dropRange), Random.Range(-dropRange, dropRange), 0);
        Instantiate(xpPrefab, dropPosition, Quaternion.identity);
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
                Debug.Log($"El jugador se cura {healAmount} de vida gracias a la Santificaci�n.");
            }
        }
    }
}
