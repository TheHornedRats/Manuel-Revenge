using UnityEngine;

public class SonidoBotones : MonoBehaviour
{
    public AudioClip clickSound;  // El sonido que se reproducir�
    private AudioSource audioSource;    // El componente AudioSource

    void Start()
    {
        // Obtener el componente AudioSource del mismo GameObject
        audioSource = GetComponent<AudioSource>();

        // Comprobar si el sonido o AudioSource est�n asignados
        if (clickSound == null || audioSource == null)
        {
            Debug.LogError("�Falta asignar clickSound o AudioSource!");
        }
    }

    void Update()
    {
        // Detectar clic del rat�n (bot�n izquierdo del rat�n)
        if (Input.GetMouseButtonDown(0))  // 0 es el bot�n izquierdo
        {
            PlayClickSound();
        }
    }

    // M�todo para reproducir el sonido
    private void PlayClickSound()
    {
        if (clickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSound);  // Reproducir el sonido
        }
    }
}
