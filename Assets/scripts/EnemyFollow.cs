using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class EnemyFollow : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;

    private Rigidbody2D rb;
    private Vector2 movementOffset;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Movimiento individual leve para evitar formaci�n perfecta
        movementOffset = Random.insideUnitCircle * 0.5f;

        // Asegurar configuraci�n de Rigidbody
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        Vector2 targetPosition = (Vector2)player.position + movementOffset;
        Vector2 direction = (targetPosition - rb.position).normalized;

        rb.velocity = direction * speed;
    }
}
