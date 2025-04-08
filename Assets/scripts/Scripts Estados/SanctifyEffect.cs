using UnityEngine;

public class SanctifyEffect : StatusEffect
{
    // Multiplicador para reducir el da�o del enemigo
    private float reductionMultiplier = 0.7f;
    // Porcentaje de curaci�n para el jugador
    private float healPercentage = 0.1f;
    // Almacenamos el da�o original para restaurarlo al finalizar el efecto
    private int originalDamage = -1;

    /// <summary>
    /// Sobrescribimos CreateParticleSystem para asignar un color y material espec�fico.
    /// </summary>
    protected override void CreateParticleSystem()
    {
        // Llama al m�todo base que crea el GameObject y el ParticleSystem
        base.CreateParticleSystem();

        // Cambia el color de inicio a algo distintivo, por ejemplo, amarillo
        var main = effectParticles.main;
        main.startColor = Color.yellow;

        // Aseg�rate de asignar un material v�lido para part�culas
        ParticleSystemRenderer psRenderer = effectParticles.GetComponent<ParticleSystemRenderer>();
        if (psRenderer != null)
        {
            // Creamos un material en runtime con un shader de part�culas
            Material runtimeMat = new Material(Shader.Find("Particles/Standard Unlit"));
            psRenderer.material = runtimeMat;

            // Opcional: Ajustar la capa de sorting si est�s en 2D
            psRenderer.sortingLayerName = "Default";
            psRenderer.sortingOrder = 20;
        }

        Debug.Log("[Santificaci�n] Particle System creado y configurado.");
    }

    protected override void OnEffectStart()
    {
        // L�gica de reducci�n de da�o del enemigo
        EnemyAttack enemyAttack = enemyHealth.GetComponent<EnemyAttack>();
        if (enemyAttack != null)
        {
            if (originalDamage < 0)
                originalDamage = enemyAttack.damage;

            enemyAttack.damage = Mathf.RoundToInt(originalDamage * reductionMultiplier);
            Debug.Log($"{enemyHealth.name} ha sido santificado. Da�o reducido a {enemyAttack.damage}");
        }
        else
        {
            Debug.LogWarning($"[Santificaci�n] {enemyHealth.name} NO tiene EnemyAttack.");
        }
    }

    /// <summary>
    /// Se llama desde EnemyHealth cuando el enemigo recibe da�o.
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
                Debug.Log($"El jugador se cura {healAmount} de vida gracias a la Santificaci�n.");
            }
            else
            {
                Debug.LogWarning("[Santificaci�n] No se encontr� un objeto PlayerHealth en la escena.");
            }
        }
    }

    protected override void OnEffectEnd()
    {
        // Restaurar el da�o original
        EnemyAttack enemyAttack = enemyHealth.GetComponent<EnemyAttack>();
        if (enemyAttack != null && originalDamage > 0)
        {
            enemyAttack.damage = originalDamage;
            Debug.Log($"{enemyHealth.name} ha recuperado su da�o original: {originalDamage}");
        }
    }
}
