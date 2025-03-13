using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float speed = 5f; // Velocidad de la bola de fuego
    public int damage = 10; // Da�o que inflige la explosi�n
    public float explosionRadius = 2f; // Radio de la explosi�n
    public LayerMask enemyLayer; // Capa que define a los enemigos
    public GameObject explosionEffect; // Prefab del efecto de explosi�n
    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        // Mover la bola de fuego en la direcci�n establecida
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si la colisi�n es con un objeto en la capa de enemigos
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            // Aplicar da�o directo al enemigo
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            // Llamar al m�todo Explode para manejar la explosi�n
            Explode();
        }
    }

    void Explode()
    {
        // Instanciar el efecto de explosi�n en la posici�n actual
        if (explosionEffect != null)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(explosion, 1f); // Destruir el efecto de explosi�n despu�s de 1 segundo
        }

        // Detectar todos los enemigos en el radio de la explosi�n
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyLayer);
        foreach (Collider2D enemy in enemies)
        {
            // Aplicar da�o a cada enemigo detectado en el �rea de explosi�n
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }

        // Destruir la bola de fuego despu�s de la explosi�n
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        // Dibujar una esfera en la escena para visualizar el radio de la explosi�n
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
