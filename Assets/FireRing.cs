using UnityEngine;

public class FireRing : MonoBehaviour
{
    public int damage = 15;
    public float duration = 3f;

    private void Start()
    {
        Destroy(gameObject, duration);
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
