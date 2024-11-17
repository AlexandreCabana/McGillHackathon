using UnityEngine;

public class PlanetProperty : MonoBehaviour
{
    public float weight; // Mass of the planet
    public Vector2 position;

    void Start()
    {
        position = transform.position;
    }

    void Update()
    {
        position = transform.position; // Update position if the planet moves
    }
}