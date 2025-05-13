using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BossBeam : MonoBehaviour
{
    public float duration = 2f;
    public int damagePerTick = 1;
    public float tickRate = 0.1f;
    public float maxDistance = 20f;
    public LayerMask hitLayers;

    private float tickTimer = 0f;
    private LineRenderer line;
    private Vector2 direction;

    private void Start()
    {
        Destroy(gameObject, duration);

        line = GetComponent<LineRenderer>();
        line.positionCount = 2;

        Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player != null)
        {
            direction = (player.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            direction = transform.right;
        }
    }

    private void Update()
    {
        tickTimer -= Time.deltaTime;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, hitLayers);

        Vector3 endPoint = transform.position + (Vector3)(direction * maxDistance);
        if (hit.collider != null)
        {
            endPoint = hit.point;

            if (tickTimer <= 0f && hit.collider.CompareTag("Player"))
            {
                PlayerHealth ph = hit.collider.GetComponent<PlayerHealth>();
                if (ph != null)
                {
                    ph.TakeDamagePlayer(damagePerTick);
                }
                tickTimer = tickRate;
            }
        }

        line.SetPosition(0, transform.position);
        line.SetPosition(1, endPoint);
    }
}
