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
            transform.parent = Camera.main.transform; // ��������ӵ����
            transform.localPosition = new Vector3(0, 0, 2); // ��������������ǰ��
            transform.localRotation = Quaternion.identity; // ������ת
            GetComponent<Rigidbody>().isKinematic = true; // ��������ģ��
            UnityEngine.Debug.Log($"{name} picked up!");
        }
    }

    public void Drop()
    {
        if (m_PickedUp)
        {
            m_PickedUp = false;
            transform.parent = null; // ������ӹ�ϵ
            GetComponent<Rigidbody>().isKinematic = false; // �ָ�����ģ��
            UnityEngine.Debug.Log($"{name} dropped!");
        }
    }
}
