using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    public int currentCoins = 0;
    public TextMeshProUGUI coinText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddCoins(int amount)
    {
        currentCoins += amount;
        Debug.Log("Monedas añadidas. Total: " + currentCoins);
        UpdateUI();
    }

    public bool SpendCoins(int amount)
    {
        if (currentCoins >= amount)
        {
            currentCoins -= amount;
            Debug.Log("Monedas gastadas. Total: " + currentCoins);
            UpdateUI();
            return true;
        }
        Debug.Log("No hay suficientes monedas.");
        return false;
    }


    void UpdateUI()
    {
        if (coinText != null)
            coinText.text = currentCoins.ToString();
    }
}
