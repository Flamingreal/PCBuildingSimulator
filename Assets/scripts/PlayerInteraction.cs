using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public float reachDistance = 3f; // ��ҿɽ�����������
    public Image crosshairImage; // ׼�ǵ� UI ͼ��
    public Color defaultCrosshairColor = Color.white; // Ĭ��׼����ɫ
    public Color interactableCrosshairColor = Color.green; // �ɽ�������ʱ׼����ɫ

    void Update()
    {
        UpdateCrosshair(); // ����׼����ʾ
        HandleInteraction(); // �������߼�
    }

    private void UpdateCrosshair()
    {
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
        if (Input.GetKeyDown(KeyCode.E)) // ���� E �����н���
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, reachDistance))
            {
                if (hit.collider.TryGetComponent(out S_PcComponent component))
                {
                    component.PickUp(); // ���� S_PcComponent ��ʰȡ����
                }
            }
        }
    }
}
