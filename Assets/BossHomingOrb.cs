using UnityEngine;

public class BossHomingOrb : MonoBehaviour
{
    public float speed = 3f;
    public int damage = 30;
    public float lifeTime = 5f;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamagePlayer(damage);
            }

            Destroy(gameObject);
        }
    }
}
