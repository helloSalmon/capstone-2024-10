using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 180f;

    void Update()
    {
        // Ű �Է��� �޾� �̵� �� ȸ�� ������ ����մϴ�.
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // �� �� �̵� ������ ����մϴ�.
        Vector3 forwardMovement = transform.forward * verticalInput;

        // �� �� ȸ���� ����մϴ�.
        Vector3 rotation = new Vector3(0, horizontalInput * rotationSpeed * Time.deltaTime, 0);

        // �̵� ���Ϳ� �̵� �ӵ��� ���Ͽ� ��ü�� �̵���ŵ�ϴ�.
        transform.Translate(forwardMovement * moveSpeed * Time.deltaTime, Space.World);

        // ȸ�� ���Ϳ� ȸ�� �ӵ��� ���Ͽ� ��ü�� ȸ����ŵ�ϴ�.
        transform.Rotate(rotation);
    }
}