using UnityEngine;
using TMPro;
using UnityEngine.UI;
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

    // Mensajes si no hay dinero
    public TextMeshProUGUI[] noMoneyTexts;

    // Mensajes si no tienes pociones
    public TextMeshProUGUI[] noPotionTexts;

    // Panels que cambian de color según dinero
    public Image[] panelImages;

    // Colores para indicar si puedes o no comprar
    public Color canBuyColor = Color.green;
    public Color cannotBuyColor = Color.red;

    // 🔹 NUEVO: Botones de uso para cada ítem
    public Button[] useItemButtons;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (noMoneyTexts != null)
            foreach (var text in noMoneyTexts)
                if (text != null) text.gameObject.SetActive(false);

        if (noPotionTexts != null)
            foreach (var text in noPotionTexts)
                if (text != null) text.gameObject.SetActive(false);

        // 🔹 Asignar listeners a los botones de uso
        if (useItemButtons != null)
        {
            for (int i = 0; i < useItemButtons.Length; i++)
            {
                int index = i; // Captura del índice local
                if (useItemButtons[index] != null)
                {
                    useItemButtons[index].onClick.RemoveAllListeners();
                    useItemButtons[index].onClick.AddListener(() => UseItem(index));
                }
            }
        }

        UpdateAllPanelColors();
    }

    void Update()
    {
        UpdateAllPanelColors();
    }

    public void BuyItem(int index)
    {
        if (index < 0 || index >= itemPrices.Length) return;

        if (MoneyManager.Instance.SpendCoins(itemPrices[index]))
        {
            itemCounts[index]++;
            UpdateItemUI(index);
            UpdatePanelColor(index);
        }
        else
        {
            if (noMoneyTexts != null && index < noMoneyTexts.Length && noMoneyTexts[index] != null)
                StartCoroutine(ShowNoMoneyText(index));
        }
    }

    public bool UseItem(int index)
    {
        if (index < 0 || index >= itemCounts.Length) return false;

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
        else
        {
            if (noPotionTexts != null && index < noPotionTexts.Length && noPotionTexts[index] != null)
                StartCoroutine(ShowNoPotionText(index));
        }
        return false;
    }

    void UpdateItemUI(int index)
    {
        if (itemCountTexts != null && index < itemCountTexts.Length)
            itemCountTexts[index].text = "x" + itemCounts[index];
    }

    void PlayRandomPotionSound()
    {
        if (potionSounds != null && potionSounds.Length > 0 && audioSource != null)
        {
            int randomIndex = Random.Range(0, potionSounds.Length);
            audioSource.PlayOneShot(potionSounds[randomIndex]);
        }
    }

    IEnumerator ShowNoMoneyText(int index)
    {
        noMoneyTexts[index].gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        noMoneyTexts[index].gameObject.SetActive(false);
    }

    IEnumerator ShowNoPotionText(int index)
    {
        noPotionTexts[index].gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        noPotionTexts[index].gameObject.SetActive(false);
    }

    public int GetItemCount(int index)
    {
        if (index >= 0 && index < itemCounts.Length)
            return itemCounts[index];
        return 0;
    }

    void UpdatePanelColor(int index)
    {
        if (panelImages != null && index < panelImages.Length && panelImages[index] != null)
        {
            if (MoneyManager.Instance.HasEnoughCoins(itemPrices[index]))
                panelImages[index].color = canBuyColor;
            else
                panelImages[index].color = cannotBuyColor;
        }
    }

    public void UpdateAllPanelColors()
    {
        for (int i = 0; i < itemPrices.Length; i++)
            UpdatePanelColor(i);
    }
}
