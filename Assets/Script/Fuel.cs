using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    public GameObject fuelBar; 
    public float fuel = 100;
    public float fuelConsomption;

    public float barSize;

    void Start()
    {
        barSize = fuelBar.transform.localScale.y;
    }

    void Update()
    {
        move();
    }

    void move()
    {
        fuel -= fuelConsomption;
        if (fuel >= 0){
            fuelBar.transform.localScale = new Vector3(fuelBar.transform.localScale.x, barSize*fuel/100, fuelBar.transform.localScale.z);
        }
    }
}