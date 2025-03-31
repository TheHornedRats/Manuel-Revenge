using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapAround : MonoBehaviour
{
    public float leftBound = -10f;  // Límite izquierdo
    public float rightBound = 10f;  // Límite derecho
    public float topBound = 5f;     // Límite superior
    public float bottomBound = -5f; // Límite inferior

    private void Update()
    {
        Vector3 newPosition = transform.position;

        // Si el objeto cruza un borde, reaparece en el lado opuesto
        if (newPosition.x > rightBound) newPosition.x = leftBound;
        else if (newPosition.x < leftBound) newPosition.x = rightBound;

        if (newPosition.y > topBound) newPosition.y = bottomBound;
        else if (newPosition.y < bottomBound) newPosition.y = topBound;

        transform.position = newPosition;
    }
}
