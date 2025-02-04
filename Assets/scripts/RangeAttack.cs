using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    public Collider2D ballestaAttackCollider;
    public SpriteRenderer ballestaSpriteRenderer;
    public float attackDuration = 2f;
    public float delayBeforeDeactivation = 0.5f;
    public float attackDelay = 2f;
    public float attackDistance = 2f;
    public float moveSpeed = 10f;

    private float attackTimer = 0f;
    private float attackTimeElapsed = 0f;
    private float deactivateTimeElapsed = 0f;
    private bool isAttacking = false;
    private Vector3 initialPosition;
    private Vector3 targetPosition;

    void Start()
    {
        if (ballestaAttackCollider != null)
        {
            ballestaAttackCollider.enabled = false;
            ballestaSpriteRenderer.enabled = false;
        }

        initialPosition = transform.position;
        attackTimer = attackDelay;
    }

    void Update()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f && !isAttacking)
        {
            StartAttack();
        }

        if (isAttacking)
        {
            attackTimeElapsed += Time.deltaTime;

            float moveDistance = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveDistance);

            if (transform.position == targetPosition)
            {
                deactivateTimeElapsed += Time.deltaTime;

                if (deactivateTimeElapsed >= delayBeforeDeactivation)
                {
                    EndAttack();
                }
            }
            else if (attackTimeElapsed >= attackDuration)
            {
                targetPosition = initialPosition;
            }
        }
    }

    private void StartAttack()
    {
        ballestaAttackCollider.enabled = true;
        ballestaSpriteRenderer.enabled = true;

        isAttacking = true;
        attackTimeElapsed = 0f;
        deactivateTimeElapsed = 0f;

        targetPosition = transform.position + transform.right * attackDistance;
    }

    private void EndAttack()
    {
        ballestaAttackCollider.enabled = false;
        ballestaSpriteRenderer.enabled = false;

        transform.position = initialPosition; // Vuelve a la posición inicial

        isAttacking = false;
        attackTimer = attackDelay;
    }
}
