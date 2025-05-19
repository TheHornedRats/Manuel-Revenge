using UnityEngine;

public class BossEnemy : EnemyHealth
{
    [Header("Boss Settings")]
    public float abilityCooldown = 5f;
    protected float nextAbilityTime;

    [Header("Boss Abilities")]
    public GameObject ability1Prefab; // Ráfaga de rayos
    public GameObject ability2Prefab; // Rayo gordo
    public GameObject summon1Prefab;  // Bolita teledirigida
    public GameObject summon2Prefab;  // Rayo canalizado

    [SerializeField]
    private new Animator animator;

    protected bool isAttacking = false;
    protected float attackDuration = 0.6f;

    protected Rigidbody2D rb;
    protected Vector2 storedVelocity;

    protected override void Start()
    {
        base.Start();
        nextAbilityTime = Time.time + abilityCooldown;
        maxHealth *= 5;
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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

    protected virtual void UseRandomAbility()
    {
        isAttacking = true;

        if (rb != null)
        {
            storedVelocity = rb.velocity;
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }

        int abilityIndex = Random.Range(0, 4);
        Quaternion facingRotation = Quaternion.Euler(0, 0, Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg);

        switch (abilityIndex)
        {
            case 0:
                if (ability1Prefab != null)
                {
                    animator.SetTrigger("Attack1");
                    Instantiate(ability1Prefab, transform.position, facingRotation);
                    Invoke(nameof(ResetAttackState), attackDuration);
                }
                break;

            case 1:
                if (ability2Prefab != null)
                {
                    animator.SetTrigger("Attack2");
                    Instantiate(ability2Prefab, transform.position, facingRotation);
                    Invoke(nameof(ResetAttackState), attackDuration);
                }
                break;

            case 2:
                if (summon1Prefab != null)
                {
                    animator.SetTrigger("Attack3");
                    Invoke(nameof(InvokeSummon1), 0.4f);
                    Invoke(nameof(ResetAttackState), attackDuration);
                }
                break;

            case 3:
                if (summon2Prefab != null)
                {
                    animator.SetTrigger("Attack4");
                    Instantiate(summon2Prefab, transform.position, facingRotation);
                    Invoke(nameof(ResetAttackState), 2.5f);
                }
                break;
        }
    }

    protected void InvokeSummon1()
    {
        Quaternion facingRotation = Quaternion.Euler(0, 0, Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg);
        Instantiate(summon1Prefab, transform.position, facingRotation);
    }

    protected void InvokeSummon2()
    {
        Quaternion facingRotation = Quaternion.Euler(0, 0, Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg);
        Instantiate(summon2Prefab, transform.position, facingRotation);
    }

    protected void ResetAttackState()
    {
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.velocity = storedVelocity;
        }

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

        Destroy(gameObject, 0.6f);
    }
}
