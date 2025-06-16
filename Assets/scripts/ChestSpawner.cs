using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    public GameObject cofrePrefab;
    public Transform jugador;
    public float maxDistancia = 25f;
    public float maxDistanciaDespawn = 30f;
    public int maxCofres = 5;
    public float tiempoEntreSpawns = 2f;

    private List<GameObject> cofresActivos = new List<GameObject>();
    private float temporizador;

    void Update()
    {
        temporizador += Time.deltaTime;

        if (cofresActivos.Count < maxCofres && temporizador >= tiempoEntreSpawns)
        {
            Vector2 posicion = GenerarPosicionAleatoria();
            if (posicion != Vector2.zero)
            {
                GameObject nuevoCofre = Instantiate(cofrePrefab, posicion, Quaternion.identity);
                cofresActivos.Add(nuevoCofre);
            }
            temporizador = 0f;
        }

        for (int i = cofresActivos.Count - 1; i >= 0; i--)
        {
            if (Vector2.Distance(cofresActivos[i].transform.position, jugador.position) > maxDistanciaDespawn)
            {
                Destroy(cofresActivos[i]);
                cofresActivos.RemoveAt(i);
            }
        }
    }

    Vector2 GenerarPosicionAleatoria()
    {
        for (int i = 0; i < 30; i++) // Máx 30 intentos
        {
            Vector2 direccion = Random.insideUnitCircle.normalized;
            float distancia = Random.Range(5f, maxDistancia);
            Vector2 posicion = (Vector2)jugador.position + direccion * distancia;

            if (!EstaDentroDeVista(posicion))
            {
                return posicion;
            }
        }

        Debug.LogWarning("No se pudo generar una posición válida fuera de la cámara.");
        return Vector2.zero;
    }

    bool EstaDentroDeVista(Vector2 posicion)
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(posicion);
        return viewportPos.x >= 0f && viewportPos.x <= 1f &&
               viewportPos.y >= 0f && viewportPos.y <= 1f &&
               viewportPos.z >= 0f; // Está delante de la cámara
    }
}
