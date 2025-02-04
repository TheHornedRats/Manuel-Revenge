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
        transform.Translate(Vector3.right * speed * Time.deltaTime);
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
                Destroy(gameObject); // Destruir el ataque tras golpear
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
