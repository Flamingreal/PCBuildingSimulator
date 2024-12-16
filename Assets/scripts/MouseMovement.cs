using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script handles mouse movement for controlling the camera's orientation,
// allowing the player to look around in both horizontal and vertical directions.

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Sensitivity of the mouse movement.

    private float xRotation = 0f; // Tracks rotation around the X-axis (up and down).
    private float yRotation = 0f; // Tracks rotation around the Y-axis (left and right).

    // Start is called before the first frame update
    void Start()
    {
        // Lock the cursor to the center of the screen and make it invisible.
        // This is commonly used in first-person or third-person games.
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse input for horizontal (X) and vertical (Y) movement.
        // Multiply by sensitivity and deltaTime for consistent movement across frame rates.
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Control vertical rotation (up and down) by modifying xRotation.
        xRotation -= mouseY;

        // Clamp the vertical rotation to prevent over-rotating (e.g., flipping upside-down).
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Control horizontal rotation (left and right) by modifying yRotation.
        yRotation += mouseX;

        // Apply the calculated rotations to the transform's local rotation.
        // The X-axis rotation affects looking up and down.
        // The Y-axis rotation affects looking left and right.
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
