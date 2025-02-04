using System.Collections;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    public Collider2D swordAttackCollider;
    public SpriteRenderer swordSpriteRenderer;
    public float attackDuration = 0.5f; // Duración del ataque
    public float delayBeforeDeactivation = 0.5f; // Espera antes de desactivar
    public float attackDelay = 2f; // Tiempo entre ataques
    public int damage = 10; // Daño del ataque
    public LayerMask enemyLayer; // Capa de los enemigos

    private float attackTimer = 0f; // Temporizador para el siguiente ataque

    void Start()
    {
        if (swordAttackCollider != null)
        {
            swordAttackCollider.enabled = false;
            swordSpriteRenderer.enabled = false;
        }
        attackTimer = attackDelay; // Inicializa el temporizador con el delay de ataque
    }

    void Update()
    {
        attackTimer -= Time.deltaTime; // Cuenta atrás para el próximo ataque

        if (attackTimer <= 0f) // Si el temporizador llega a cero, hacer el ataque
        {
            StartCoroutine(TimeToAttack());
            attackTimer = attackDelay; // Reinicia el temporizador para el siguiente ataque
        }
    }

    private IEnumerator TimeToAttack()
    {
        swordAttackCollider.enabled = true;
        swordSpriteRenderer.enabled = true;

        yield return new WaitForSeconds(attackDuration);

        swordAttackCollider.enabled = false;
        swordSpriteRenderer.enabled = false;

        yield return new WaitForSeconds(delayBeforeDeactivation);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & enemyLayer) != 0) // Verifica si el objeto pertenece a la capa de enemigos
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                Debug.Log("Golpeó a " + other.name);
                enemyHealth.TakeDamage(damage);
            }
        }
    }
}
