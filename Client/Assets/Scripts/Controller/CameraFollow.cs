using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player; // �÷��̾��� Transform�� ������ ����
    public float rotationSpeed = 1.0f; // ī�޶� ȸ�� �ӵ�
    private float currentAngle = 180f; // ���� ����

    public float initialXRotation = 10.0f; // �ʱ� x�� ȸ����


    void Start()
    {
        // �ʱ� ī�޶� ��ġ ����
        UpdateCameraPosition();
    }
    void Update()
    {
        // ���콺 �Է��� �޾ƿ� ȸ�� ���� ���
        float mouseX = Input.GetAxis("Mouse X");
        currentAngle += mouseX * rotationSpeed;

        // ������ 0���� 360�� ���̷� ����
        currentAngle = Mathf.Repeat(currentAngle, 360f);

        // ī�޶� ��ġ ����
        UpdateCameraPosition();

    }

    void UpdateCameraPosition()
    {
        // ���� �˵� ���� ��ġ ���
        float radianAngle = Mathf.Deg2Rad * currentAngle;
        float distance = 2.5f; // ���� ������ ���� (���� ����)
        Vector3 cameraPosition = new Vector3(Mathf.Sin(radianAngle) * distance, 1.7f, Mathf.Cos(radianAngle) * distance);

        // �÷��̾ �߽����� �ϴ� ���� �˵��� ���� ī�޶� �̵�
        transform.position = player.position + cameraPosition;

        // �÷��̾ �ٶ󺸴� ���� ���� ����
        Vector3 lookDirection = (player.position + Vector3.up * 1.7f) - transform.position;

        // ���� ���͸� ȸ�� ������ ��ȯ
        Quaternion rotation = Quaternion.LookRotation(lookDirection);

        // �ʱ� x�� ȸ���� ����
        rotation *= Quaternion.Euler(initialXRotation, 0, 0);

        // ī�޶� ȸ�� ����
        transform.rotation = rotation;
    }

}
