using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int hp = 100;
    public int damage;

    void Start()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Atack")
        {
            TakeDamagePlayer(damage);
        }
    }
    public void TakeDamagePlayer(int damage)
    {
        hp -= damage;
        Debug.Log(name + " tomó " + damage + " de daño. Salud restante: " + hp);

        if (hp <= 0)
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
