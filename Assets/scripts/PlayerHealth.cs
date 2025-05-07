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

    public AudioClip damageSound;
    public AudioClip deathSound; // <- Nuevo campo para el sonido de muerte
    private AudioSource audioSource;

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

        if (damageSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
        else
        {
            Debug.Log("Falta asignar 'damageSound' o 'AudioSource' en el objeto con PlayerHealth.");
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

        if (deathSound != null)
        {
            GameObject tempGO = new GameObject("TempAudio"); // Objeto temporal
            tempGO.transform.position = transform.position;

            AudioSource tempSource = tempGO.AddComponent<AudioSource>();
            tempSource.clip = deathSound;
            tempSource.volume = 2.0f; // <- Puedes poner más de 1 aquí
            tempSource.Play();

            Destroy(tempGO, deathSound.length); // Eliminar al acabar el sonido
        }

        if (deathScreen != null)
        {
            deathScreen.SetActive(true);
            uiScreen.SetActive(false);
            Debug.Log("Pantalla de muerte");
        }

        Time.timeScale = 0f;
        Camera.main.transform.SetParent(null);
        gameObject.SetActive(false);
    }
}