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
    public GameObject uiScreen;

    void Start()
    {
        currentHealth = maxHealth;              // Inicializa la vida al máximo
        healthSlider.maxValue = maxHealth;      // Configura el valor máximo del slider
        healthSlider.value = currentHealth;     // Establece el valor actual del slider
        deathScreen.SetActive(false);           // Desactiva la pantalla de muerte al empezar
        uiScreen.SetActive(true);
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

        currentHealth -= damage; // Reducimos `currentHealth` en lugar de `maxHealth`

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Asegura que la vida no sea menor a 0
        healthSlider.value = currentHealth;  // Actualiza la barra de vida
        Debug.Log(name + " tomó " + damage + " de daño. Salud restante: " + currentHealth);

        if (currentHealth <= 0)  // Cambio aquí, comparar con currentHealth, no maxHealth

        if (currentHealth <= 0)

        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        int previousHealth = currentHealth; // Guardamos la vida antes de curar
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        healthSlider.value = currentHealth; // Asegura que la UI de la barra de vida refleje el cambio

        Debug.Log($" El jugador ha sido curado por {amount} puntos de vida. Vida antes: {previousHealth}, Vida actual: {currentHealth}");
    }

    private void Die()
    {
        Debug.Log(name + " ha muerto.");
        if (deathScreen != null)
        {
            deathScreen.SetActive(true);  // Activa la pantalla de muerte
            uiScreen.SetActive(false);

            Debug.Log("Pantalla de muerte");
        }
        else
        {
            Debug.Log("Error, no se asigna pantalla muerte");
        }

        Time.timeScale = 0f; // Detiene el tiempo
        Camera.main.transform.SetParent(null);
        gameObject.SetActive(false); // Desactiva al jugador (puedes modificar esto si prefieres destruirlo)
    }
}
