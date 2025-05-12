using UnityEngine;

public class BossLightningBurst : MonoBehaviour
{
    public GameObject lightningBoltPrefab;
    public int numberOfBolts = 5;
    public float timeBetweenBolts = 0.1f;
    public float boltSpeed = 12f;

    private Vector2 directionToPlayer;

    private void Start()
    {
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

        StartCoroutine(FireBurst());
    }


    private System.Collections.IEnumerator FireBurst()
    {
        for (int i = 0; i < numberOfBolts; i++)
        {
            GameObject bolt = Instantiate(lightningBoltPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bolt.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.velocity = transform.right * boltSpeed;

            yield return new WaitForSeconds(timeBetweenBolts);
        }

        Destroy(gameObject, 1f);
    }
}
