using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBarProgress : MonoBehaviour
{
    public Slider slider;
    public int maxExp = 300; // Valor máximo de experiencia por nivel

    private void Start()
    {
        slider.maxValue = maxExp;
        slider.value = 0;

        // Asegurar que ScoreManager está listo antes de suscribirse
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.OnScoreChanged += UpdateXPBar;
        }
        else
        {
            Debug.LogError("ScoreManager no encontrado en la escena. Asegúrate de que está presente.");
        }
    }

    private void OnDestroy()
    {
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.OnScoreChanged -= UpdateXPBar;
        }
    }

    private void UpdateXPBar(int newScore)
    {
        slider.value = newScore % maxExp; // Reinicia la barra cuando llega al máximo
    }
}
