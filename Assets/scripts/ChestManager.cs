using UnityEngine;

public class ChestManager : MonoBehaviour
{
    public LevelUpChoose levelUpChoose;
    public AudioSource sonidoApertura;

    void OnEnable()
    {
        Cofre.OnCofreAbierto += CofreFueAbierto;
    }

    void OnDisable()
    {
        Cofre.OnCofreAbierto -= CofreFueAbierto;
    }

    private void CofreFueAbierto(Cofre cofre)
    {
        if (levelUpChoose != null)
        {
            levelUpChoose.ShowPanel();
            Time.timeScale = 0;
        }
        else
        {
            Debug.LogError("No se ha asignado el LevelUpChoose en el Inspector.");
        }

        if (sonidoApertura != null)
        {
            sonidoApertura.Play();
        }
        else
        {
            Debug.LogWarning("No se ha asignado un AudioSource para el sonido de apertura.");
        }
    }
}
