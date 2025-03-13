using UnityEngine;
using TMPro;
using UnityEngine.UI; // Necesario para usar UI

public class SurvivalTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Referencia al texto UI
    public TextMeshProUGUI finalTimerText; // Referencia al texto UI
    public GameObject player; // Referencia al jugador
    private float survivalTime = 0f;
    private bool isRunning = true;

    void Update()
    {
        if (player == null)
        {
            StopTimer();
            return;
        }

        if (isRunning)
        {
            survivalTime += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(survivalTime / 60);
        int seconds = Mathf.FloorToInt(survivalTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        finalTimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}
