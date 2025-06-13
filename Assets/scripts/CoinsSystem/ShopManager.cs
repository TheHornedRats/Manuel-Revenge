using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public int[] itemPrices = { 50, 100, 200 };
    public int[] itemCounts = { 0, 0, 0 };
    public TextMeshProUGUI[] itemCountTexts;
    public Trucos trucos;

    // 🎵 Audio
    public AudioClip[] potionSounds; // Asigna 3 clips desde el Inspector
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void BuyItem(int index)
    {
        if (index < 0 || index >= itemPrices.Length) return;

        if (MoneyManager.Instance.SpendCoins(itemPrices[index]))
        {
            itemCounts[index]++;
            UpdateItemUI(index);
        }
    }

    public bool UseItem(int index)
    {
        if (itemCounts[index] > 0)
        {
            itemCounts[index]--;
            UpdateItemUI(index);

            // Activar efectos
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
            }

            PlayRandomPotionSound(); // 🔊 Sonido al usar ítem

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

    // 🔊 Método para reproducir un sonido aleatorio
    void PlayRandomPotionSound()
    {
        if (potionSounds != null && potionSounds.Length > 0 && audioSource != null)
        {
            int randomIndex = Random.Range(0, potionSounds.Length);
            audioSource.PlayOneShot(potionSounds[randomIndex]);
        }
    }
}
