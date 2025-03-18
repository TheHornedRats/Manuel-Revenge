using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBarProgress : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        if (ScoreManager.instance != null)
        {
            slider.maxValue = ScoreManager.instance.pointsPerLevel;
            slider.value = ScoreManager.instance.score;
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
        slider.maxValue = ScoreManager.instance.pointsPerLevel;
        slider.value = newScore;
    }
}
