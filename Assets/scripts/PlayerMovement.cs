using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script handles player movement including walking, jumping, and gravity.
// It uses Unity's CharacterController component for movement.

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller; // Reference to the CharacterController component.

    public float speed = 12f; // Walking speed of the player.
    public float gravity = -9.81f * 2; // Gravity force applied to the player (doubled for faster falling).
    public float jumpHeight = 3f; // The maximum height the player can jump.

    public Transform groundCheck; // A point to check if the player is touching the ground.
    public float groundDistance = 0.4f; // The radius of the ground check sphere.
    public LayerMask groundMask; // Layer mask to specify which objects are considered as ground.

    private Vector3 velocity; // Stores the player's velocity, including vertical motion.

    private bool isGrounded; // Tracks if the player is on the ground.

    // Update is called once per frame
    void Update()
    {
        // Check if the player is on the ground by casting a sphere at the ground check position.
        // This prevents the player from falling infinitely when on solid ground.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // If the player is grounded and the vertical velocity is less than 0, reset it.
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small downward force to keep the player grounded.
        }

        // Get input for horizontal (A/D or Left/Right keys) and vertical (W/S or Up/Down keys) movement.
        float x = Input.GetAxis("Horizontal"); // Input for moving left and right.
        float z = Input.GetAxis("Vertical");   // Input for moving forward and backward.

        // Calculate movement direction relative to the player's orientation.
        // Right is along the red axis, forward is along the blue axis.
        Vector3 move = transform.right * x + transform.forward * z;

        // Move the player using the CharacterController. Multiplying by speed and deltaTime ensures smooth movement.
        controller.Move(move * speed * Time.deltaTime);

        // Check if the player is grounded and presses the jump button (default is Space key).
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Calculate jump velocity using the equation: velocity = sqrt(2 * gravity * jumpHeight).
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity over time to simulate falling.
        velocity.y += gravity * Time.deltaTime;

        // Move the player vertically using the CharacterController.
        controller.Move(velocity * Time.deltaTime);
    }
}
