using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    void OnMouseDown()
    {
        // ���ʱ�������¼�
        Debug.Log($"{gameObject.name} was clicked!");
        Application.Quit();
    }
}
