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
        base.CreateParticleSystem();
        if (effectParticles != null)
        {
            var main = effectParticles.main;
            main.startColor = new Color(1f, 0.95f, 0.7f); // Dorado suave
            main.startSize = 0.15f;
            main.startLifetime = 0.8f;
            main.startSpeed = 0.1f;
            // main.duration = ... -> NO tocar duration en tiempo de ejecuci�n

            var shape = effectParticles.shape;
            shape.shapeType = ParticleSystemShapeType.Cone;
            shape.angle = 15f;
            shape.radius = 0.1f;

            var emission = effectParticles.emission;
            emission.rateOverTime = 10f;

            var colorOverLifetime = effectParticles.colorOverLifetime;
            colorOverLifetime.enabled = true;
            Gradient grad = new Gradient();
            grad.SetKeys(
                new GradientColorKey[] {
                new GradientColorKey(new Color(1f, 1f, 0.7f), 0.0f),
                new GradientColorKey(Color.clear, 1.0f)
                },
                new GradientAlphaKey[] {
                new GradientAlphaKey(1f, 0f),
                new GradientAlphaKey(0f, 1f)
                }
            );
            colorOverLifetime.color = grad;
        }
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
