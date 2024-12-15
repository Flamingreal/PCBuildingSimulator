using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public float reachDistance = 3f; // 玩家可交互的最大距离
    public Image crosshairImage; // 准星的 UI 图像
    public Color defaultCrosshairColor = Color.white; // 默认准星颜色
    public Color interactableCrosshairColor = Color.green; // 可交互对象时准星颜色

    void Update()
    {
        UpdateCrosshair(); // 更新准星显示
        HandleInteraction(); // 处理交互逻辑
    }

    private void UpdateCrosshair()
    {
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
        if (Input.GetKeyDown(KeyCode.E)) // 按下 E 键进行交互
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, reachDistance))
            {
                if (hit.collider.TryGetComponent(out S_PcComponent component))
                {
                    component.PickUp(); // 调用 S_PcComponent 的拾取方法
                }
            }
        }
    }
}
