using System.Collections;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    public Collider2D swordAttackCollider;
    public SpriteRenderer swordSpriteRenderer;
    public float attackDuration = 0.5f; // Duración del ataque
    public float delayBeforeDeactivation = 0.5f; // Espera antes de desactivar
    public float attackDelay = 2f; // Tiempo entre ataques

    private float attackTimer = 0f; // Temporizador para el siguiente ataque

    // Start is called before the first frame update
    void Start()
    {
        if (swordAttackCollider != null)
        {
            swordAttackCollider.enabled = false;
            swordSpriteRenderer.enabled = false;
        }
        attackTimer = attackDelay; // Inicializa el temporizador con el delay de ataque
    }

    // Update is called once per frame
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
        // Activa la colisión y el sprite del ataque
        swordAttackCollider.enabled = true;
        swordSpriteRenderer.enabled = true;

        // Espera el tiempo de duración del ataque
        yield return new WaitForSeconds(attackDuration);

        // Espera el tiempo antes de desactivar el ataque
        yield return new WaitForSeconds(delayBeforeDeactivation);

        // Desactiva el ataque
        swordAttackCollider.enabled = false;
        swordSpriteRenderer.enabled = false;
    }
}
