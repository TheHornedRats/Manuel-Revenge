using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necesario para usar UI

public class PlayerHealth : MonoBehaviour
{
    public int damage;
    public Slider healthSlider;   // Referencia al Slider de la barra de vida
    public int maxHealth = 100;   // Vida m�xima del jugador
    private int currentHealth;    // Vida actual del jugador

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
        maxHealth -= damage;
        Debug.Log(name + " tom� " + damage + " de da�o. Salud restante: " + maxHealth);

        if (maxHealth <= 0)
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
