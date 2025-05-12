using UnityEngine;

public class BossLightningBurst : MonoBehaviour
{
    public GameObject lightningBoltPrefab;
    public int numberOfBolts = 5;
    public float timeBetweenBolts = 0.1f;
    public float boltSpeed = 12f;

    private void Start()
    {
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
