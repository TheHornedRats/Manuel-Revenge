using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necesario para usar UI
using UnityEngine.SceneManagement; // Necesario para cargar escenas

public class PlayerHealth : MonoBehaviour
{
    public int damage;
    public Slider healthSlider;                 // Referencia al Slider de la barra de vida
    public int maxHealth = 100;                 // Vida máxima del jugador
    private int currentHealth;                  // Vida actual del jugador
    public GameObject deathScreen;              // Pantalla de muerte

    void Start()
    {
        currentHealth = maxHealth;              // Inicializa la vida al máximo
        healthSlider.maxValue = maxHealth;      // Configura el valor máximo del slider
        healthSlider.value = currentHealth;     // Establece el valor actual del slider
        deathScreen.SetActive(false);           // Desactiva la pantalla de muerte al empezar
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
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Asegura que la vida no sea menor a 0
        healthSlider.value = currentHealth;  // Actualiza la barra de vida
        Debug.Log(name + " tomó " + damage + " de daño. Salud restante: " + maxHealth);

        if (currentHealth <= 0)  // Cambio aquí, comparar con currentHealth, no maxHealth
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(name + " ha muerto.");
        if (deathScreen != null)
        {
            deathScreen.SetActive(true);  // Activa la pantalla de muerte
            Debug.Log("Pantalla de muerte");
        }
        else
        {
            Debug.Log("Error, no se asigna pantalla muerte");
        }

        Time.timeScale = 0f; // Detiene el tiempo
        gameObject.SetActive(false); // Desactiva al jugador (puedes modificar esto si prefieres destruirlo)
    }
}
