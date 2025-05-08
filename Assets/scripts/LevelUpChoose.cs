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

    // UI visibles por mejora
    public GameObject swordUI;
    public GameObject fireballUI;
    public GameObject crucifixUI;
    public GameObject javelinUI;
    public GameObject healthUI;
    public GameObject speedUI;

    // Contadores de mejora (x2, x3, etc.)
    public TextMeshProUGUI swordCountText;
    public TextMeshProUGUI fireballCountText;
    public TextMeshProUGUI crucifixCountText;
    public TextMeshProUGUI javelinCountText;
    public TextMeshProUGUI healthCountText;
    public TextMeshProUGUI speedCountText;

    private int[] upgradeCounts = new int[6] { 1, 0, 0, 0, 0, 0 };
    private List<string> weapons = new List<string> { "Espada", "Fireball", "Crucifijo", "Javalina", "Vida", "Movimiento" };
    private List<string> descriptions = new List<string>
    {
        "El espadón",
        "Dispara donde apuntes con el ratón",
        "Dispara en posiciones aleatorias",
        "Dispara al hacer click",
        "Aumenta la vida máxima",
        "Aumenta la velocidad"
    };

    private List<int> selectedWeaponIndexes = new List<int>();

    void Start()
    {
        //if (panel != null)
        //    panel.SetActive(false);
        panel.SetActive(true);
        panel.SetActive(false);
    }

    void Update()
    {
        if (panel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) HandleButtonFunction(selectedWeaponIndexes[0]);
            if (Input.GetKeyDown(KeyCode.Alpha2)) HandleButtonFunction(selectedWeaponIndexes[1]);
            if (Input.GetKeyDown(KeyCode.Alpha3)) HandleButtonFunction(selectedWeaponIndexes[2]);
        }
    }

    public void ShowPanel()
    {
        if (panel == null) return;

        AssignRandomWeapons();
        AssignRandomFunctionsToButtons();
        panel.SetActive(true);
        Time.timeScale = 0; // Pausa el juego si lo deseas

        if (audioSource != null)
            audioSource.Play(); // <-- reproducir sonido al abrir panel
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
            int rand = Random.Range(0, availableIndexes.Count);
            selectedWeaponIndexes.Add(availableIndexes[rand]);
            availableIndexes.RemoveAt(rand);
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
            case 0: weaponToActivate = weaponUnlock.swordPrefab; swordUI?.SetActive(true); break;
            case 1: weaponToActivate = weaponUnlock.fireballPrefab; fireballUI?.SetActive(true); break;
            case 2: weaponToActivate = weaponUnlock.crucifixPrefab; crucifixUI?.SetActive(true); break;
            case 3: weaponToActivate = weaponUnlock.javelinPrefab; javelinUI?.SetActive(true); break;
            case 4: playerHealth.maxHealth += 100; healthUI?.SetActive(true); break;
            case 5: playerMovement.speed += 1; speedUI?.SetActive(true); break;
        }

        if (weaponToActivate != null)
        {
            weaponUnlock.ActivateWeapon(weaponToActivate);
        }

        upgradeCounts[index]++;
        UpdateUpgradeCountUI(index);

        ShowWeaponSelectedText(weapons[index]);
        ClosePanel();
    }

    void UpdateUpgradeCountUI(int index)
    {
        string count = "x" + upgradeCounts[index];

        switch (index)
        {
            case 0: if (swordCountText != null) swordCountText.text = count; break;
            case 1: if (fireballCountText != null) fireballCountText.text = count; break;
            case 2: if (crucifixCountText != null) crucifixCountText.text = count; break;
            case 3: if (javelinCountText != null) javelinCountText.text = count; break;
            case 4: if (healthCountText != null) healthCountText.text = count; break;
            case 5: if (speedCountText != null) speedCountText.text = count; break;
        }
    }

    void ShowWeaponSelectedText(string weaponName)
    {
        weaponSelectedText.text = $"Seleccionaste: <color=yellow>{weaponName}</color>";
        weaponSelectedText.gameObject.SetActive(true);
        Invoke("HideWeaponSelectedText", 2f);
    }

    void HideWeaponSelectedText()
    {
        weaponSelectedText.gameObject.SetActive(false);
    }
}
