using UnityEngine;

public class FireTornado : MonoBehaviour
{
    public float speed = 2f;
    public float duration = 5f;
    public int damage = 15;

    private Vector2 direction;

    void Start()
    {
        direction = Random.insideUnitCircle.normalized;
        Destroy(gameObject, duration);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
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
        }
    }
}
