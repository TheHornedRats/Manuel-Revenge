using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelUpChoose : MonoBehaviour
{
    public GameObject panel;
    public WeaponUnlock weaponUnlock;

    // UI del panel
    public TextMeshProUGUI text1, text2, text3;
    public TextMeshProUGUI desc1, desc2, desc3;
    public Image img1, img2, img3;
    public Button button1, button2, button3;

    public List<Sprite> weaponImages; // Asignar en el Inspector
    public TextMeshProUGUI weaponSelectedText; // Texto visual de selección
    public AudioSource audioSource;

    private List<string> weapons = new List<string> { "Espada", "Fireball", "Crucifijo", "Javalina", "Arma 5" };
    private List<string> descriptions = new List<string>
    {
        "El espadón",
        "Dispara en función a donde apuntes con el ratón",
        "Dispara en posiciones aleatorias",
        "Dispara al hacer click",
        "Descripción de Arma 5"
    };

    private List<int> selectedWeaponIndexes = new List<int>();

    void Start()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) HandleButtonFunction(selectedWeaponIndexes[0]);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) HandleButtonFunction(selectedWeaponIndexes[1]);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) HandleButtonFunction(selectedWeaponIndexes[2]);
    }

    public void ShowPanel()
    {
        AssignRandomWeapons();
        panel.SetActive(true);

        // No pausamos el juego por defecto, pero puedes activar esto si lo deseas:
        // Time.timeScale = 0;

        if (audioSource != null) audioSource.Play();
        AssignRandomFunctionsToButtons();
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }

    void AssignRandomWeapons()
    {
        List<int> availableIndexes = new List<int>();
        for (int i = 0; i < weapons.Count; i++)
            availableIndexes.Add(i);

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
                // Si añades más armas, amplía aquí
        }

        if (weaponToActivate != null)
        {
            Debug.Log("Activando arma desde panel de subida de nivel: " + weaponToActivate.name);
            weaponUnlock.ActivateWeapon(weaponToActivate);
            ShowWeaponSelectedText(weapons[index]);
        }
        else
        {
            Debug.LogError("La referencia del arma es nula.");
        }

        ClosePanel();
    }

    private void ShowWeaponSelectedText(string weaponName)
    {
        if (weaponSelectedText == null) return;

        weaponSelectedText.text = "Seleccionaste: <color=yellow>" + weaponName + "</color>";
        weaponSelectedText.gameObject.SetActive(true);
        Invoke("HideWeaponSelectedText", 2f);
    }

    private void HideWeaponSelectedText()
    {
        if (weaponSelectedText != null)
            weaponSelectedText.gameObject.SetActive(false);
    }
}
