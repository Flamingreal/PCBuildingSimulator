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
        // 点击时触发的事件
        djcs += 1;
        sz.text ="点击次数："+djcs;
    }
}
