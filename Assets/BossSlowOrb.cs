using UnityEngine;

public class BossSlowProjectile : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 10;
    public float slowDuration = 2f;
    public float slowAmount = 0.3f;

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth player = collision.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamagePlayer(damage);
                // Aquí se puede aplicar el efecto de ralentización si el jugador tiene un script para ello
            }
            Destroy(gameObject);
        }
    }
}
