using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelUpChoose : MonoBehaviour
{
    public GameObject panel;
    public WeaponUnlock weaponUnlock;

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

    private List<string> weapons = new List<string> { "Espada", "Fireball", "Crucifijo", "Javalina", "Arma 5" };
    private List<string> descriptions = new List<string>
    {
        "El espadon",
        "Dispara en función a donde apuntes con el ratón",
        "Dispara en posiciones aleatorias",
        "Disapara al hacer click",
        "Descripción de Arma 5"
    };

    private List<System.Action> buttonFunctions = new List<System.Action>();
    private List<int> selectedWeaponIndexes = new List<int>();

    public AudioSource audioSource; // Referencia al AudioSource

    void Start()
    {
        panel.SetActive(false); // Asegura que el panel esté oculto al iniciar
        buttonFunctions.Add(() => HandleButtonFunction(0));
        buttonFunctions.Add(() => HandleButtonFunction(1));
        buttonFunctions.Add(() => HandleButtonFunction(2));
        buttonFunctions.Add(() => HandleButtonFunction(3));
        buttonFunctions.Add(() => HandleButtonFunction(4));
    }

    void Update()
    {
        // Detecta las teclas numéricas 1, 2, 3
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            HandleButtonFunction(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            HandleButtonFunction(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            HandleButtonFunction(2);
        }
    }

    public void ShowPanel()
    {
        AssignRandomWeapons(); // Asigna los textos e imágenes antes de mostrar el panel
        panel.SetActive(true);

        // Eliminar esta línea si no quieres pausar el juego
        // Time.timeScale = 0; 

        if (audioSource != null)
        {
            audioSource.Play(); // Reproduce el sonido
        }

        // Asigna una función aleatoria a cada botón
        AssignRandomFunctionsToButtons();
    }


    public void ClosePanel()
    {
        panel.SetActive(false);
        Time.timeScale = 1; // Reanuda el juego
    }

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

    void AssignRandomFunctionsToButtons()
    {
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();

        button1.onClick.AddListener(() => HandleButtonFunction(selectedWeaponIndexes[0]));
        button2.onClick.AddListener(() => HandleButtonFunction(selectedWeaponIndexes[1]));
        button3.onClick.AddListener(() => HandleButtonFunction(selectedWeaponIndexes[2]));
    }

    // Funcionalidades para los botones
    public void HandleButtonFunction(int index)
    {
        GameObject weaponToActivate = null;

        switch (index)
        {
            case 0: weaponToActivate = weaponUnlock.swordPrefab; break; // Espada
            case 1: weaponToActivate = weaponUnlock.fireballPrefab; break; // Fireball
            case 2: weaponToActivate = weaponUnlock.crucifixPrefab; break; // Crucifijo
            case 3: weaponToActivate = weaponUnlock.javelinPrefab; break; // Javalina
            case 4: weaponToActivate = weaponUnlock.weapon5Prefab; break; // Arma 5
        }

        if (weaponToActivate != null)
        {
            Debug.Log("Activando arma: " + weaponToActivate.name);
            weaponUnlock.ActivateWeapon(weaponToActivate);
        }
        else
        {
            Debug.LogError("La referencia del arma es nula.");
        }

        // Cerrar el panel después de seleccionar un arma
        ClosePanel();
    }



}
