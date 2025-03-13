using UnityEngine;

public class SanctifyEffect : StatusEffect
{
    // Multiplicador para reducir el daño del enemigo
    private float reductionMultiplier = 0.7f;
    // Porcentaje de curación para el jugador
    private float healPercentage = 0.1f;
    // Almacenamos el daño original para restaurarlo al finalizar el efecto
    private int originalDamage = -1;

    /// <summary>
    /// Sobrescribimos CreateParticleSystem para asignar un color y material específico.
    /// </summary>
    protected override void CreateParticleSystem()
    {
        // Llama al método base que crea el GameObject y el ParticleSystem
        base.CreateParticleSystem();

        // Cambia el color de inicio a algo distintivo, por ejemplo, amarillo
        var main = effectParticles.main;
        main.startColor = Color.yellow;

        // Asegúrate de asignar un material válido para partículas
        ParticleSystemRenderer psRenderer = effectParticles.GetComponent<ParticleSystemRenderer>();
        if (psRenderer != null)
        {
            // Creamos un material en runtime con un shader de partículas
            Material runtimeMat = new Material(Shader.Find("Particles/Standard Unlit"));
            psRenderer.material = runtimeMat;

            // Opcional: Ajustar la capa de sorting si estás en 2D
            psRenderer.sortingLayerName = "Default";
            psRenderer.sortingOrder = 20;
        }

        Debug.Log("[Santificación] Particle System creado y configurado.");
    }

    protected override void OnEffectStart()
    {
        // Lógica de reducción de daño del enemigo
        EnemyAttack enemyAttack = enemyHealth.GetComponent<EnemyAttack>();
        if (enemyAttack != null)
        {
            if (originalDamage < 0)
                originalDamage = enemyAttack.damage;

            enemyAttack.damage = Mathf.RoundToInt(originalDamage * reductionMultiplier);
            Debug.Log($"{enemyHealth.name} ha sido santificado. Daño reducido a {enemyAttack.damage}");
        }
        else
        {
            Debug.LogWarning($"[Santificación] {enemyHealth.name} NO tiene EnemyAttack.");
        }
    }

    /// <summary>
    /// Se llama desde EnemyHealth cuando el enemigo recibe daño.
    /// </summary>
    public void HealPlayer(int damageDealt)
    {
        if (damageDealt <= 0) return;

        int healAmount = Mathf.RoundToInt(damageDealt * healPercentage);
        if (healAmount > 0)
        {
            PlayerHealth player = FindObjectOfType<PlayerHealth>();
            if (player != null)
            {
                player.Heal(healAmount);
                Debug.Log($"El jugador se cura {healAmount} de vida gracias a la Santificación.");
            }
            else
            {
                Debug.LogWarning("[Santificación] No se encontró un objeto PlayerHealth en la escena.");
            }
        }
    }

    protected override void OnEffectEnd()
    {
        // Restaurar el daño original
        EnemyAttack enemyAttack = enemyHealth.GetComponent<EnemyAttack>();
        if (enemyAttack != null && originalDamage > 0)
        {
            enemyAttack.damage = originalDamage;
            Debug.Log($"{enemyHealth.name} ha recuperado su daño original: {originalDamage}");
        }
    }
}
