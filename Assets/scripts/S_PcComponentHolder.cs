using UnityEngine;

// This script defines the behavior of a PC component slot (holder).
// It handles the interaction and validation of components placed in the slot.
[RequireComponent(typeof(BoxCollider))]
public class S_PcComponentHolder : MonoBehaviour
{
    [Header("Component Configuration")]
    [Tooltip("Select the component type this slot accepts")]
    [SerializeField]
    private e_Components m_Component; // The type of component this slot accepts.

    [Tooltip("The highlight model to indicate correct placement")]
    [SerializeField]
    private GameObject highlightModel; // Highlight model for the slot (used for visual feedback).

    private bool isOccupied = false; // Tracks if the slot is currently occupied.

    // Getter for the component type this slot accepts.
    public e_Components G_Component
    {
        get { return m_Component; }
    }

    // Initialization function.
    void Start()
    {
        // If the highlight model is not assigned, attempt to find it as a child object.
        if (highlightModel == null)
        {
            highlightModel = transform.Find("HighlightBox")?.gameObject;
        }

        // If a highlight model exists, disable its renderer by default.
        if (highlightModel != null)
        {
            highlightModel.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            Debug.LogWarning("Highlight model reference is missing!"); // Warning if no highlight model is found.
        }
    }

    // Checks if the slot is available for placing a correct component.
    public bool IsCorrectComponentNearby()
    {
        return !isOccupied;
    }

    // Sets the occupied state of the slot and updates the highlight visibility accordingly.
    public void SetOccupied(bool occupied)
    {
        isOccupied = occupied;

        // Show or hide the highlight model based on the occupied state.
        if (highlightModel != null)
        {
            highlightModel.GetComponent<MeshRenderer>().enabled = !occupied;
        }
    }

    // Triggered when another collider enters this slot's trigger area.
    void OnTriggerEnter(Collider other)
    {
        // If the slot is unoccupied and the incoming object is the correct type of component.
        if (!isOccupied && other.TryGetComponent(out S_PcComponent component) && component.GetComponentType() == m_Component)
        {
            // Enable the highlight model to indicate the component can be placed.
            if (highlightModel != null)
            {
                highlightModel.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }

    // Triggered when another collider exits this slot's trigger area.
    void OnTriggerExit(Collider other)
    {
        // If the exiting object matches the component type and the slot is still unoccupied.
        if (!isOccupied && other.TryGetComponent(out S_PcComponent component) && component.GetComponentType() == m_Component)
        {
            // Disable the highlight model to indicate the component is no longer nearby.
            if (highlightModel != null)
            {
                highlightModel.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
