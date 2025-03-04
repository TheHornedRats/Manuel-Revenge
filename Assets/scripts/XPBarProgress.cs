using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBarProgress : MonoBehaviour
{
    public Slider slider;
    public int maxExp = 300; // Valor m�ximo de experiencia por nivel

    private void Start()
    {
        slider.maxValue = maxExp;
        slider.value = 0;

        // Asegurar que ScoreManager est� listo antes de suscribirse
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.OnScoreChanged += UpdateXPBar;
        }
        else
        {
            Debug.LogError("ScoreManager no encontrado en la escena. Aseg�rate de que est� presente.");
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
        slider.value = newScore % maxExp; // Reinicia la barra cuando llega al m�ximo
    }
}
