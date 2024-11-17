using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PhysicEngine : MonoBehaviour
{
    private float gravitationalUniversalConstant = 6.67430E-11f;
    private List<PlanetProprety> planets = new List<PlanetProprety>();
    public float weight;
    public Rigidbody2D rb;
    Vector2 position;
    public Vector2 velocity = Vector2.zero;
    [FormerlySerializedAs("rocketInputAcceleration")] [HideInInspector]
    public Vector2 rocketInputVelocity = Vector2.zero;
    bool hasCrached = false;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasCrached)
        {
            Vector2 gravity = calculateGravity();
            velocity += rocketInputVelocity;
            velocity += gravity * (weight * Time.deltaTime);
            position += velocity * Time.deltaTime;
            rb.MovePosition(position);
        }
        else
        {
            Debug.Log("Crashed");
        }
    }
    private Vector2 calculateAcceleration(Vector2 force)
    {
        return force/weight;
    }
    private Vector2 calculateGravity()
    {
        Vector2 gravity = Vector2.zero;
        for (int i = 0; i < planets.Count; i++)
        {
            gravity += calculteForcesFromPlanet(planets[i]);
        }
        return gravity;
    }
    private Vector2 calculteForcesFromPlanet(PlanetProprety planet)
    {
        float distance = Vector2.Distance(position, planet.position);
        if (distance==0)
        {
            return Vector2.zero;
        }
        float force = (gravitationalUniversalConstant * planet.weight * this.weight) / (distance * distance);
        float deltaY = (planet.position.y-position.y);
        float deltaX = (planet.position.x -position.x);
        float angle;
        if (deltaX == 0)
            angle = Mathf.PI / 2;
        else if (deltaX < 0)
        {
            angle = Mathf.Atan(deltaY / deltaX) + Mathf.PI;
        }
        else
        {
            angle = Mathf.Atan(deltaY / deltaX);
        }
        return new Vector2(force*Mathf.Cos(angle), force*Mathf.Sin(angle));
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene("Menu");
    }

    public void registerPlanet(GameObject planet)
    {
        planets.Add(planet.GetComponent<PlanetProprety>());
    }
}
