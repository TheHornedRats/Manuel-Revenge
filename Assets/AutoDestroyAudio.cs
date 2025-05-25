using UnityEngine;

public class AutoDestroyAudio : MonoBehaviour
{
    public float lifeTime = 10f;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
