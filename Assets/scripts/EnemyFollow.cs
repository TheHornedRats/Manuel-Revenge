using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float speed = 2f; // Velocidad de movimiento del enemigo
    private float originalSpeed;

    private bool isStunned = false;
    private float stunDuration = 0f;

    private void Start()
    {
        originalSpeed = speed; // Guardamos la velocidad original
    }

    private void Update()
    {
        if (isStunned)
        {
            stunDuration -= Time.deltaTime;
            if (stunDuration <= 0)
            {
                isStunned = false;
                speed = originalSpeed;
            }
            return; // No se mueve mientras está aturdido
        }

        // Comprueba que el jugador está asignado
        if (player != null)
        {
            // Calcula la dirección hacia el jugador
            Vector3 direction = (player.position - transform.position).normalized;

            // Mueve al enemigo hacia el jugador sin necesidad de rotarlo
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ralentización
        if (collision.gameObject.layer == LayerMask.NameToLayer("Slowed"))
        {
            speed = originalSpeed * 0.5f; // Reduce la velocidad a la mitad
        }

        // Aturdimiento
        if (collision.gameObject.layer == LayerMask.NameToLayer("Stunned"))
        {
            isStunned = true;
            stunDuration = 2f; // Duración del aturdimiento (2 segundos)
            speed = 0f; // Detiene al enemigo
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Si deja de colisionar con la capa ralentizada, vuelve a la velocidad normal
        if (collision.gameObject.layer == LayerMask.NameToLayer("Slowed"))
        {
            speed = originalSpeed;
        }
    }
}


