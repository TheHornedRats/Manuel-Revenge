using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Transform cameraTransform;
    public float maxCameraOffset = 2f;
    public float cameraMoveSpeed = 7f;

    public AudioSource sonidoMovimiento;
    public Transform objetoASincronizar;

    private Vector3 targetCameraPosition;

    // Nuevas variables para movimiento por botones UI
    private Vector2 uiInputDirection = Vector2.zero;

    void Update()
    {
        // Entrada por teclado
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Combinar input de teclado y UI
        Vector3 inputDirection = new Vector3(horizontal + uiInputDirection.x, vertical + uiInputDirection.y, 0f).normalized;

        // Movimiento
        if (inputDirection.magnitude > 0.1f)
        {
            transform.Translate(inputDirection * speed * Time.deltaTime, Space.World);

            if (sonidoMovimiento != null && !sonidoMovimiento.isPlaying)
                sonidoMovimiento.Play();
        }
        else
        {
            if (sonidoMovimiento != null && sonidoMovimiento.isPlaying)
                sonidoMovimiento.Stop();
        }

        // Flip visual
        if (inputDirection.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            if (objetoASincronizar != null)
                objetoASincronizar.localScale = new Vector3(12, 12, 12);
        }
        else if (inputDirection.x < 0)
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

    // Métodos para los botones UI
    public void SetUIInputX(float value)
    {
        uiInputDirection.x = value;
    }

    public void SetUIInputY(float value)
    {
        uiInputDirection.y = value;
    }

    public void ResetUIInputX()
    {
        uiInputDirection.x = 0;
    }

    public void ResetUIInputY()
    {
        uiInputDirection.y = 0;
    }
}
