using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerMario : MonoBehaviour
{
    public float speed = 5f;
    public float turnSpeed = 5f; // Add a turnSpeed variable to control the rotation speed
    public Transform cam;

    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Get input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Create a new movement direction vector based on input
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // If some input has been given (the player is intending to move)
        if (direction.magnitude >= 0.1f)
        {
            // Calculate the angle we should be facing
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            // Create a rotation towards that angle
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            // Smoothly rotate the player towards the target rotation over time, instead of instantly
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

            // Finally, create a new direction vector in the direction the player is now facing
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // And move the player in that direction
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
    }
}
