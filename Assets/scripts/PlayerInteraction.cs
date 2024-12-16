using UnityEngine;
using UnityEngine.UI;

// This script handles player interaction with PC components, including picking up objects, 
// adjusting their distance, and updating the crosshair color based on interaction possibilities.

public class PlayerInteraction : MonoBehaviour
{
    public float reachDistance = 50f; // Maximum interaction distance for the player.
    public Image crosshairImage; // Reference to the crosshair UI image.
    public Color defaultCrosshairColor = Color.white; // Default color of the crosshair.
    public Color interactableCrosshairColor = Color.green; // Crosshair color when an interactable object is in range.

    private S_PcComponent currentComponent; // Reference to the currently picked-up PC component.

    // Update is called once per frame
    void Update()
    {
        UpdateCrosshair(); // Update the crosshair's appearance based on the interaction state.
        HandleInteraction(); // Handle interaction logic (pick up or drop objects).
        HandleDistanceAdjustment(); // Handle object distance adjustment using the mouse scroll wheel.
    }

    // Updates the crosshair color based on the player's ability to interact with objects.
    private void UpdateCrosshair()
    {
        // Ensure there is a Main Camera tagged as "MainCamera".
        if (Camera.main == null)
        {
            Debug.LogError("Main Camera not found! Ensure your camera is tagged as 'MainCamera'.");
            return;
        }

        // Ensure the crosshair image is assigned in the Inspector.
        if (crosshairImage == null)
        {
            Debug.LogWarning("Crosshair Image is not assigned in the Inspector.");
            return;
        }

        // If an object is currently being picked up, show the interactable color.
        if (currentComponent != null && currentComponent.G_PickedUp)
        {
            crosshairImage.color = interactableCrosshairColor; // Change crosshair to green.
            return;
        }

        // Perform a raycast to detect objects in front of the player.
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, reachDistance))
        {
            // If the raycast hits a valid interactable component, change the crosshair color to green.
            if (hit.collider.TryGetComponent(out S_PcComponent component))
            {
                crosshairImage.color = interactableCrosshairColor;
            }
            else
            {
                crosshairImage.color = defaultCrosshairColor; // Revert to default color for non-interactable objects.
            }
        }
        else
        {
            crosshairImage.color = defaultCrosshairColor; // Revert to default color if no object is detected.
        }
    }

    // Handles interaction logic when the player presses the interaction key (E).
    private void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Check if the player presses the E key.
        {
            // Perform a raycast to detect objects within the interaction range.
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, reachDistance))
            {
                // If the raycast hits a valid component, handle pick-up or drop logic.
                if (hit.collider.TryGetComponent(out S_PcComponent component))
                {
                    if (!component.G_PickedUp) // If the object is not already picked up.
                    {
                        component.PickUp(); // Pick up the object.
                        currentComponent = component; // Assign it as the current component.
                    }
                    else
                    {
                        component.Drop(); // Drop the object.
                        currentComponent = null; // Clear the current component reference.
                    }
                }
            }
        }
    }

    // Handles the adjustment of the distance of the picked-up object using the mouse scroll wheel.
    private void HandleDistanceAdjustment()
    {
        // If the player is holding a component, allow distance adjustment.
        if (currentComponent != null && currentComponent.G_PickedUp)
        {
            // Get the scroll wheel input.
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                currentComponent.AdjustDistance(scroll); // Adjust the object's distance from the player.
            }
        }
    }
}
