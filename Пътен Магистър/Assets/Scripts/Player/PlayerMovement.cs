using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float enginePower = 1000f;  // Adjust this value to control the engine power
    public float maxSpeed = 20f;       // Adjust this value to set the maximum speed
    public float turnSpeed = 10f;      // Adjust this value to set the turning speed
    public Rigidbody carRigidbody;     // Reference to the car's rigidbody component

    void Start()
    {
        // Make sure there is a Rigidbody component attached to the car GameObject
        if (!carRigidbody)
        {
            Debug.LogError("Rigidbody component not found. Please attach a Rigidbody to the car GameObject.");
        }
    }

    void Update()
    {
        // Get user input for acceleration and turning
        float acceleration = Input.GetAxis("Vertical");
        float steering = Input.GetAxis("Horizontal");

        // Apply acceleration
        Accelerate(acceleration);

        // Apply steering
        Steer(steering);
    }

    void Accelerate(float accelerationInput)
    {
        // Calculate the force to apply based on the engine power and user input
        float accelerationForce = accelerationInput * enginePower;

        // Apply the force to the car in the forward direction
        carRigidbody.AddForce(transform.forward * accelerationForce);

        // Limit the car's speed to the maximum speed
        if (carRigidbody.velocity.magnitude > maxSpeed)
        {
            carRigidbody.velocity = carRigidbody.velocity.normalized * maxSpeed;
        }
    }

    void Steer(float steeringInput)
    {
        // Calculate the rotation to apply based on the steering input
        float rotation = steeringInput * turnSpeed * Time.deltaTime;

        // Create a rotation quaternion
        Quaternion rotationQuaternion = Quaternion.Euler(0, rotation, 0);

        // Apply the rotation to the car's rigidbody
        carRigidbody.MoveRotation(carRigidbody.rotation * rotationQuaternion);
    }
}
