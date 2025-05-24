using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int damage;
    public Slider healthSlider;
    public int maxHealth = 100;
    private int currentHealth;
    public GameObject deathScreen;
    public GameObject uiScreen;
    public TextMeshProUGUI vidaTexto;

    public Color vidaNormalColor = Color.white;
    public Color vidaBajaColor = Color.red;
    public float umbralVidaBaja = 0.25f;

    public AudioClip damageSound1; // Referencia al primer sonido de daño
    public AudioClip damageSound2; // Referencia al segundo sonido de daño
    public AudioClip deathSound;

    private AudioSource audioSource;
    private bool usarPrimerSonido = true; // Alternador

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        deathScreen.SetActive(false);
        uiScreen.SetActive(true);

        audioSource = GetComponent<AudioSource>();
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

        // Alternar entre sonidos de daño
        if (audioSource != null)
        {
            if (usarPrimerSonido && damageSound1 != null)
            {
                audioSource.PlayOneShot(damageSound1);
            }
            else if (!usarPrimerSonido && damageSound2 != null)
            {
                audioSource.PlayOneShot(damageSound2);
            }

            usarPrimerSonido = !usarPrimerSonido; //Cambia para la próxima vez
        }
        else
        {
            Debug.LogWarning("Falta el AudioSource en PlayerHealth.");
        }

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

        Debug.Log($"El jugador ha sido curado por {amount} puntos de vida. Vida antes: {previousHealth}, Vida actual: {currentHealth}");

        ActualizarTextoVida();
    }

    private void ActualizarTextoVida()
    {
        if (vidaTexto != null)
        {
            vidaTexto.text = "Vida: " + currentHealth + " / " + maxHealth;

            float porcentaje = (float)currentHealth / maxHealth;
            vidaTexto.color = porcentaje <= umbralVidaBaja ? vidaBajaColor : vidaNormalColor;
        }
    }

    private void Die()
    {
        Debug.Log("El jugador ha muerto. Activando pantalla de muerte.");

        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        if (deathScreen != null)
        {
            deathScreen.SetActive(true);
            uiScreen.SetActive(false);
            Debug.Log("Pantalla de muerte activada.");
        }
        else
        {
            Debug.LogWarning("deathScreen no asignado.");
        }

        Time.timeScale = 0f;
        Camera.main.transform.SetParent(null);
        gameObject.SetActive(false);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

}