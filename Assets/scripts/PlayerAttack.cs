using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Ajustes de Ataque")]
    public GameObject attackPrefab; // Prefab del ataque
    public Transform attackSpawnPoint; // Punto de aparici�n del ataque
    public float attackSpeed = 5f; // Velocidad del ataque
    public int attackDamage = 10; // Da�o del ataque
    public LayerMask enemyLayer; // Capa de los enemigos

    [Header("Cooldown del Ataque")]
    public float attackInterval = 2f; // Tiempo entre ataques
    private float timeSinceLastAttack = 0f;

    // Update se llama una vez por frame
    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (timeSinceLastAttack >= attackInterval)
        {
            PerformAttack();
            timeSinceLastAttack = 0f;
        }
    }

    void PerformAttack()
    {
        // Crear el objeto de ataque
        GameObject attackObject = Instantiate(attackPrefab, attackSpawnPoint.position, Quaternion.identity);

        // Configurar los par�metros del ataque
        Attack attack = attackObject.GetComponent<Attack>();
        if (attack != null)
        {
            attack.Initialize(attackSpeed, attackDamage, enemyLayer);
        }

        // Destruir el objeto de ataque despu�s de 5 segundos
        Destroy(attackObject, 5f);
    }
}
