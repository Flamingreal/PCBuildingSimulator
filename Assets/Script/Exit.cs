using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    void OnMouseDown()
    {
        // 点击时触发的事件
        Debug.Log($"{gameObject.name} was clicked!");
        Application.Quit();
    }
}
