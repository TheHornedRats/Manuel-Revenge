using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private float speed;
    private int damage;
    private LayerMask enemyLayer;

    public void Initialize(float attackSpeed, int attackDamage, LayerMask layer)
    {
        speed = attackSpeed;
        damage = attackDamage;
        enemyLayer = layer;
    }

    void Update()
    {
        // Mover el ataque hacia la derecha
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // Detectar colisiones con enemigos mientras se mueve
        CheckCollisions();
    }

    private void CheckCollisions()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position,0.5f,  enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("Golpeó a " + enemy.name);
            enemy.GetComponent<EnemyHealth>()?.TakeDamage(damage);
            Destroy(gameObject); // Destruir el ataque tras golpear
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
