using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Transform cameraTransform;
    public float maxCameraOffset = 2f;
    public float cameraMoveSpeed = 7f;

    public AudioSource sonidoMovimiento;

    public Transform objetoASincronizar; // Prefab o objeto que también debe girar

    private Vector3 targetCameraPosition;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection = new Vector3(horizontal, vertical, 0f).normalized;

        if (inputDirection.magnitude > 0.1f)
        {
            transform.Translate(inputDirection * speed * Time.deltaTime, Space.World);

            if (sonidoMovimiento != null && !sonidoMovimiento.isPlaying)
            {
                sonidoMovimiento.Play();
            }
        }
        else
        {
            if (sonidoMovimiento != null && sonidoMovimiento.isPlaying)
            {
                sonidoMovimiento.Stop();
            }
        }

        // Flip visual del jugador y objeto sincronizado
        if (horizontal > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            if (objetoASincronizar != null)
                objetoASincronizar.localScale = new Vector3(12, 12, 12);
        }
        else if (horizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            if (objetoASincronizar != null)
                objetoASincronizar.localScale = new Vector3(-12, 12, 12);
        }

        // Movimiento de la cámara
        Vector3 offset = inputDirection * maxCameraOffset;
        targetCameraPosition = transform.position + offset;
        targetCameraPosition.z = cameraTransform.position.z;

        cameraTransform.position = Vector3.MoveTowards(
            cameraTransform.position,
            targetCameraPosition,
            cameraMoveSpeed * Time.deltaTime
        );
    }
}
