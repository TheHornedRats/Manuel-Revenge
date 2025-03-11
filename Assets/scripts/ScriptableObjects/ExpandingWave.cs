using UnityEngine;

public class ExpandingWave : MonoBehaviour
{
    public float maxSize = 5f;
    public float expansionSpeed = 5f;
    public float movementSpeed = 2f;
    private Vector3 moveDirection = Vector3.zero;
    private bool isInitialized = false;
    private float lifetime = 3f; // Tiempo antes de destruirse

    public void Initialize(Vector3 direction)
    {
        if (!isInitialized)
        {
            moveDirection = direction.normalized;
            transform.localScale = Vector3.zero;
            isInitialized = true;
            Destroy(gameObject, lifetime); // Se autodestruye después del tiempo definido
        }
    }

    private void Update()
    {
        if (!isInitialized) return;

        // Expansión progresiva
        if (transform.localScale.x < maxSize)
        {
            transform.localScale += Vector3.one * expansionSpeed * Time.deltaTime;
        }

        // Movimiento en la dirección establecida
        transform.position += moveDirection * movementSpeed * Time.deltaTime;
    }
}
