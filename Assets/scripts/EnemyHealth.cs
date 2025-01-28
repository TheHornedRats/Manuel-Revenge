using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(name + " tom� " + damage + " de da�o. Salud restante: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(name + " ha muerto.");
        Destroy(gameObject);
    }
}
