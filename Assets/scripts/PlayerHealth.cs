using System.Collections;
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

    public AudioClip damageSound1;
    public AudioClip damageSound2;
    public AudioClip deathSound;

    private AudioSource audioSource;
    private bool usarPrimerSonido = true;

    // Sprite flash
    private SpriteRenderer spriteRenderer;
    public float flashDuration = 0.1f;
    public Color flashColor = Color.red;
    private Color originalColor;

    // Cámara
    private CameraShake cameraShake;

    // Imagen UI efecto daño
    public Image externalDamageEffect;
    public float externalEffectDuration = 0.3f;
    public AnimationCurve fadeCurve; // opcional para suavizar la transparencia

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        deathScreen.SetActive(false);
        uiScreen.SetActive(true);

        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;

        cameraShake = Camera.main?.GetComponent<CameraShake>();

        // Asegurar que la imagen UI empieza invisible
        if (externalDamageEffect != null)
        {
            Color c = externalDamageEffect.color;
            c.a = 0f;
            externalDamageEffect.color = c;
        }

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

        // Sonido daño alternado
        if (audioSource != null)
        {
            if (usarPrimerSonido && damageSound1 != null)
                audioSource.PlayOneShot(damageSound1);
            else if (!usarPrimerSonido && damageSound2 != null)
                audioSource.PlayOneShot(damageSound2);

            usarPrimerSonido = !usarPrimerSonido;
        }

        Debug.Log(name + " tomó " + damage + " de daño. Salud restante: " + currentHealth);

        // Flash rojo
        if (spriteRenderer != null)
            StartCoroutine(FlashRed());

        // Efecto externo UI
        if (externalDamageEffect != null)
            StartCoroutine(ShowExternalDamageEffect());

        // Sacudida cámara
        if (cameraShake != null)
            cameraShake.Shake();

        ActualizarTextoVida();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }

    private IEnumerator ShowExternalDamageEffect()
    {
        float elapsed = 0f;
        Color color = externalDamageEffect.color;

        // Aseguramos que empieza transparente
        color.a = 0f;
        externalDamageEffect.color = color;

        // Fade in (0 -> 1)
        while (elapsed < externalEffectDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / externalEffectDuration);

            color.a = Mathf.Lerp(0f, 0.3f, t);
            externalDamageEffect.color = color;

            yield return null;
        }

        // Pausa breve al máximo alpha, opcional
        yield return new WaitForSecondsRealtime(0.1f);

        elapsed = 0f;

        // Fade out (1 -> 0)
        while (elapsed < externalEffectDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / externalEffectDuration);

            color.a = Mathf.Lerp(0.3f, 0f, t);
            externalDamageEffect.color = color;

            yield return null;
        }

        // Al final, aseguramos alpha 0
        color.a = 0f;
        externalDamageEffect.color = color;
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
            vidaTexto.text = "Health: " + currentHealth + " / " + maxHealth;

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

        Time.timeScale = 0f;
        Camera.main.transform.SetParent(null);
        gameObject.SetActive(false);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
