using UnityEngine;

[CreateAssetMenu(fileName = "NewStatusEffect", menuName = "ScriptableObjects/StatusData")]
public class StatusData : ScriptableObject
{
    public string statusName;  // Nombre del efecto (Ej: "Fuego", "Electrocución")
    public float duration;  // Duración del efecto
    public float effectDamage;  // Daño por segundo o por tick
    public int effectTicks;  // Cantidad de veces que se aplica el daño
    public bool canSpread;  // Si se propaga a otros enemigos
    public float spreadRadius;  // Radio de propagación
    public float knockbackForce;  // Retroceso si aplica
    public GameObject visualEffectPrefab;  // Prefab visual del efecto (llamas, rayos, etc.)
}
