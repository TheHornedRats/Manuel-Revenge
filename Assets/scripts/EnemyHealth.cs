using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    [Header("Salud")]
    public int maxHealth = 100;
    private int currentHealth;
    private bool isDead = false;
    private Animator animator;

    [Header("Recompensa de Puntuación y Experiencia")]
    public int puntuacion;
    public GameObject xp5Prefab;
    public GameObject xp10Prefab;
    public GameObject xp20Prefab;
    public float dropRange = 1.0f;

    [Header("Combo")]
    public TextMeshPro comboTextPrefab;
    private static int comboCount = 0;
    private static float lastKillTime;
    private static float comboResetTime = 3.0f;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        Debug.Log($"[DEBUG] {name} recibiendo {damage} de daño por {this.GetType().Name}");

        currentHealth -= damage;
        Debug.Log($"[DAÑO] {name} sufrió {damage} de daño. Salud restante: {currentHealth}");

        if (animator != null)
            animator.SetTrigger("Hurt");

        SanctifyEffect sanctifyEffect = GetComponent<SanctifyEffect>();
        if (sanctifyEffect != null)
        {
            sanctifyEffect.HealPlayer(damage);
        }

        if (currentHealth <= 0 && !isDead)
        {
            Debug.Log($"[MUERTE] {name} activando Die()"); // <- Añade esto
            Die();
        }

    }

    public int GetHealth()
    {
        return currentHealth;
    }

    private void Die()
    {
        isDead = true;
        Debug.Log($"{name} ha muerto.");      //La animacion de muerte está bug

        //if (animator != null)
        //    animator.SetTrigger("Die");

        UpdateCombo();
        DropXPItems();

        ScoreManager.instance.AddScore(5);

        // Espera a que la animación termine antes de destruir
        Destroy(gameObject); // Ajusta según la duración de la animación
    }

    private void DropXPItems()
    {
        Vector3 dropPosition1 = transform.position + new Vector3(Random.Range(-dropRange, dropRange), Random.Range(-dropRange, dropRange), 0);
        Vector3 dropPosition2 = transform.position + new Vector3(Random.Range(-dropRange, dropRange), Random.Range(-dropRange, dropRange), 0.1f);
        Vector3 dropPosition3 = transform.position + new Vector3(Random.Range(-dropRange, dropRange), Random.Range(-dropRange, dropRange), -0.1f);

        Instantiate(xp5Prefab, dropPosition1, Quaternion.identity);
        Instantiate(xp10Prefab, dropPosition2, Quaternion.identity);
        Instantiate(xp20Prefab, dropPosition3, Quaternion.identity);
    }

    private void UpdateCombo()
    {
        if (Time.time - lastKillTime > comboResetTime)
        {
            comboCount = 1;
        }
        else
        {
            comboCount++;
        }

        lastKillTime = Time.time;
        ShowComboText();
    }

    private void ShowComboText()
    {
        if (comboTextPrefab == null) return;

        TextMeshPro comboTextInstance = Instantiate(comboTextPrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
        comboTextInstance.text = $"Combo x{comboCount}";
        comboTextInstance.fontSize = Mathf.Clamp(3 + (comboCount * 0.5f), 3, 7);
        comboTextInstance.color = Color.Lerp(Color.white, Color.red, comboCount / 10f);

        Destroy(comboTextInstance.gameObject, 1.5f);
    }
}
