using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public Transform objetivo; // El objetivo al que seguir� la c�mara
    public float velocidadCamara = 0.025f; // Velocidad de movimiento suave de la c�mara
    public Vector3 desplazamiento; // Desplazamiento relativo de la c�mara al objetivo

    private void LateUpdate()
    {
        // Calcula la posici�n deseada de la c�mara
        Vector3 posicionDeseada = objetivo.position + desplazamiento;

        // Suaviza el movimiento hacia la posici�n deseada
        Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, velocidadCamara);

        // Actualiza la posici�n de la c�mara
        transform.position = posicionSuavizada;
    }
}
