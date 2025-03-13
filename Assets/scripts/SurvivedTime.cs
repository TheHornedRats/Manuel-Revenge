using UnityEngine;
using TMPro; // Importar TextMeshPro

public class SurvivedTime : MonoBehaviour
{
    public TMP_Text tiempoText; // Para TextMeshPro
    public TMP_Text finalTiempoText; // Para TextMeshPro

    void Update()
    {
        float tiempoJugado = Time.timeSinceLevelLoad;
        int minutos = Mathf.FloorToInt(tiempoJugado / 60);
        int segundos = Mathf.FloorToInt(tiempoJugado % 60);
        tiempoText.text = $"Tiempo Jugado: {minutos:D2}:{segundos:D2}";
        finalTiempoText.text = $"TIEMPO SOBREVIVIDO: {minutos:D2}:{segundos:D2}";
    }
}
