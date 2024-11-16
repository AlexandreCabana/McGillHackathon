using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PhysicEngine Phys; 
    public float rotationSpeed = 0.5f; 
    public float acceleration;
    public float maxSpeed = 5f;
    private Vector2 velocity;
    private float angle;
    private Fuel fuel;
    void Start()
    {
        Phys = GetComponent<PhysicEngine>();
        fuel = GameObject.Find("Panel").GetComponent<Fuel>();
    }

    void Update()
    {
        transform.Rotate(0, 0, -Input.GetAxis("Horizontal"));
        angle = Mathf.Deg2Rad*transform.localRotation.eulerAngles.z+Mathf.PI/2;
        if (Input.GetKey(KeyCode.W) && fuel.fuel>0){
            fuel.move();
            velocity += new Vector2(Mathf.Cos(angle)*acceleration,Mathf.Sin(angle)*acceleration) * Time.deltaTime;
            if (velocity.magnitude > maxSpeed)
            {
                velocity = new Vector2(Mathf.Cos(angle) * maxSpeed, Mathf.Sin(angle) * maxSpeed);
            }
        }
        Phys.velocity = velocity;
    }
}
