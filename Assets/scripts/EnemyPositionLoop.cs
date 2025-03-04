using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPositionLoop : MonoBehaviour
{
    public Transform playerTransform; // Referencia al jugador
    public float respawnDistance = 30f; // Distancia antes de que el enemigo reaparezca en el otro lado

    void Update()
    {
        Vector3 playerPosition = playerTransform.position;
        Vector3 enemyPosition = transform.position;

        Vector3 distanceToPlayer = enemyPosition - playerPosition;

        // Mueve al enemigo en el eje X si está demasiado lejos
        if (distanceToPlayer.x > respawnDistance)
            enemyPosition.x -= respawnDistance * 2;
        else if (distanceToPlayer.x < -respawnDistance)
            enemyPosition.x += respawnDistance * 2;

        // Mueve al enemigo en el eje Y si está demasiado lejos
        if (distanceToPlayer.y > respawnDistance)
            enemyPosition.y -= respawnDistance * 2;
        else if (distanceToPlayer.y < -respawnDistance)
            enemyPosition.y += respawnDistance * 2;

        transform.position = enemyPosition;
    }
}
