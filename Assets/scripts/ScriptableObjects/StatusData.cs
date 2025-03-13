using UnityEngine;

[CreateAssetMenu(fileName = "NewStatusEffect", menuName = "ScriptableObjects/StatusData")]
public class StatusData : ScriptableObject
{
    public string statusName;  // Nombre del efecto (Ej: "Fuego", "Electrocuci�n")
    public float duration;  // Duraci�n del efecto
    public float effectDamage;  // Da�o por segundo o por tick
    public int effectTicks;  // Cantidad de veces que se aplica el da�o
    public bool canSpread;  // Si se propaga a otros enemigos
    public float spreadRadius;  // Radio de propagaci�n
    public float knockbackForce;  // Retroceso si aplica
    public GameObject visualEffectPrefab;  // Prefab visual del efecto (llamas, rayos, etc.)
}
