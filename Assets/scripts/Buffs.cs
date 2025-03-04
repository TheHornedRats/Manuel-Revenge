using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Buffs : MonoBehaviour
{
    public GameObject buffMenu;
    public Button[] buffButtons;
    public TextMeshProUGUI[] buffTexts;

    private string[] buffs =
    {
        "Resistente (+Vida)", "Matón (+Ataque)", "Atleta (+Vel. Movimiento)", "Ágil (+Vel. Ataque)",
        "Veterano (+Vida y +Ataque)", "Hábil (+Vel. Movimiento y +Ataque)", "Piernas Firmes (+Vida y +Vel. Movimiento)",
        "Brazos Firmes (+Daño y +Vel. Ataque)", "Experto (Ralentiza enemigos atacados)", "Noqueador (Probabilidad de paralizar enemigos)"
    };

    private PlayerHealth playerHealth;
    private PlayerAttack playerAttack;
    private PlayerMovement playerMovement;
    private ScoreManager scoreManager;
    private EnemyFollow[] enemies;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerAttack = FindObjectOfType<PlayerAttack>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    public void ShowBuffOptions()
    {
        Time.timeScale = 0f;
        buffMenu.SetActive(true);
        enemies = FindObjectsOfType<EnemyFollow>();

        foreach (var enemy in enemies)
        {
            enemy.enabled = false;
        }

        HashSet<int> selectedBuffs = new HashSet<int>();
        for (int i = 0; i < buffButtons.Length; i++)
        {
            int randomBuff;
            do
            {
                randomBuff = Random.Range(0, buffs.Length);
            } while (!selectedBuffs.Add(randomBuff));

            buffTexts[i].text = buffs[randomBuff];
            int buffIndex = randomBuff;
            buffButtons[i].onClick.RemoveAllListeners();
            buffButtons[i].onClick.AddListener(() => ApplyBuff(buffIndex));
        }
    }

    private void ApplyBuff(int index)
    {
        switch (index)
        {
            case 0:
                playerHealth.maxHealth += 20;
                playerHealth.healthSlider.maxValue = playerHealth.maxHealth;
                playerHealth.healthSlider.value = playerHealth.maxHealth;
                break;
            case 1:
                playerAttack.baseFireballDamage += 5;
                break;
            case 2:
                playerMovement.speed += 1f;
                break;
            case 3:
                playerAttack.attackInterval *= 0.9f;
                break;
            case 4:
                playerHealth.maxHealth += 10;
                playerAttack.baseFireballDamage += 3;
                playerHealth.healthSlider.maxValue = playerHealth.maxHealth;
                playerHealth.healthSlider.value = playerHealth.maxHealth;
                break;
            case 5:
                playerMovement.speed += 0.5f;
                playerAttack.attackInterval *= 0.95f;
                break;
            case 6:
                playerHealth.maxHealth += 10;
                playerMovement.speed += 0.5f;
                playerHealth.healthSlider.maxValue = playerHealth.maxHealth;
                playerHealth.healthSlider.value = playerHealth.maxHealth;
                break;
            case 7:
                playerAttack.baseFireballDamage += 3;
                playerAttack.attackInterval *= 0.95f;
                break;
            case 8:
                playerAttack.enemyLayer |= (1 << LayerMask.NameToLayer("Slowed"));
                break;
            case 9:
                playerAttack.enemyLayer |= (1 << LayerMask.NameToLayer("Stunned"));
                break;
        }
        ResumeGame();
    }

    private void ResumeGame()
    {
        buffMenu.SetActive(false);
        Time.timeScale = 1f;

        foreach (var enemy in enemies)
        {
            enemy.enabled = true;
        }
    }
}

