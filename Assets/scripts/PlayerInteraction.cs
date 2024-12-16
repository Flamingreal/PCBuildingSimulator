using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public float reachDistance = 5f; // 玩家可交互的最大距离，增加距离
    public Image crosshairImage; // 准星图像
    public Color defaultCrosshairColor = Color.white; // 默认准星颜色
    public Color interactableCrosshairColor = Color.green; // 可交互对象时准星颜色

    private S_PcComponent currentComponent;

    void Update()
    {
        UpdateCrosshair(); // 更新准星显示
        HandleInteraction(); // 处理交互逻辑
        HandleDistanceAdjustment(); // 调整物体距离
    }

    private void UpdateCrosshair()
    {
        if (Camera.main == null)
        {
            Debug.LogError("Main Camera not found! Ensure your camera is tagged as 'MainCamera'.");
            return;
        }

        if (crosshairImage == null)
        {
            Debug.LogWarning("Crosshair Image is not assigned in the Inspector.");
            return;
        }

        if (currentComponent != null && currentComponent.G_PickedUp)
        {
            crosshairImage.color = interactableCrosshairColor; // 拾取物体时显示绿色
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, reachDistance))
        {
            if (hit.collider.TryGetComponent(out S_PcComponent component))
            {
                crosshairImage.color = interactableCrosshairColor; // 可交互时显示绿色
            }
            else
            {
                crosshairImage.color = defaultCrosshairColor; // 非交互对象恢复为默认颜色
            }
        }
        else
        {
            crosshairImage.color = defaultCrosshairColor; // 无检测时恢复为默认颜色
        }
    }

    private void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E)) // 按下 E 键触发交互
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, reachDistance))
            {
                if (hit.collider.TryGetComponent(out S_PcComponent component))
                {
                    if (!component.G_PickedUp)
                    {
                        component.PickUp(); // 拾取物体
                        currentComponent = component;
                    }
                    else
                    {
                        component.Drop(); // 放下物体
                        currentComponent = null;
                    }
                }
            }
        }
    }

    private void HandleDistanceAdjustment()
    {
        if (currentComponent != null && currentComponent.G_PickedUp)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel"); // 获取鼠标滚轮输入
            if (scroll != 0)
            {
                currentComponent.AdjustDistance(scroll); // 调整距离
            }
        }
    }
}
