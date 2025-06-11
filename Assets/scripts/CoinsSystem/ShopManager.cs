using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public int[] itemPrices = { 50, 100, 200 };
    public int[] itemCounts = { 0, 0, 0 };

    public TextMeshProUGUI[] itemCountTexts;
    public Trucos trucos;

    public void BuyItem(int index)
    {
        if (index < 0 || index >= itemPrices.Length) return;

        if (MoneyManager.Instance.SpendCoins(itemPrices[index]))
        {
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
                    break;
                case 1:
                    trucos.AddSpeed();
                    break;
                case 2:
                    trucos.AddXP();
                    break;
                default:
                    Debug.LogWarning("Ítem no definido para activar.");
                    break;
            }

            return true; // uso exitoso
        }
        else
        {
            Debug.Log("No tienes suficientes pociones de este tipo.");
            return false; // uso fallido
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
