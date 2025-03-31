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
        "El espadón",
        "Dispara en función a donde apuntes con el ratón",
        "Dispara en posiciones aleatorias",
        "Dispara al hacer click",
        "Descripción de Arma 5"
    };

    private List<System.Action> buttonFunctions = new List<System.Action>();
    private List<int> selectedWeaponIndexes = new List<int>();

    public AudioSource audioSource;

    void Start()
    {
        panel.SetActive(false);
        buttonFunctions.Add(() => HandleButtonFunction(0));
        buttonFunctions.Add(() => HandleButtonFunction(1));
        buttonFunctions.Add(() => HandleButtonFunction(2));
        buttonFunctions.Add(() => HandleButtonFunction(3));
        buttonFunctions.Add(() => HandleButtonFunction(4));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) HandleButtonFunction(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) HandleButtonFunction(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) HandleButtonFunction(2);
    }

    public void ShowPanel()
    {
        AssignRandomWeapons();
        panel.SetActive(true);
        Time.timeScale = 0;

        if (audioSource != null)
        {
            audioSource.Play();
        }

        AssignRandomFunctionsToButtons();
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }

    void AssignRandomWeapons()
    {
        List<int> availableIndexes = new List<int> { 0, 1, 2, 3, 4 };
        selectedWeaponIndexes.Clear();

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, availableIndexes.Count);
            selectedWeaponIndexes.Add(availableIndexes[randomIndex]);
            availableIndexes.RemoveAt(randomIndex);
        }

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

    public void HandleButtonFunction(int index)
    {
        GameObject weaponToActivate = null;

        switch (index)
        {
            case 0: weaponToActivate = weaponUnlock.swordPrefab; break;
            case 1: weaponToActivate = weaponUnlock.fireballPrefab; break;
            case 2: weaponToActivate = weaponUnlock.crucifixPrefab; break;
            case 3: weaponToActivate = weaponUnlock.javelinPrefab; break;
            case 4: weaponToActivate = weaponUnlock.weapon5Prefab; break;
        }

        if (weaponToActivate != null)
        {
            Debug.Log("Activando arma desde panel de subida de nivel: " + weaponToActivate.name);
            weaponUnlock.ActivateWeapon(weaponToActivate);
        }
        else
        {
            Debug.LogError("La referencia del arma es nula.");
        }

        ClosePanel();
    }
}

// Extensión para barajar listas (por si deseas usarla después)
public static class ListExtensions
{
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
