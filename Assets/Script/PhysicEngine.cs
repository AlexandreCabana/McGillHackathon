using System.Collections.Generic;
using UnityEngine;

public class PhysicEngine : MonoBehaviour
{
    private const float gravitationalUniversalConstant = 6.67430e-6f; // Scaled G for Unity
    public float weight; // Mass of the rocket
    private Rigidbody2D rb;
    private Vector2 position;
    private Vector2 velocity = Vector2.zero;
    [HideInInspector]
    public Vector2 rocketInputAcceleration = Vector2.zero;

    public List<PlanetProperty> planets = new List<PlanetProperty>(); // Planets affecting gravity
    public LineRenderer trajectoryLine; // LineRenderer for the trajectory visualization
    public int trajectorySteps = 100; // Number of steps to predict
    public float timeStep = 0.1f; // Time between prediction steps

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        position = rb.position;

        // Automatically find and assign planets if not manually set
        if (planets.Count == 0)
        {
            planets.AddRange(FindObjectsOfType<PlanetProperty>());
        }

        // Ensure LineRenderer is assigned
        if (trajectoryLine == null)
        {
            Debug.LogError("LineRenderer for trajectory is not assigned!");
        }
    }

    void FixedUpdate()
    {
        Vector2 gravityForce = CalculateGravity();
        Vector2 gravityAcceleration = gravityForce / weight;

        // Total acceleration (gravity + player input)
        Vector2 totalAcceleration = gravityAcceleration + rocketInputAcceleration;

        // Update velocity and position
        velocity += totalAcceleration * Time.fixedDeltaTime;
        position += velocity * Time.fixedDeltaTime;

        rb.MovePosition(position);

        // Reset rocketInputAcceleration to prevent continuous input
        rocketInputAcceleration = Vector2.zero;

        // Update the trajectory line
        UpdateTrajectoryLine();
    }

    private Vector2 CalculateGravity()
    {
        Vector2 totalGravityForce = Vector2.zero;

        foreach (PlanetProperty planet in planets)
        {
            totalGravityForce += CalculateForceFromPlanet(planet);
        }

        return totalGravityForce;
    }

    private Vector2 CalculateForceFromPlanet(PlanetProperty planet)
    {
        Vector2 direction = planet.position - position;
        float distanceSquared = direction.sqrMagnitude;

        // Cap the minimum distance to avoid extremely large gravitational force
        float minDistance = 2f; // Minimum distance in Unity units
        if (distanceSquared < minDistance * minDistance)
            distanceSquared = minDistance * minDistance;

        float forceMagnitude = (gravitationalUniversalConstant * planet.weight * weight) / distanceSquared;
        Vector2 force = direction.normalized * forceMagnitude;

        return force;
    }

    private void UpdateTrajectoryLine()
    {
        if (trajectoryLine == null) return;

        // Create a list to store the predicted positions
        List<Vector3> predictedPositions = new List<Vector3>();
        Vector2 simulatedPosition = position;
        Vector2 simulatedVelocity = velocity;

        for (int i = 0; i < trajectorySteps; i++)
        {
            // Calculate simulated forces and accelerations
            Vector2 gravityForce = Vector2.zero;
            foreach (PlanetProperty planet in planets)
            {
                gravityForce += CalculateSimulatedForceFromPlanet(planet, simulatedPosition);
            }

            Vector2 gravityAcceleration = gravityForce / weight;
            Vector2 totalAcceleration = gravityAcceleration;

            // Update simulated velocity and position
            simulatedVelocity += totalAcceleration * timeStep;
            simulatedPosition += simulatedVelocity * timeStep;

            // Add the predicted position to the list
            predictedPositions.Add(new Vector3(simulatedPosition.x, simulatedPosition.y, 0));
        }

        // Apply the predicted positions to the LineRenderer
        trajectoryLine.positionCount = predictedPositions.Count;
        trajectoryLine.SetPositions(predictedPositions.ToArray());
    }

    private Vector2 CalculateSimulatedForceFromPlanet(PlanetProperty planet, Vector2 simulatedPosition)
    {
        Vector2 direction = planet.position - simulatedPosition;
        float distanceSquared = direction.sqrMagnitude;

        // Cap the minimum distance to avoid extremely large gravitational force
        float minDistance = 2f; // Minimum distance in Unity units
        if (distanceSquared < minDistance * minDistance)
            distanceSquared = minDistance * minDistance;

        float forceMagnitude = (gravitationalUniversalConstant * planet.weight * weight) / distanceSquared;
        Vector2 force = direction.normalized * forceMagnitude;

        return force;
    }

    public void registerPlanet(PlanetProperty planet)
    {
        planets.Add(planet);
    }
}