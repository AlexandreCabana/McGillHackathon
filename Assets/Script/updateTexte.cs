using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class updateTexte : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    public FloatScriptableObject value;
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = value.value.ToString() + "/100";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
