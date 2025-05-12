using UnityEngine;

public class BossBigLaser : MonoBehaviour
{
    public int damage = 40;
    public float speed = 20f;
    public float lifetime = 2f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
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
