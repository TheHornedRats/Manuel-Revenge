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

        // Cambiar color según el daño recibido
        if (damage <= 25)
        {
            textMesh.color = Color.yellow;
        }
        else if (damage <= 50)
        {
            textMesh.color = new Color(1f, 0.5f, 0f); // naranja
        }
        else
        {
            textMesh.color = Color.red;
        }
    }

    void Update()
    {
        transform.position += floatDirection * floatSpeed * Time.deltaTime;
        timer += Time.deltaTime;

        if (timer >= lifeTime)
            Destroy(gameObject);
    }
}
