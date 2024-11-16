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
    void Start()
    {
        Phys = GetComponent<PhysicEngine>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A)){
            Rotate("Left");
        } else if (Input.GetKey(KeyCode.D)){
            Rotate("Right");
        }
        angle = Mathf.Deg2Rad*transform.localRotation.eulerAngles.z+Mathf.PI/2;
        if (Input.GetKey(KeyCode.W)){
            if (velocity.magnitude < maxSpeed)
            {
                velocity += new Vector2(Mathf.Cos(angle)*acceleration,Mathf.Sin(angle)*acceleration) * Time.deltaTime;
                if (velocity.magnitude > maxSpeed)
                {
                    velocity = new Vector2(Mathf.Cos(angle) * maxSpeed, Mathf.Sin(angle) * maxSpeed);
                }
            }
        }
        Phys.velocity = velocity;
    }

    void Rotate(String direction){
        switch(direction){
            case "Left":
                transform.Rotate(0,0,rotationSpeed);
                break;
            case "Right":
                transform.Rotate(0,0,-rotationSpeed);
                break;
        }
    }
}
