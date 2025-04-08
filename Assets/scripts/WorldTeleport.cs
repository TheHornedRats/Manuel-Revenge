using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTeleport : MonoBehaviour
{
    public Camera mainCamera;  // Cámara principal
    public Transform player;   // Jugador
    public Transform map;      // Todo el mapa (tiles, enemigos, objetos)
    public float buffer = 0.5f; // Margen para evitar errores de detección

    void Update()
    {
        if (mainCamera == null) mainCamera = Camera.main; // Encuentra la cámara principal si no está asignada

        // Obtener los límites de la cámara en coordenadas del mundo
        float screenLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        float screenRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        float screenBottom = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        float screenTop = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

        Vector3 mapNewPosition = map.position;

        // Detectar si el jugador ha cruzado los límites
        bool moveMap = false;

        if (player.position.x > screenRight + buffer)
        {
            mapNewPosition.x -= (screenRight - screenLeft);
            moveMap = true;
        }
        else if (player.position.x < screenLeft - buffer)
        {
            mapNewPosition.x += (screenRight - screenLeft);
            moveMap = true;
        }

        if (player.position.y > screenTop + buffer)
        {
            mapNewPosition.y -= (screenTop - screenBottom);
            moveMap = true;
        }
        else if (player.position.y < screenBottom - buffer)
        {
            mapNewPosition.y += (screenTop - screenBottom);
            moveMap = true;
        }

        // Si el mapa debe moverse, lo teletransportamos
        if (moveMap)
        {
            map.position = mapNewPosition;

            // Encuentra todos los objetos en la escena que tienen Rigidbody2D (enemigos, ítems, balas)
            GameObject[] objetos = FindObjectsOfType<GameObject>();

            foreach (GameObject obj in objetos)
            {
                if (obj.GetComponent<Rigidbody2D>() != null) // Solo teletransporta objetos con física
                {
                    Vector3 newPosition = obj.transform.position;

                    // Teletransporte horizontal
                    if (obj.transform.position.x > screenRight + buffer)
                        newPosition.x = screenLeft - buffer;
                    else if (obj.transform.position.x < screenLeft - buffer)
                        newPosition.x = screenRight + buffer;

                    // Teletransporte vertical
                    if (obj.transform.position.y > screenTop + buffer)
                        newPosition.y = screenBottom - buffer;
                    else if (obj.transform.position.y < screenBottom - buffer)
                        newPosition.y = screenTop + buffer;

                    obj.transform.position = newPosition;
                }
            }
        }
    }
}
