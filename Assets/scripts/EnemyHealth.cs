using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    [Header("Salud")]
    public int maxHealth = 100;
    protected int currentHealth;
    protected bool isDead = false;
    protected Animator animator;

    [Header("Recompensa de Puntuación y Experiencia")]
    public int puntuacion;
    public GameObject xp5Prefab;
    public GameObject xp10Prefab;
    public GameObject xp20Prefab;
    public float dropRange = 1.0f;

    [Header("Combo")]
    public TextMeshPro comboTextPrefab;
    protected static int comboCount = 0;
    protected static float lastKillTime;
    protected static float comboResetTime = 3.0f;

    [Header("Texto de Daño")]
    public GameObject damageTextPrefab;  

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public virtual void TakeDamage(int damage)
    {
        if (isDead) return;

        Debug.Log($"[DEBUG] {name} recibiendo {damage} de daño por {this.GetType().Name}");

        currentHealth -= damage;
        Debug.Log($"[DAÑO] {name} sufrió {damage} de daño. Salud restante: {currentHealth}");

        ShowDamageText(damage); 

        SanctifyEffect sanctifyEffect = GetComponent<SanctifyEffect>();
        if (sanctifyEffect != null)
        {
            sanctifyEffect.HealPlayer(damage);
        }

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
        else
        {
            if (animator != null)
                animator.SetTrigger("Hurt");
        }
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    protected virtual void Die()
    {
        isDead = true;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        Debug.Log($"{name} ha muerto.");

        if (animator != null)
            animator.SetTrigger("Die");

        UpdateCombo();
        DropXPItems();

        Destroy(gameObject, 0.5f);
    }

    protected void DropXPItems()
    {
        Vector3 dropPosition1 = transform.position + new Vector3(Random.Range(-dropRange, dropRange), Random.Range(-dropRange, dropRange), 0);
        Vector3 dropPosition2 = transform.position + new Vector3(Random.Range(-dropRange, dropRange), Random.Range(-dropRange, dropRange), 0.1f);
        Vector3 dropPosition3 = transform.position + new Vector3(Random.Range(-dropRange, dropRange), Random.Range(-dropRange, dropRange), -0.1f);

        Instantiate(xp5Prefab, dropPosition1, Quaternion.identity);
        Instantiate(xp10Prefab, dropPosition2, Quaternion.identity);
        Instantiate(xp20Prefab, dropPosition3, Quaternion.identity);
    }

    protected void UpdateCombo()
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

    protected void ShowComboText()
    {
        if (comboTextPrefab == null) return;

        TextMeshPro comboTextInstance = Instantiate(comboTextPrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
        comboTextInstance.text = $" x{comboCount}";
        comboTextInstance.fontSize = Mathf.Clamp(3 + (comboCount * 0.5f), 3, 7);
        comboTextInstance.color = Color.Lerp(Color.white, Color.red, comboCount / 10f);

        Destroy(comboTextInstance.gameObject, 1.5f);
    }

    protected void ShowDamageText(int damage)
    {
        if (damageTextPrefab == null) return;

        Vector3 offset = new Vector3(Random.Range(-0.3f, 0.3f), 1.0f, 0);
        GameObject instance = Instantiate(damageTextPrefab, transform.position + offset, Quaternion.identity);

        TextMeshPro text = instance.GetComponent<TextMeshPro>();
        if (text != null)
        {
            text.text = $"-{damage}";
            text.fontSize = 4 + damage * 0.1f;
            text.color = new Color(1f, 0.843f, 0f);
            ;
        }

        Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0, 1.5f);
        }

        Destroy(instance, 1.0f);
    }
}
