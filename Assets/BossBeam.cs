using UnityEngine;

public class BossBeam : MonoBehaviour
{
    public float duration = 2f;
    public int damagePerTick = 1;
    public float tickRate = 0.1f; // Cada 0.1s

    private float tickTimer;

    void Start()
    {
        Destroy(gameObject, duration);

        Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player != null)
        {
            Vector2 dir = (player.position - transform.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
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
