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

    // Referencias a los botones dentro del panel
    public Button button1;
    public Button button2;
    public Button button3;

    public List<Sprite> weaponImages; // Asignar en el Inspector

    private List<string> weapons = new List<string> { "Arma 1", "Arma 2", "Arma 3", "Arma 4", "Arma 5" };
    private List<string> descriptions = new List<string>
    {
        "Descripción de Arma 1",
        "Descripción de Arma 2",
        "Descripción de Arma 3",
        "Descripción de Arma 4",
        "Descripción de Arma 5"
    };

    private List<int> selectedWeaponIndexes = new List<int>(); // Almacena los índices de las armas seleccionadas

    public AudioSource audioSource; // Referencia al AudioSource

    public WeaponActivator weaponActivator; // Referencia al script WeaponActivator

    void Start()
    {
        panel.SetActive(false); // Asegura que el panel esté oculto al iniciar
    }

    public void ShowPanel()
    {
        AssignRandomWeapons(); // Asigna los textos e imágenes antes de mostrar el panel
        panel.SetActive(true);
        Time.timeScale = 0; // Pausa el juego si es necesario

        if (audioSource != null)
        {
            audioSource.Play(); // Reproduce el sonido
        }

        // Asigna las funcionalidades a los botones
        AssignFunctionsToButtons();
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
        Time.timeScale = 1; // Reanuda el juego
    }

    // Asigna armas aleatorias a los botones
    void AssignRandomWeapons()
    {
        List<int> availableIndexes = new List<int> { 0, 1, 2, 3, 4 };
        selectedWeaponIndexes.Clear(); // Limpiar la lista de índices seleccionados

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, availableIndexes.Count);
            selectedWeaponIndexes.Add(availableIndexes[randomIndex]);
            availableIndexes.RemoveAt(randomIndex);
        }

        // Asigna los valores aleatorios a los textos, descripciones e imágenes
        text1.text = weapons[selectedWeaponIndexes[0]];
        text2.text = weapons[selectedWeaponIndexes[1]];
        text3.text = weapons[selectedWeaponIndexes[2]];

        desc1.text = descriptions[selectedWeaponIndexes[0]];
        desc2.text = descriptions[selectedWeaponIndexes[1]];
        desc3.text = descriptions[selectedWeaponIndexes[2]];

        img1.sprite = weaponImages[selectedWeaponIndexes[0]];
        img2.sprite = weaponImages[selectedWeaponIndexes[1]];
        img3.sprite = weaponImages[selectedWeaponIndexes[2]];
    }

    // Asigna las funciones correspondientes a los botones
    void AssignFunctionsToButtons()
    {
        // Asigna a cada botón la función correspondiente a su índice
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();

        button1.onClick.AddListener(() => HandleButtonFunction(0)); // Asocia el índice 0 al primer botón
        button2.onClick.AddListener(() => HandleButtonFunction(1)); // Asocia el índice 1 al segundo botón
        button3.onClick.AddListener(() => HandleButtonFunction(2)); // Asocia el índice 2 al tercer botón
    }

    // Función que se llama cuando un botón es presionado
    void HandleButtonFunction(int index)
    {
        if (index < 0 || index >= selectedWeaponIndexes.Count) return;

        // Llama al método de WeaponActivator para activar el prefab correspondiente
        weaponActivator.ActivateWeapon(selectedWeaponIndexes[index]);
    }
}
