using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; // Necesario para usar TextMeshProUGUI

public class PlayerHealth : MonoBehaviour
{
    public int damage;
    public Slider healthSlider;
    public int maxHealth = 100;
    private int currentHealth;
    public GameObject deathScreen;
    public GameObject uiScreen;
    public TextMeshProUGUI vidaTexto; // Texto que muestra la vida

    public Color vidaNormalColor = Color.white;
    public Color vidaBajaColor = Color.red;
    public float umbralVidaBaja = 0.25f; // Porcentaje a partir del cual el texto cambia de color

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        deathScreen.SetActive(false);
        uiScreen.SetActive(true);

        ActualizarTextoVida();
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
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthSlider.value = currentHealth;
        Debug.Log(name + " tomó " + damage + " de daño. Salud restante: " + currentHealth);

        ActualizarTextoVida();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        int previousHealth = currentHealth;
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        healthSlider.value = currentHealth;

        Debug.Log($" El jugador ha sido curado por {amount} puntos de vida. Vida antes: {previousHealth}, Vida actual: {currentHealth}");

        ActualizarTextoVida();
    }

    private void ActualizarTextoVida()
    {
        if (vidaTexto != null)
        {
            vidaTexto.text = "Vida: " + currentHealth + " / " + maxHealth;

            float porcentaje = (float)currentHealth / maxHealth;
            if (porcentaje <= umbralVidaBaja)
            {
                vidaTexto.color = vidaBajaColor;
            }
            else
            {
                vidaTexto.color = vidaNormalColor;
            }
        }
    }

    private void Die()
    {
        Debug.Log(name + " ha muerto.");
        if (deathScreen != null)
        {
            deathScreen.SetActive(true);
            uiScreen.SetActive(false);
            Debug.Log("Pantalla de muerte");
        }
        else
        {
            Debug.Log("Error, no se asigna pantalla muerte");
        }

        Time.timeScale = 0f;
        Camera.main.transform.SetParent(null);
        gameObject.SetActive(false);
    }
}
