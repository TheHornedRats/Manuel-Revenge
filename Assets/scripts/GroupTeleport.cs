using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupTeleport : MonoBehaviour
{
    public float leftBound = -10f;
    public float rightBound = 10f;
    public float topBound = 5f;
    public float bottomBound = -5f;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector3 playerPos = transform.position;
        Vector3 cameraPos = mainCamera.transform.position;

        // Detecta si el jugador cruzó un borde
        if (playerPos.x > rightBound || playerPos.x < leftBound ||
            playerPos.y > topBound || playerPos.y < bottomBound)
        {
            TeleportGroup();
        }
    }

    private void TeleportGroup()
    {
        // Calcula el desplazamiento del teletransporte
        float offsetX = (transform.position.x > rightBound) ? leftBound - rightBound :
                        (transform.position.x < leftBound) ? rightBound - leftBound : 0;

        float offsetY = (transform.position.y > topBound) ? bottomBound - topBound :
                        (transform.position.y < bottomBound) ? topBound - bottomBound : 0;

        Vector3 offset = new Vector3(offsetX, offsetY, 0);

        // Encuentra todos los objetos dentro de la cámara y los teletransporta
        Collider2D[] objectsInView = Physics2D.OverlapBoxAll(mainCamera.transform.position, new Vector2(20, 15), 0);
        foreach (Collider2D obj in objectsInView)
        {
            obj.transform.position += offset;
        }

        // También mueve la cámara
        mainCamera.transform.position += offset;
    }
}
