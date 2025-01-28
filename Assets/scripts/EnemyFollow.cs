using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float speed = 2f; // Velocidad de movimiento del enemigo

    private void Update()
    {
        // Comprueba que el jugador está asignado
        if (player != null)
        {
            // Calcula la dirección hacia el jugador
            Vector3 direction = (player.position - transform.position).normalized;

            // Mueve al enemigo hacia el jugador sin necesidad de rotarlo
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}