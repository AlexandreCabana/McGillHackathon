using System.Collections.Generic;
using UnityEngine;

public class OrbitAnalyzer : MonoBehaviour
{
    public int samples = 100; // Number of distance samples to track for analysis
    public float maxDistance = 10f; // Reference maximum distance for scoring
    public float circularityWeight = 0.6f; // Weight for circularity in scoring (60%)
    public float proximityWeight = 0.3f; // Weight for proximity (30%)
    public float stabilityWeight = 0.1f; // Weight for stability (10%)
    public float circularityThreshold = 0.2f; // Threshold for acceptable circularity variance

    private List<float> distances = new List<float>(); // Tracks the distances from the planet
    private List<Vector2> positions = new List<Vector2>(); // Tracks the positions for stability checks
    private bool gameStopped = false; // Tracks if the game is stopped

    void Update()
    {
        if (gameStopped) return; // Skip updates if the game is stopped

        // Detect key press for manual orbit analysis
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("O key pressed! Performing orbit analysis...");
            AnalyzeOrbit();
        }

        // Track distances and positions while the game is running
        TrackDistance();
        TrackPosition();
    }

    void TrackDistance()
    {
        GameObject closestPlanet = FindClosestPlanet();
        if (closestPlanet == null) return;

        float distance = Vector2.Distance(transform.position, closestPlanet.transform.position);
        distances.Add(distance);

        // Maintain a fixed number of samples
        if (distances.Count > samples)
        {
            distances.RemoveAt(0);
        }
    }

    void TrackPosition()
    {
        positions.Add(transform.position);

        // Limit the number of stored positions
        if (positions.Count > samples * 10)
        {
            positions.RemoveAt(0);
        }
    }

    GameObject FindClosestPlanet()
    {
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");
        GameObject endPlanet = GameObject.FindGameObjectWithTag("EndPlanet");

        GameObject closest = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject planet in planets)
        {
            float distance = Vector2.Distance(transform.position, planet.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closest = planet;
            }
        }

        if (endPlanet != null)
        {
            float endDistance = Vector2.Distance(transform.position, endPlanet.transform.position);
            if (endDistance < shortestDistance)
            {
                closest = endPlanet;
            }
        }

        return closest;
    }

    void AnalyzeOrbit()
    {
        if (distances.Count == 0 || positions.Count < samples)
        {
            Debug.LogWarning("Not enough orbit data to analyze.");
            return;
        }

        // Calculate average and variance of distances
        float averageDistance = 0f;
        float variance = 0f;

        foreach (float distance in distances)
        {
            averageDistance += distance;
        }
        averageDistance /= distances.Count;

        foreach (float distance in distances)
        {
            variance += Mathf.Pow(distance - averageDistance, 2);
        }
        variance /= distances.Count;

        // Determine orbit type
        bool isInfinite = CheckOrbitStability();
        float stabilityScore = isInfinite ? 100f : 0f;

        // Calculate circularity
        float circularity = Mathf.Clamp01(1f - Mathf.Sqrt(variance) / averageDistance) * 100f;

        // Penalize non-infinite orbits
        if (!isInfinite)
        {
            Debug.LogWarning("Orbit is unstable or not infinite.");
            circularity = Mathf.Min(circularity, 20f); // Heavily penalized
        }

        // Calculate proximity score
        float proximityScore = Mathf.Clamp01(1f - (averageDistance / maxDistance)) * 100f;

        // Ensure weights sum to 1.0
        float totalWeight = circularityWeight + proximityWeight + stabilityWeight;
        if (totalWeight != 1.0f)
        {
            Debug.LogWarning("Weights do not sum to 1. Normalizing weights.");
            circularityWeight /= totalWeight;
            proximityWeight /= totalWeight;
            stabilityWeight /= totalWeight;
        }

        // Calculate final score
        float goodnessScore = (circularityWeight * circularity) +
                              (proximityWeight * proximityScore) +
                              (stabilityWeight * stabilityScore);

        // Clamp final score to 0–100
        goodnessScore = Mathf.Clamp(goodnessScore, 0f, 100f);

        // Print results
        Debug.Log($"Orbit Analysis:");
        Debug.Log($"Circularity: {circularity:F2}%");
        Debug.Log($"Average Distance: {averageDistance:F2} units");
        Debug.Log($"Orbit Stability: {(isInfinite ? "Stable (Infinite)" : "Unstable")}");
        Debug.Log($"Goodness Score: {goodnessScore:F2}/100");

        StopGame();
    }

    bool CheckOrbitStability()
    {
        // Check if the orbit is stable by comparing distance fluctuations
        if (distances.Count < samples) return false;

        float maxDistance = Mathf.Max(distances.ToArray());
        float minDistance = Mathf.Min(distances.ToArray());
        float stabilityThreshold = 0.1f * maxDistance; // 10% threshold for stability

        return (maxDistance - minDistance) <= stabilityThreshold;
    }

    void StopGame()
    {
        Time.timeScale = 0f; // Freeze the game
        gameStopped = true;
    }
}