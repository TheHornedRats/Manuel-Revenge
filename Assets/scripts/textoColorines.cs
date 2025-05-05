using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class textoColorines : MonoBehaviour
{

    public TextMeshProUGUI weaponSelectedText;

    // Start is called before the first frame update
    void Start()
    {
        weaponSelectedText.text = "<color=yellow>ESC </color>" + "To pause";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
