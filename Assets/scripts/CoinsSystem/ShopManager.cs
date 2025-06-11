using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public int[] itemPrices = { 50, 100, 200 };
    public int[] itemCounts = { 0, 0, 0 };

    public TextMeshProUGUI[] itemCountTexts;
    public Trucos trucos;

    void Start()
    {
        for (int i = 0; i < itemCounts.Length; i++)
        {
            UpdateItemUI(i);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) BuyItem(0);
        if (Input.GetKeyDown(KeyCode.K)) BuyItem(1);
        if (Input.GetKeyDown(KeyCode.M)) BuyItem(2);
    }

    public void BuyItem(int index)
    {
        Debug.Log($"Intentando comprar ítem {index}");

        if (index < 0 || index >= itemPrices.Length) return;

        if (MoneyManager.Instance.SpendCoins(itemPrices[index]))
        {
            Debug.Log("Compra realizada.");
            itemCounts[index]++;
            UpdateItemUI(index);
        }
        else
        {
            Debug.Log("No tienes suficientes monedas.");
        }
    }

    public bool UseItem(int index)
    {
        if (itemCounts[index] > 0)
        {
            itemCounts[index]--;
            UpdateItemUI(index);

            switch (index)
            {
                case 0:
                    trucos.AddHealth();
                    Debug.Log("a");
                    break;
                case 1:
                    trucos.AddSpeed();
                    Debug.Log("b");
                    break;
                case 2:
                    trucos.AddXP();
                    Debug.Log("c");
                    break;
                default:
                    Debug.LogWarning("Ítem no definido para activar.");
                    break;
            }

            return true;
        }
        else
        {
            Debug.Log("No tienes suficientes pociones de este tipo.");
            return false;
        }
    }

    void UpdateItemUI(int index)
    {
        if (itemCountTexts != null && index < itemCountTexts.Length)
        {
            itemCountTexts[index].text = "x" + itemCounts[index];
        }
    }

    public int GetItemCount(int index)
    {
        if (index >= 0 && index < itemCounts.Length)
            return itemCounts[index];
        return 0;
    }
}
