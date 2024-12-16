using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class S_PcComponent : MonoBehaviour
{
    [Header("Component Selecting")]
    [Tooltip("Select the component this has to be here")]
    [SerializeField]
    private e_Components m_Component;

    [Header("Movement Settings")]
    [SerializeField]
    private float m_MouseSensitivity = 100f; // 鼠标滚轮调整距离的灵敏度

    private S_PcComponentHolder m_CurrentHolder;

    [Space(5)]
    [Header("Debug Information:")]
    [SerializeField]
    private bool m_PickedUp; // 表示组件是否被拾取

    public bool G_PickedUp
    {
        get { return m_PickedUp; }
    }

    void Start()
    {
        m_CurrentHolder = null;
    }

    public e_Components GetComponentType()
    {
        return m_Component;
    }

    public void PickUp()
    {
        m_PickedUp = true;
        GetComponent<Rigidbody>().isKinematic = true; // 禁用物理效果
        Debug.Log($"{name} picked up!");
    }

    public void Drop()
    {
        if (m_CurrentHolder != null && m_CurrentHolder.IsCorrectComponentNearby())
        {
            Connect(m_CurrentHolder.gameObject);
            Debug.Log($"{name} successfully placed!");
        }
        else
        {
            m_PickedUp = false;
            GetComponent<Rigidbody>().isKinematic = false; // 启用物理效果
            Debug.LogWarning($"{name} dropped incorrectly.");
        }
    }

    public void AdjustDistance(float scroll)
    {
        float adjustedDistance = scroll * m_MouseSensitivity * Time.deltaTime; // 调整距离的灵敏度
        Vector3 newPosition = transform.localPosition + new Vector3(0, 0, adjustedDistance);
        transform.localPosition = newPosition;
    }

    public void Connect(GameObject slot)
    {
        transform.parent = slot.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        m_PickedUp = false;
    }

    void OnTriggerEnter(Collider collide)
    {
        if (collide.TryGetComponent(out S_PcComponentHolder holder) && holder.G_Component == m_Component)
        {
            m_CurrentHolder = holder;
            Debug.Log($"Component is near a valid holder: {holder.name}");
        }
    }

    void OnTriggerExit(Collider collide)
    {
        if (collide.TryGetComponent(out S_PcComponentHolder holder) && holder == m_CurrentHolder)
        {
            m_CurrentHolder = null;
            Debug.Log("Component left the holder.");
        }
    }
}
