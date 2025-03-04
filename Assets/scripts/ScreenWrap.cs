using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    public float screenWidth = 10f;  // Ancho de la pantalla en unidades del mundo
    public float screenHeight = 10f; // Alto de la pantalla en unidades del mundo
    public float buffer = 0.5f;      // Pequeño margen para evitar errores de detección

    void Update()
    {
        // Encuentra todos los objetos en la capa "Teletransportable"
        GameObject[] objetos = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in objetos)
        {
            if (obj.layer == LayerMask.NameToLayer("Teletransportable")) // Verifica la capa
            {
                Vector3 newPosition = obj.transform.position;

                if (obj.transform.position.x > screenWidth / 2)
                    newPosition.x = -screenWidth / 2;
                else if (obj.transform.position.x < -screenWidth / 2)
                    newPosition.x = screenWidth / 2;

                if (obj.transform.position.y > screenHeight / 2)
                    newPosition.y = -screenHeight / 2;
                else if (obj.transform.position.y < -screenHeight / 2)
                    newPosition.y = screenHeight / 2;

                obj.transform.position = newPosition;
            }
        }
    }
}
