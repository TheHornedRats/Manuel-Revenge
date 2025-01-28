using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento del jugador

    private void Update()
    {
        // Captura la entrada del teclado
        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical");  
        // Calcula la dirección de movimiento
        Vector3 direction = new Vector3(horizontal, vertical, 0f).normalized;

        // Si hay movimiento, aplica la traslación
        if (direction.magnitude >= 0.1f)
        {
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
    }
}
