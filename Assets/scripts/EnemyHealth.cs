using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;
    public int damage;
    public int puntuacion;

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
        Debug.Log(name + " tomó " + damage + " de daño. Salud restante: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(name + " ha muerto.");
        Destroy(gameObject);
        ScoreManager.instance.AddScore(5);
    }
}
