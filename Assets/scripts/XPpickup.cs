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
        while (Vector2.Distance(transform.position, targetPlayer.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPlayer.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Reproducir sonido si está asignado
        if (pickupSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }
        else
        {
            Debug.LogWarning("Falta AudioSource o pickupSound en XPpickup.");
        }

        // Añadir experiencia
        ScoreManager.instance.AddScore(XPobtenida);

        // Esperar un poco para que suene el audio (si lo necesitas)
        Destroy(gameObject, 0.1f);
    }
}
