using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class EnemyFollow : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;

    private Rigidbody2D rb;
    private Vector2 movementOffset;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        movementOffset = Random.insideUnitCircle * 0.5f;

        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        Vector2 targetPosition = (Vector2)player.position + movementOffset;
        Vector2 direction = (targetPosition - rb.position).normalized;

        rb.velocity = direction * speed;

        // Flip si se mueve hacia la derecha
        if (direction.x != 0 && spriteRenderer != null)
        {
            spriteRenderer.flipX = direction.x > 0;
        }
    }
}
