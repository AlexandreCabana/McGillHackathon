using UnityEngine;
using UnityEngine.UI;

public class Fuel : MonoBehaviour
{
    public Slider fuelBar; // Reference to the slider UI element
    public float maxFuel = 100f; // Maximum fuel amount
    public float fuelConsumptionRate = 0.1f; // Fuel consumption per second

    private float currentFuel;

    void Start()
    {
        currentFuel = maxFuel;

        if (fuelBar != null)
        {
            fuelBar.maxValue = maxFuel;
            fuelBar.value = currentFuel;
        }
    }

    void Update()
    {
        // Update the UI fuel bar
        if (fuelBar != null)
        {
            fuelBar.value = currentFuel;
        }
    }

    public bool ConsumeFuel(float amount)
    {
        if (currentFuel > 0)
        {
            currentFuel -= amount * Time.deltaTime;

            // Clamp to ensure fuel doesn’t go below zero
            if (currentFuel < 0)
            {
                currentFuel = 0;
            }

            return true; // Successfully consumed fuel
        }

        return false; // No fuel left
    }

    public float GetCurrentFuel()
    {
        return currentFuel;
    }

    public bool HasFuel()
    {
        return currentFuel > 0;
    }
}