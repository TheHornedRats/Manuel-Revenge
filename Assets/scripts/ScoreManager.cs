using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int currentXP = 0;
    public int xpToLevelUp = 100;
    public int playerLevel = 1;
    public TextMeshProUGUI xpText;


    private void Start()
    {
        UpdateXPText();
    }

    public void AddXP(int amount)
    {
        currentXP += amount;
        Debug.Log("XP obtenida: " + amount + ". XP total: " + currentXP + "/" + xpToLevelUp);

        UpdateXPText();

        if (currentXP >= xpToLevelUp)
        {
            LevelUp();
        }
    }

    private void UpdateXPText()
    {
        if (xpText != null)
        {
            xpText.text = "XP: " + currentXP + "/" + xpToLevelUp;
        }
        else
        {
            Debug.LogError("xpText no asignado en ScoreManager.");
        }
    }

    private void LevelUp()
    {
        playerLevel++;
        currentXP -= xpToLevelUp;
        xpToLevelUp = Mathf.RoundToInt(xpToLevelUp * 1.2f);

        Debug.Log("Subiste al nivel " + playerLevel);
        UpdateXPText();
    }
}
