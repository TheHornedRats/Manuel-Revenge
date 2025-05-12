using UnityEngine;

public class LightningBolt : MonoBehaviour
{
    public int damage = 15;
    public float lifetime = 1.5f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamagePlayer(damage);
            }

            Destroy(gameObject);
        }
    }
}
