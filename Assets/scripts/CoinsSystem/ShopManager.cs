using UnityEngine;
using TMPro;
using System.Collections;

public class ShopManager : MonoBehaviour
{
    public int[] itemPrices = { 50, 100, 200 };
    public int[] itemCounts = { 0, 0, 0 };
    public TextMeshProUGUI[] itemCountTexts;
    public Trucos trucos;

    // Audio
    public AudioClip[] potionSounds;
    private AudioSource audioSource;

    // Mensajes individuales por ítem si no hay dinero
    public TextMeshProUGUI[] noMoneyTexts; // Asigna 3 desde el Inspector

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Asegurarse que los textos están ocultos al iniciar
        if (noMoneyTexts != null)
        {
            foreach (var text in noMoneyTexts)
            {
                if (text != null)
                    text.gameObject.SetActive(false);
            }
        }
    }

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
            if (noMoneyTexts != null && index < noMoneyTexts.Length && noMoneyTexts[index] != null)
                StartCoroutine(ShowNoMoneyText(index));
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
                case 0: trucos.AddHealth(); break;
                case 1: trucos.AddSpeed(); break;
                case 2: trucos.AddXP(); break;
            }

            PlayRandomPotionSound();
            return true;
        }
        return false;
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

    void PlayRandomPotionSound()
    {
        if (potionSounds != null && potionSounds.Length > 0 && audioSource != null)
        {
            int randomIndex = Random.Range(0, potionSounds.Length);
            audioSource.PlayOneShot(potionSounds[randomIndex]);
        }
    }

    // Mostrar mensaje correspondiente al botón sin dinero durante 2 segundos
    IEnumerator ShowNoMoneyText(int index)
    {
        noMoneyTexts[index].gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        noMoneyTexts[index].gameObject.SetActive(false);
    }
}
