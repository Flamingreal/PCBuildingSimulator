using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Start : MonoBehaviour
{
    void OnMouseDown()
    {
        // ���ʱ�������¼�
        Debug.Log($"{gameObject.name} was clicked!");

        string currentSceneName = SceneManager.GetActiveScene().name;
        // �������볡��
        SceneManager.LoadScene(currentSceneName);
    }
}
