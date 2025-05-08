using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingDamageText : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float lifeTime = 1f;
    public Vector3 floatDirection = Vector3.up;
    public TextMeshPro textMesh;

    private float timer = 0f;

    private void Awake()
    {
        if (textMesh == null)
            textMesh = GetComponent<TextMeshPro>();
    }

    public void SetDamage(int damage)
    {
        textMesh.text = $"-{damage}";
    }

    void Update()
    {
        transform.position += floatDirection * floatSpeed * Time.deltaTime;
        timer += Time.deltaTime;

        if (timer >= lifeTime)
            Destroy(gameObject);
    }
}
