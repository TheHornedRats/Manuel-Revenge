using UnityEngine;

public class LevelUpTrigger : MonoBehaviour
{
    public LevelUpChoose levelUpChoose; // Referencia al script LevelUpChoose

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el jugador tiene la etiqueta "Player"
        {
            Debug.Log("¡Colisión detectada con el jugador! Activando LevelUpChoose.");

            if (levelUpChoose != null)
            {
                levelUpChoose.ShowPanel(); // Llama al método para mostrar el panel
                Time.timeScale = 0; // Pausa el juego
                                    // Si el panel es interactivo, solo pausar el fondo o elementos no interactivos.
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("No se ha asignado el LevelUpChoose en el Inspector.");
            }
        }
    }
}
