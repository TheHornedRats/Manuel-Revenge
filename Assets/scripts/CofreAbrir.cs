using UnityEngine;

public class CofreAbrir : MonoBehaviour
{
    public LevelUpChoose levelUpChoose; // Referencia al script LevelUpChoose
    public Sprite nuevoSprite;
    public AudioSource sonidoApertura; // Referencia al AudioSource para el sonido de apertura del cofre
    private SpriteRenderer spriteRenderer;
    private bool usado = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("No se encontró un SpriteRenderer en este GameObject.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !usado)
        {
            Debug.Log("¡Colisión detectada con el jugador! Activando LevelUpChoose.");

            if (levelUpChoose != null)
            {
                levelUpChoose.ShowPanel();
                Time.timeScale = 0;

                if (spriteRenderer != null && nuevoSprite != null)
                {
                    spriteRenderer.sprite = nuevoSprite;
                }

                // Reproducir sonido de apertura si está asignado
                if (sonidoApertura != null)
                {
                    sonidoApertura.Play();
                }
                else
                {
                    Debug.LogWarning("No se ha asignado el AudioSource para el sonido de apertura.");
                }

                usado = true;
            }
            else
            {
                Debug.LogError("No se ha asignado el LevelUpChoose en el Inspector.");
            }
        }
    }
}
