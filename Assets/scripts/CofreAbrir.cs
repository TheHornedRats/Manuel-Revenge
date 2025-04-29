using UnityEngine;

public class CofreAbrir : MonoBehaviour
{
    public LevelUpChoose levelUpChoose; // Referencia al script LevelUpChoose
    public Sprite nuevoSprite;

    private SpriteRenderer spriteRenderer;

    bool usado = false;

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
        if (other.CompareTag("Player") && !usado) // Asegúrate de que el jugador tiene la etiqueta "Player"
        {
            Debug.Log("¡Colisión detectada con el jugador! Activando LevelUpChoose.");

            if (levelUpChoose != null)
            {
                levelUpChoose.ShowPanel(); // Llama al método para mostrar el panel
                Time.timeScale = 0; // Pausa el juego
                                    // Si el panel es interactivo, solo pausar el fondo o elementos no interactivos.

                if (spriteRenderer != null && nuevoSprite != null)
                {
                    spriteRenderer.sprite = nuevoSprite; // Cambia el sprite por el cofre abierto
                }
                //Destroy(gameObject);
                usado = true;
            }
            else
            {
                Debug.LogError("No se ha asignado el LevelUpChoose en el Inspector.");
            }
        }
    }
}
