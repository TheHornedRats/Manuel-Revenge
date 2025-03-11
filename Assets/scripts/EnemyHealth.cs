using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;
    public int damage;
    public int puntuacion;
<<<<<<< Updated upstream
=======
    public GameObject xp5Prefab;
    public GameObject xp10Prefab;
    public GameObject xp20Prefab;
    public float dropRange = 1.0f;
    private bool isDead = false;
>>>>>>> Stashed changes

    public GameObject xp5Prefab;
    public GameObject xp10Prefab;
    public GameObject xp20Prefab;
    public float dropRange = 1.0f;

    void Start()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Atack")
        {
            TakeDamage(damage);
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(name + " sufrió " + damage + " de daño. Salud restante: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(name + " ha muerto.");

        DropXPItems();

        Destroy(gameObject);
        ScoreManager.instance.AddScore(5);
    }

    private void DropXPItems()
    {
        Vector3 dropPosition1 = transform.position + new Vector3(Random.Range(-dropRange, dropRange), Random.Range(-dropRange, dropRange), 0);
        Vector3 dropPosition2 = transform.position + new Vector3(Random.Range(-dropRange, dropRange), Random.Range(-dropRange, dropRange), 0.1f);
<<<<<<< Updated upstream
        Vector3 dropPosition3 = transform.position + new Vector3(Random.Range(-dropRange, dropRange), Random.Range(-dropRange, dropRange), 0.2f);

        // Instanciar los objetos de XP en posiciones aleatorias
        Instantiate(xp5Prefab, dropPosition1 , Quaternion.identity);
        Instantiate(xp10Prefab, dropPosition2, Quaternion.identity);
        Instantiate(xp20Prefab, dropPosition3, Quaternion.identity);
=======
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
>>>>>>> Stashed changes
    }
}
