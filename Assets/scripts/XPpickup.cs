using System.Collections;
using UnityEngine;

public class XPpickup : MonoBehaviour
{
    public int XPobtenida = 5;
    public AudioClip pickupSound;
    private AudioSource audioSource;
    private bool isMovingToPlayer = false;
    private Transform targetPlayer;

    public float moveSpeed = 5f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            audioSource.pitch = Random.Range(0.95f, 1.05f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isMovingToPlayer && other.gameObject.name == "Manuel")
        {
            isMovingToPlayer = true;
            targetPlayer = other.transform;
            GetComponent<Collider2D>().enabled = false;
            StartCoroutine(MoveToPlayer());
        }
    }

    private IEnumerator MoveToPlayer()
    {
        // Colocarse delante visualmente
        foreach (var sr in GetComponentsInChildren<SpriteRenderer>())
        {
            sr.sortingOrder = 100;
        }

        float closeEnoughDistance = 0.1f;
        float followTimeLimit = 3f;
        float timer = 0f;

        while (timer < followTimeLimit)
        {
            if (targetPlayer == null) yield break;

            transform.position = Vector2.MoveTowards(transform.position, targetPlayer.position, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetPlayer.position) < closeEnoughDistance)
                break;

            timer += Time.deltaTime;
            yield return null;
        }

        if (pickupSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }
        else
        {
            Debug.LogWarning("Falta AudioSource o pickupSound en XPpickup.");
        }

        ScoreManager.instance.AddScore(XPobtenida);
        Destroy(gameObject, 0.1f);
    }
}
