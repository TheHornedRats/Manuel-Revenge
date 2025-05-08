using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HorizontalHealthUI : MonoBehaviour
{
    public Image healthFill; // Imagen que se vacía
    public PlayerHealth playerHealth; // Vida del jugador

    void Update()
    {
        if (playerHealth != null && healthFill != null)
        {
            float porcentajeVida = (float)playerHealth.GetCurrentHealth() / playerHealth.maxHealth;
            healthFill.fillAmount = porcentajeVida;
        }
    }
}
