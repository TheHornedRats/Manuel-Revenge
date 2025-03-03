using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;
    public int damage;
    public int puntuacion;

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
        Vector3 dropPosition = transform.position + new Vector3(Random.Range(-dropRange, dropRange), Random.Range(-dropRange, dropRange), 0);

        // Instanciar los objetos de XP en posiciones aleatorias
        Instantiate(xp5Prefab, dropPosition, Quaternion.identity);
        Instantiate(xp10Prefab, dropPosition, Quaternion.identity);
        Instantiate(xp20Prefab, dropPosition, Quaternion.identity);
    }
}
