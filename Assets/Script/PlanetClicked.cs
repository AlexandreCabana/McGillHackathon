using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class PlanetClicked : MonoBehaviour
{
    public TextMeshProUGUI text;
    public PlanetProperty properties;
    void Start(){
        properties=gameObject.GetComponent<PlanetProperty>();
        text = GameObject.Find("Canvas").GetComponentInChildren<TextMeshProUGUI>();
    }

    void OnMouseDown(){
        text.text="Planet Mass: "+properties.weight;
    }
}
