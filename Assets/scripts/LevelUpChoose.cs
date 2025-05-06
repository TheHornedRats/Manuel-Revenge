using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelUpChoose : MonoBehaviour
{
    public GameObject panel;
    public WeaponUnlock weaponUnlock;
    public PlayerHealth playerHealth;
    public PlayerMovement playerMovement;

    public TextMeshProUGUI text1, text2, text3;
    public TextMeshProUGUI desc1, desc2, desc3;
    public Image img1, img2, img3;
    public Button button1, button2, button3;
    public List<Sprite> weaponImages;
    public TextMeshProUGUI weaponSelectedText;
    public AudioSource audioSource;

    // Elementos de UI que se activarán al elegir cierta mejora
    public GameObject swordUI;
    public GameObject fireballUI;
    public GameObject crucifixUI;
    public GameObject javelinUI;
    public GameObject healthUI;
    public GameObject speedUI;

    private List<string> weapons = new List<string> { "Espada", "Fireball", "Crucifijo", "Javalina", "Vida", "Movimiento" };
    private List<string> descriptions = new List<string>
    {
        "El espadon",
        "Dispara en función a donde apuntes con el ratón",
        "Dispara en posiciones aleatorias",
        "Dispara al hacer click",
        "Más vida",
        "Más velocidad de movimiento"
    };

    private List<int> selectedWeaponIndexes = new List<int>();

    void Start()
    {
        //panel.SetActive(false);
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
        AssignRandomFunctionsToButtons();
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }

    void AssignRandomWeapons()
    {
        List<int> availableIndexes = new List<int> { 0, 1, 2, 3, 4, 5 };
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
            case 0: // Espada
                weaponToActivate = weaponUnlock.swordPrefab;
                if (swordUI != null) swordUI.SetActive(true);
                break;

            case 1: // Fireball
                weaponToActivate = weaponUnlock.fireballPrefab;
                if (fireballUI != null) fireballUI.SetActive(true);
                break;

            case 2: // Crucifijo
                weaponToActivate = weaponUnlock.crucifixPrefab;
                if (crucifixUI != null) crucifixUI.SetActive(true);
                break;

            case 3: // Javalina
                weaponToActivate = weaponUnlock.javelinPrefab;
                if (javelinUI != null) javelinUI.SetActive(true);
                break;

            case 4: // Vida
                playerHealth.maxHealth += 100;
                if (healthUI != null) healthUI.SetActive(true);
                break;

            case 5: // Movimiento
                playerMovement.speed += 1;
                if (speedUI != null) speedUI.SetActive(true);
                break;
        }

        if (weaponToActivate != null)
        {
            Debug.Log("Activando arma: " + weaponToActivate.name);
            weaponUnlock.ActivateWeapon(weaponToActivate);
        }

        ShowWeaponSelectedText(weapons[index]);
        ClosePanel();
    }

    private void ShowWeaponSelectedText(string weaponName)
    {
        weaponSelectedText.text = "Seleccionaste: <color=yellow>" + weaponName + "</color>";
        weaponSelectedText.gameObject.SetActive(true);
        Invoke("HideWeaponSelectedText", 2f);
    }

    private void HideWeaponSelectedText()
    {
        weaponSelectedText.gameObject.SetActive(false);
    }
}
