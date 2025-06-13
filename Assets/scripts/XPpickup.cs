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

        // Aplicar pitch aleatorio al aparecer (rango entre 0.9 y 1.1 por ejemplo)
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
            GetComponent<Collider2D>().enabled = false; // Evitar múltiples colisiones
            StartCoroutine(MoveToPlayer());
        }
    }

    private IEnumerator MoveToPlayer()
    {
        float closeEnoughDistance = 0.1f;
        float followTimeLimit = 3f; // evitar bucles infinitos
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

        Destroy(gameObject, 0.1f); // esperar que suene el audio
    }

}
