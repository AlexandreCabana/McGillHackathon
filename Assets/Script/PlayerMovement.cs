using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PhysicEngine Phys; 
    public float rotationSpeed = 0.5f; 
    public float movementSpeed;
    public float maxSpeed = 5f;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A)){
            Rotate("Left");
        } else if (Input.GetKey(KeyCode.D)){
            Rotate("Right");
        } else if (Input.GetKey(KeyCode.W) && ((Phys.velocity.magnitude<maxSpeed && Phys.velocity.x*Phys.velocity.y>=0)|| (Phys.velocity.x*Phys.velocity.y<=0))){
            Accelerate(movementSpeed);
            Debug.Log("ACCELERATING");
        } else if(Input.GetKey(KeyCode.S) && Phys.velocity.magnitude>0){
            Accelerate(-movementSpeed);
        }else{
            Phys.rocketInputAcceleration = Vector2.zero;
        }
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

    void Accelerate(float amount){
        float angle = transform.localRotation.eulerAngles.z;
        // BROKEN
        Phys.rocketInputAcceleration += new Vector2(Mathf.Cos(angle)*amount,Mathf.Sin(angle)*amount);
    }

    void Decelerate(float amount){

    }

}
