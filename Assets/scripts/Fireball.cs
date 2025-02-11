using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 5f; // Velocidad de la bola de fuego
    public int damage = 10; // Daño que inflige la explosión
    public float explosionRadius = 2f; // Radio de la explosión
    public LayerMask enemyLayer; // Capa que define a los enemigos
    public GameObject explosionEffect; // Prefab del efecto de explosión

    void Start()
    {
        // Destruir la bola de fuego después de 5 segundos si no colisiona
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        // Mover la bola de fuego hacia adelante en cada frame
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si la colisión es con un objeto en la capa de enemigos
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            // Llamar al método Explode para manejar la explosión
            Explode();
        }
    }

    void Explode()
    {
        // Instanciar el efecto de explosión en la posición actual
        if (explosionEffect != null)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            // Destruir el efecto de explosión después de 1 segundo
            Destroy(explosion, 1f);
        }

        // Detectar todos los enemigos en el radio de la explosión
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyLayer);
        foreach (Collider2D enemy in enemies)
        {
            // Aplicar daño a cada enemigo detectado
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }

        // Destruir la bola de fuego después de la explosión
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        // Dibujar una esfera en la escena para visualizar el radio de la explosión
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
