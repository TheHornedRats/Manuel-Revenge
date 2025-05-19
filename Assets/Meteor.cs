using UnityEngine;

public class MeteorShot : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 20;
    public float lifetime = 5f;

    private Vector2 direction;

    public void Initialize(Vector2 dir)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90); // base mirando hacia delante
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D other)
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
