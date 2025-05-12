using UnityEngine;

public class BossBigLaser : MonoBehaviour
{
    public int damage = 40;
    public float speed = 20f;
    public float lifetime = 2f;

    private Vector2 directionToPlayer;

    private void Start()
    {
        Destroy(gameObject, lifetime);

        Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player != null)
        {
            directionToPlayer = (player.position - transform.position).normalized;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            directionToPlayer = Vector2.right;
        }
    }

    private void Update()
    {
        transform.position += (Vector3)directionToPlayer * speed * Time.deltaTime;
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
