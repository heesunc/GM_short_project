using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;  // �÷��̾��� ��ġ�� ������ ����

    public float distance = 10f;  // ī�޶�� �÷��̾� ���� �Ÿ�
    public float height = 5f;  // ī�޶�� �÷��̾� ���� ����
    public float smoothSpeed = 0.5f;  // ī�޶� �̵� �� �ε巯�� ������ ���� ����

    private Vector3 velocity = Vector3.zero;  // ī�޶� �̵� �� ����� �ӵ� ����

    void LateUpdate()
    {
        // ī�޶��� ��ġ�� �ε巴�� �̵���Ű�� ���� Lerp �Լ� ���
        Vector3 targetPosition = target.position + Vector3.up * height - target.forward * distance;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);

        // ī�޶� �÷��̾ �ٶ󺸵��� ȸ����Ŵ
        transform.LookAt(target);
    }
}
