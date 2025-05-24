using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public Transform objetivo; // El objetivo al que seguirá la cámara
    public float velocidadCamara = 0.025f; // Velocidad de movimiento suave de la cámara
    public Vector3 desplazamiento; // Desplazamiento relativo de la cámara al objetivo

    private void LateUpdate()
    {
        Vector3 posicionDeseada = objetivo.position + desplazamiento;

        // Suaviza solo X e Y
        Vector3 posicionSuavizada = Vector3.Lerp(
            new Vector3(transform.position.x, transform.position.y, 0),
            new Vector3(posicionDeseada.x, posicionDeseada.y, 0),
            velocidadCamara
        );

        // Fija el valor de Z manualmente (por ejemplo, -10 si es una cámara 2D)
        posicionSuavizada.z = transform.position.z; // o un valor fijo como -10

        transform.position = posicionSuavizada;
    }

}
