using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Start : MonoBehaviour
{
    void OnMouseDown()
    {
        // 点击时触发的事件
        Debug.Log($"{gameObject.name} was clicked!");

        string currentSceneName = SceneManager.GetActiveScene().name;
        // 重新载入场景
        SceneManager.LoadScene(currentSceneName);
    }
}
