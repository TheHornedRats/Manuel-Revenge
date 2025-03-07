//using UnityEngine;

//public class PlayerMovement : MonoBehaviour
//{
//    public float speed = 5f; // Velocidad de movimiento del jugador

//    private void Update()
//    {
//        // Captura la entrada del teclado
//        float horizontal = Input.GetAxis("Horizontal");

//        if (moveInput > 0)
//        {
//            transform.localScale = new Vector3(1, 1, 1); // Mirar a la derecha (escala normal)
//        }
//        else if (moveInput < 0)
//        {
//            transform.localScale = new Vector3(-1, 1, 1); // Mirar a la izquierda (invertir en X)
//        }

//        float vertical = Input.GetAxis("Vertical");  
//        // Calcula la dirección de movimiento
//        Vector3 direction = new Vector3(horizontal, vertical, 0f).normalized;

//        // Si hay movimiento, aplica la traslación
//        if (direction.magnitude >= 0.1f)
//        {
//            transform.Translate(direction * speed * Time.deltaTime, Space.World);
//        }
//    }
//}

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento del jugador

    private void Update()
    {
        // Captura la entrada del teclado
        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal > 0) // Cambié "moveInput" por "horizontal"
        {
            transform.localScale = new Vector3(1, 1, 1); // Mirar a la derecha (escala normal)
        }
        else if (horizontal < 0) // Cambié "moveInput" por "horizontal"
        {
            transform.localScale = new Vector3(-1, 1, 1); // Mirar a la izquierda (invertir en X)
        }

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
