using UnityEngine;

public class BossEnemy : EnemyHealth
{
    [Header("Boss Settings")]
    public float abilityCooldown = 5f;
    private float nextAbilityTime;

    [Header("Boss Abilities")]
    public GameObject ability1Prefab; // Ataque cuerpo a cuerpo
    public GameObject ability2Prefab; // Proyectil que ralentiza
    public GameObject summon1Prefab;  // Invoca Enemigo 1 (instancia de objeto con script)
    public GameObject summon2Prefab;  // Invoca Enemigo 2 (instancia de objeto con script)

    [SerializeField]
    private new Animator animator;

    private bool isAttacking = false;
    private float attackDuration = 0.6f;

    protected override void Start()
    {
        base.Start();
        nextAbilityTime = Time.time + abilityCooldown;
        maxHealth *= 5;
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDead || isAttacking) return;

        if (Time.time >= nextAbilityTime)
        {
            UseRandomAbility();
            nextAbilityTime = Time.time + abilityCooldown;
        }
    }

    private void UseRandomAbility()
    {
        isAttacking = true;
        int abilityIndex = Random.Range(0, 4);

        switch (abilityIndex)
        {
            case 0:
                if (ability1Prefab != null)
                {
                    animator.SetTrigger("Attack1");
                    Instantiate(ability1Prefab, transform.position, Quaternion.identity);
                }
                break;

            case 1:
                if (ability2Prefab != null)
                {
                    animator.SetTrigger("Attack2");
                    Instantiate(ability2Prefab, transform.position, Quaternion.identity);
                }
                break;

            case 2:
                if (summon1Prefab != null)
                {
                    animator.SetTrigger("Attack3");
                    Invoke(nameof(InvokeSummon1), 0.4f);
                }
                break;

            case 3:
                if (summon2Prefab != null)
                {
                    animator.SetTrigger("Attack4");
                    Invoke(nameof(InvokeSummon2), 0.4f);
                }
                break;
        }

        Invoke(nameof(ResetAttackState), attackDuration);
    }

    private void InvokeSummon1()
    {
        Instantiate(summon1Prefab, transform.position, Quaternion.identity);
    }

    private void InvokeSummon2()
    {
        Instantiate(summon2Prefab, transform.position, Quaternion.identity);
    }

    private void ResetAttackState()
    {
        isAttacking = false;
    }

    public override void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            animator?.SetTrigger("Hurt");
        }
    }

    protected override void Die()
    {
        isDead = true;
        animator?.SetTrigger("Die");

        DropXPItems();
        ScoreManager.instance.AddScore(5);

        Destroy(gameObject, 0.6f); // tiempo estimado de la animación de muerte
    }
}
