using UnityEngine;

public class ItemActivator : MonoBehaviour
{
    public ShopManager shopManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            ActivateItem(0);
        if (Input.GetKeyDown(KeyCode.F))
            ActivateItem(1);
        if (Input.GetKeyDown(KeyCode.C))
            ActivateItem(2);
    }

    void ActivateItem(int index)
    {
        if (shopManager.UseItem(index))
        {
            Debug.Log($"Item {index} activado!");
            // Aquí aplica efecto del ítem
        }
        else
        {
            Debug.Log($"No tienes ítems {index} disponibles.");
        }
    }
}
