using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public float reachDistance = 5f; // ��ҿɽ����������룬���Ӿ���
    public Image crosshairImage; // ׼��ͼ��
    public Color defaultCrosshairColor = Color.white; // Ĭ��׼����ɫ
    public Color interactableCrosshairColor = Color.green; // �ɽ�������ʱ׼����ɫ

    private S_PcComponent currentComponent;

    void Update()
    {
        UpdateCrosshair(); // ����׼����ʾ
        HandleInteraction(); // �������߼�
        HandleDistanceAdjustment(); // �����������
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
            crosshairImage.color = interactableCrosshairColor; // ʰȡ����ʱ��ʾ��ɫ
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, reachDistance))
        {
            if (hit.collider.TryGetComponent(out S_PcComponent component))
            {
                crosshairImage.color = interactableCrosshairColor; // �ɽ���ʱ��ʾ��ɫ
            }
            else
            {
                crosshairImage.color = defaultCrosshairColor; // �ǽ�������ָ�ΪĬ����ɫ
            }
        }
        else
        {
            crosshairImage.color = defaultCrosshairColor; // �޼��ʱ�ָ�ΪĬ����ɫ
        }
    }

    private void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E)) // ���� E ����������
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, reachDistance))
            {
                if (hit.collider.TryGetComponent(out S_PcComponent component))
                {
                    if (!component.G_PickedUp)
                    {
                        component.PickUp(); // ʰȡ����
                        currentComponent = component;
                    }
                    else
                    {
                        component.Drop(); // ��������
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
            float scroll = Input.GetAxis("Mouse ScrollWheel"); // ��ȡ����������
            if (scroll != 0)
            {
                currentComponent.AdjustDistance(scroll); // ��������
            }
        }
    }
}
