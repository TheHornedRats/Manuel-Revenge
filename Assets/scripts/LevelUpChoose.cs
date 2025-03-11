using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelUpChoose : MonoBehaviour
{
    public GameObject panel;

    // Referencias a los textos dentro del panel
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;

    // Referencias a los textos adicionales (descripción)
    public TextMeshProUGUI desc1;
    public TextMeshProUGUI desc2;
    public TextMeshProUGUI desc3;

    // Referencias a las imágenes dentro del panel
    public Image img1;
    public Image img2;
    public Image img3;

    private List<string> weapons = new List<string> { "Arma 1", "Arma 2", "Arma 3", "Arma 4", "Arma 5" };
    private List<string> descriptions = new List<string>
    {
        "Descripción de Arma 1",
        "Descripción de Arma 2",
        "Descripción de Arma 3",
        "Descripción de Arma 4",
        "Descripción de Arma 5"
    };
    public List<Sprite> weaponImages; // Lista de imágenes de armas

    void Start()
    {
        panel.SetActive(false); // Asegura que el panel esté oculto al iniciar
    }

    public void ShowPanel()
    {
        AssignRandomWeapons(); // Asigna los textos e imágenes antes de mostrar el panel
        panel.SetActive(true);
        Time.timeScale = 0; // Pausa el juego si es necesario
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
        Time.timeScale = 1; // Reanuda el juego
    }

    void AssignRandomWeapons()
    {
        List<int> availableIndexes = new List<int> { 0, 1, 2, 3, 4 };
        List<int> selectedIndexes = new List<int>();

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, availableIndexes.Count);
            selectedIndexes.Add(availableIndexes[randomIndex]);
            availableIndexes.RemoveAt(randomIndex);
        }

        // Asigna los valores aleatorios a los textos, descripciones e imágenes
        text1.text = weapons[selectedIndexes[0]];
        text2.text = weapons[selectedIndexes[1]];
        text3.text = weapons[selectedIndexes[2]];

        desc1.text = descriptions[selectedIndexes[0]];
        desc2.text = descriptions[selectedIndexes[1]];
        desc3.text = descriptions[selectedIndexes[2]];

        img1.sprite = weaponImages[selectedIndexes[0]];
        img2.sprite = weaponImages[selectedIndexes[1]];
        img3.sprite = weaponImages[selectedIndexes[2]];
    }
}
