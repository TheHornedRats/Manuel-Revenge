using System.Collections.Generic;
using UnityEngine;

public class Buffs : MonoBehaviour
{
    [Header("Lista de Buffs")]
    public string[] buffs =
    {
        "Resistente: M�s vida",
        "Mat�n: M�s ataque",
        "Atleta: M�s velocidad de movimiento",
        "�gil: M�s velocidad de ataque",
        "Veterano: Un poco m�s de vida y ataque",
        "H�bil: Un poco m�s de velocidad de movimiento y ataque",
        "Piernas Firmes: Un poco m�s de vida y velocidad de movimiento",
        "Brazos Firmes: Un poco m�s de da�o y velocidad de ataque",
        "Experto: Ralentiza a los enemigos a los que ataca",
        "Noqueador: Probabilidad de paralizar a los enemigos"
    };

    private PlayerHealth playerHealth;
    private PlayerAttack playerAttack;
    private PlayerMovement playerMovement;
    private EnemyFollow[] enemies;
    public Buffs buffsManager;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerAttack = FindObjectOfType<PlayerAttack>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    public void ShowBuffOptions()
    {
        Time.timeScale = 0f;
        enemies = FindObjectsOfType<EnemyFollow>();

        foreach (var enemy in enemies)
        {
            enemy.enabled = false;
        }

        List<int> selectedBuffs = GetRandomBuffs(3);
        foreach (int buffIndex in selectedBuffs)
        {
            Debug.Log($"Buff disponible: {buffs[buffIndex]}");
        }
    }

    public void ApplyBuff(int index)
    {
        Debug.Log($"Has seleccionado: {buffs[index]}");
        ResumeGame();
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        foreach (var enemy in enemies)
        {
            enemy.enabled = true;
        }
    }

    private List<int> GetRandomBuffs(int count)
    {
        HashSet<int> selectedBuffs = new HashSet<int>();
        while (selectedBuffs.Count < count)
        {
            selectedBuffs.Add(Random.Range(0, buffs.Length));
        }
        return new List<int>(selectedBuffs);
    }
}
