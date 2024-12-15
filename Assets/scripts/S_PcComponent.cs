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

    private bool m_PickedUp;

    public bool G_PickedUp
    {
        get { return m_PickedUp; }
    }

    public void PickUp()
    {
        if (!m_PickedUp)
        {
            m_PickedUp = true;
            transform.parent = Camera.main.transform; // 将组件附加到相机
            transform.localPosition = new Vector3(0, 0, 2); // 将物体放置在玩家前方
            transform.localRotation = Quaternion.identity; // 重置旋转
            GetComponent<Rigidbody>().isKinematic = true; // 禁用物理模拟
            UnityEngine.Debug.Log($"{name} picked up!");
        }
    }

    public void Drop()
    {
        if (m_PickedUp)
        {
            m_PickedUp = false;
            transform.parent = null; // 解除父子关系
            GetComponent<Rigidbody>().isKinematic = false; // 恢复物理模拟
            UnityEngine.Debug.Log($"{name} dropped!");
        }
    }
}
