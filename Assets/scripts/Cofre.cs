using UnityEngine;

public class Cofre : MonoBehaviour
{
    public Sprite nuevoSprite;

    private SpriteRenderer spriteRenderer;
    private bool usado = false;

    public delegate void CofreAbiertoEvent(Cofre cofre);
    public static event CofreAbiertoEvent OnCofreAbierto;

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
            usado = true;
            Debug.Log("¡Colisión detectada con el jugador! Cofre se abre.");

            if (nuevoSprite != null)
                spriteRenderer.sprite = nuevoSprite;

            OnCofreAbierto?.Invoke(this);
        }
    }
}
