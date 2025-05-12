using UnityEngine;

public class BossBeam : MonoBehaviour
{
    public float duration = 2f;
    public int damagePerTick = 1;
    public float tickRate = 0.1f; // Cada 0.1s

    private float tickTimer;

    private void Start()
    {
        Destroy(gameObject, duration);
    }

    private void Update()
    {
        tickTimer -= Time.deltaTime;
        if (tickTimer <= 0f)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 20f, LayerMask.GetMask("Player"));
            if (hit.collider != null)
            {
                PlayerHealth ph = hit.collider.GetComponent<PlayerHealth>();
                if (ph != null)
                {
                    ph.TakeDamagePlayer(damagePerTick);
                }
            }

            tickTimer = tickRate;
        }
    }
}
