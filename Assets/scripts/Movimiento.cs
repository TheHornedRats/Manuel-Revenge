using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Transform cameraTransform;
    public float maxCameraOffset = 2f;
    public float cameraMoveSpeed = 7f;

    public AudioSource sonidoMovimiento; //Referencia al AudioSource para el sonido de movimiento

    private Vector3 targetCameraPosition;

    void Update()
    {
        // Entrada del jugador
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection = new Vector3(horizontal, vertical, 0f).normalized;

        // Movimiento del jugador
        if (inputDirection.magnitude > 0.1f)
        {
            transform.Translate(inputDirection * speed * Time.deltaTime, Space.World);

            // Reproducir sonido si no se está reproduciendo ya
            if (sonidoMovimiento != null && !sonidoMovimiento.isPlaying)
            {
                sonidoMovimiento.Play();
            }
        }
        else
        {
            // Detener sonido si el jugador no se está moviendo
            if (sonidoMovimiento != null && sonidoMovimiento.isPlaying)
            {
                sonidoMovimiento.Stop();
            }
        }

        // Flip visual
        if (horizontal > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (horizontal < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // Calcular posición objetivo de la cámara con offset limitado
        Vector3 offset = inputDirection * maxCameraOffset;
        targetCameraPosition = transform.position + offset;
        targetCameraPosition.z = cameraTransform.position.z;

        // Mover la cámara hacia la posición objetivo
        cameraTransform.position = Vector3.MoveTowards(
            cameraTransform.position,
            targetCameraPosition,
            cameraMoveSpeed * Time.deltaTime
        );
    }
}
