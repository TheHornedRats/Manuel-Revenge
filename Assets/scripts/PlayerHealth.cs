using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necesario para usar UI

public class PlayerHealth : MonoBehaviour
{
    public int damage;
    public Slider healthSlider;   // Referencia al Slider de la barra de vida
    public int maxHealth = 100;   // Vida máxima del jugador
    private int currentHealth;    // Vida actual del jugador

    void Start()
    {
        currentHealth = maxHealth;           // Inicializa la vida al máximo
        healthSlider.maxValue = maxHealth;   // Configura el valor máximo del slider
        healthSlider.value = currentHealth;  // Establece el valor actual del slider
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
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Asegura que la vida no sea menor a 0
        healthSlider.value = currentHealth;  // Actualiza la barra de vida
        Debug.Log(name + " tomó " + damage + " de daño. Salud restante: " + maxHealth);

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