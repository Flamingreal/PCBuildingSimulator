using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cs : MonoBehaviour
{
    public Text sz;
    private int djcs;
     void OnMouseDown()
    {
        // ���ʱ�������¼�
        djcs += 1;
        sz.text ="���������"+djcs;
    }
}
