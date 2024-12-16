using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class S_PcComponentHolder : MonoBehaviour
{
    [Header("Component Selecting")]
    [Tooltip("Select the component type this slot accepts")]
    [SerializeField]
    private e_Components m_Component;

    [SerializeField]
    private GameObject highlightModel; // ��۸���ģ��

    private bool isOccupied = false;

    public e_Components G_Component
    {
        get { return m_Component; }
    }

    void Start()
    {
        if (highlightModel == null)
        {
            highlightModel = transform.Find("HighlightBox")?.gameObject; // �Զ������Ӷ���
        }

        if (highlightModel != null)
        {
            highlightModel.GetComponent<MeshRenderer>().enabled = false; // Ĭ������
        }
        else
        {
            Debug.LogWarning("Highlight model reference is missing!");
        }
    }

    public bool IsCorrectComponentNearby()
    {
        return !isOccupied;
    }

    public void SetOccupied(bool occupied)
    {
        isOccupied = occupied;
        if (highlightModel != null)
        {
            highlightModel.GetComponent<MeshRenderer>().enabled = !occupied;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isOccupied && other.TryGetComponent(out S_PcComponent component) && component.GetComponentType() == m_Component)
        {
            if (highlightModel != null)
            {
                highlightModel.GetComponent<MeshRenderer>().enabled = true; // ��ʾ����
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!isOccupied && other.TryGetComponent(out S_PcComponent component) && component.GetComponentType() == m_Component)
        {
            if (highlightModel != null)
            {
                highlightModel.GetComponent<MeshRenderer>().enabled = false; // ���ظ���
            }
        }
    }
}
