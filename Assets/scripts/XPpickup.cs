using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPpickup : MonoBehaviour
{
    public int XPobtenida = 5;
    public AudioClip pickupSound; // Sonido que se reproducirá al recoger
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Manuel")
        {
            // Reproducir sonido si está asignado
            if (pickupSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(pickupSound);
            }
            else
            {
                Debug.LogWarning("Falta AudioSource o pickupSound en XPpickup.");
            }

            ScoreManager.instance.AddScore(XPobtenida);

            // Destruir después de un pequeño retraso para permitir que el sonido suene
            Destroy(gameObject, 0.1f);
        }
    }
}

