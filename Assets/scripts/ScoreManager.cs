using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private int score = 0;
    private int level = 1;
    private int scoreToLevelUp = 100;
    public WeaponHandler weaponHandler;
    public TextMeshProUGUI experienceText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateExperienceText();
        Debug.Log("Puntos: " + score + " / " + scoreToLevelUp);

        if (score >= scoreToLevelUp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        score = 0;
        scoreToLevelUp += 50;
        UpdateExperienceText();

        Debug.Log("¡Subiste al nivel " + level + "!");

        if (weaponHandler != null && weaponHandler.weapons.Count > 0)
        {
            int weaponIndex = Random.Range(0, weaponHandler.weapons.Count);
            weaponHandler.weapons[weaponIndex].InitWeapon(level);
            Debug.Log("Se mejoró el arma: " + weaponHandler.weapons[weaponIndex].weaponName);
        }
    }

    private void UpdateExperienceText()
    {
        if (experienceText != null)
        {
            experienceText.text = "EXP: " + score + " / " + scoreToLevelUp;
        }
    }
}
