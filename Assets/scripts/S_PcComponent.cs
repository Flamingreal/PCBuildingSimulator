using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script handles the behavior of a PC component, including movement, interaction, and placement in a slot.
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class S_PcComponent : MonoBehaviour
{
    [Header("Component Configuration")]
    [Tooltip("Select the component type for this object.")]
    [SerializeField]
    private e_Components m_Component; // The type of component this object represents.

    [Header("Movement Settings")]
    [Tooltip("Mouse scroll sensitivity for adjusting the distance.")]
    [SerializeField]
    private float m_MouseSensitivity = 100f;

    private S_PcComponentHolder m_CurrentHolder; // Reference to the current slot the component is near.

    [Space(5)]
    [Header("Debug Information")]
    [SerializeField]
    private bool m_PickedUp; // Whether the component is currently being picked up.
    private float m_CurrentDistance = 3f; // Initial distance from the camera when picked up.
    private float m_MinDistance = 1f; // Minimum distance from the camera.
    private float m_MaxDistance = 10f; // Maximum distance from the camera.

    // Public property to check if the component is picked up.
    public bool G_PickedUp
    {
        get { return m_PickedUp; }
    }

    // Initialization
    void Start()
    {
        m_CurrentHolder = null; // Initially, the component is not near any holder.
    }

    // Returns the component type of this object.
    public e_Components GetComponentType()
    {
        return m_Component;
    }

    // Handles picking up the component.
    public void PickUp()
    {
        if (!m_PickedUp)
        {
            m_PickedUp = true;
            transform.parent = Camera.main.transform; // Attach the component to the camera.
            transform.localPosition = new Vector3(0, 0, m_CurrentDistance); // Set its initial position.
            transform.localRotation = Quaternion.identity; // Reset rotation.
            GetComponent<Rigidbody>().isKinematic = true; // Disable physics simulation.
            Debug.Log($"{name} picked up!");
        }
    }

    // Handles dropping the component.
    public void Drop()
    {
        if (m_PickedUp)
        {
            m_PickedUp = false;
            transform.parent = null; // Detach from the camera.
            GetComponent<Rigidbody>().isKinematic = false; // Re-enable physics simulation.
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None; // Remove constraints.
            Debug.Log($"{name} dropped!");

            // Ensure the component lands on the ground properly.
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity))
            {
                transform.position = new Vector3(transform.position.x, hit.point.y + 0.1f, transform.position.z);
            }
            else
            {
                Debug.LogWarning("No ground detected below object.");
            }
        }
    }

    // Adjusts the distance of the component from the camera while being held.
    public void AdjustDistance(float adjustment)
    {
        if (m_PickedUp)
        {
            m_CurrentDistance = Mathf.Clamp(m_CurrentDistance + adjustment, m_MinDistance, m_MaxDistance); // Clamp the distance.
            transform.localPosition = new Vector3(0, 0, m_CurrentDistance); // Update position.

            // Check if the adjusted distance maintains ground proximity.
            if (!Physics.Raycast(transform.position, Vector3.down, 1f))
            {
                Debug.LogWarning("No ground detected while adjusting distance.");
            }
        }
    }

    // Connects the component to a valid slot.
    public void Connect(GameObject slot)
    {
        transform.parent = slot.transform; // Attach the component to the slot.
        transform.localPosition = Vector3.zero; // Reset position relative to the slot.
        transform.localRotation = Quaternion.identity; // Reset rotation.
        m_PickedUp = false; // Mark the component as not picked up.
    }

    // Detects when the component enters a valid slot's trigger area.
    void OnTriggerEnter(Collider collide)
    {
        if (collide.TryGetComponent(out S_PcComponentHolder holder) && holder.G_Component == m_Component)
        {
            m_CurrentHolder = holder; // Assign the holder.
            Debug.Log($"Component is near a valid holder: {holder.name}");
        }
    }

    // Detects when the component exits a valid slot's trigger area.
    void OnTriggerExit(Collider collide)
    {
        if (collide.TryGetComponent(out S_PcComponentHolder holder) && holder == m_CurrentHolder)
        {
            m_CurrentHolder = null; // Clear the holder reference.
            Debug.Log("Component left the holder.");
        }
    }
}
