using UnityEngine;

public class BossMeleeSmash : MonoBehaviour
{
    public int damage = 50;
    public float radius = 1.5f;
    public float duration = 0.3f;

    void Start()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                PlayerHealth player = hit.GetComponent<PlayerHealth>();
                if (player != null)
                {
                    player.TakeDamagePlayer(damage);
                }
            }
        }
        Destroy(gameObject, duration);
    }
}
