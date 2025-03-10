using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPpickup : MonoBehaviour
{

    public int XPobtenida = 5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Manuel")
        {
            ScoreManager.instance.AddScore(XPobtenida);

            Destroy(gameObject);
        }
    }
}
