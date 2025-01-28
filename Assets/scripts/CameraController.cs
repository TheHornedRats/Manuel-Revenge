using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public Transform objetivo; // El objetivo al que seguirá la cámara
    public float velocidadCamara = 0.025f; // Velocidad de movimiento suave de la cámara
    public Vector3 desplazamiento; // Desplazamiento relativo de la cámara al objetivo

    private void LateUpdate()
    {
        // Calcula la posición deseada de la cámara
        Vector3 posicionDeseada = objetivo.position + desplazamiento;

        // Suaviza el movimiento hacia la posición deseada
        Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, velocidadCamara);

        // Actualiza la posición de la cámara
        transform.position = posicionSuavizada;
    }
}
