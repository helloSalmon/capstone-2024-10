using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float walkSpeed = 1f;   // �ȱ� �ӵ�
    float runSpeed = 3f;   // �޸��� �ӵ�
    float idleSpeed = 0f;   // �޸��� �ӵ�
    float currentSpeed = 0;    //���� �ӵ�
    float sitSpeed = 0;    //�ɴ� �ӵ�
    float sit_walkSpeed = 0;    //�ɾƼ� �ȴ� �ӵ�
    float rotationSpeed = 5.0f;  //�� ȸ�� �ӵ�


    float hAxis;
    float vAxis;
    Vector3 moveVec;
    Quaternion targetRotation;


    private bool isRunning = false;
    private bool isSitting = false;

    private Animator animator;
    private Rigidbody rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
        Move();
    }

    void GetInput() // Ű���� �� �ޱ�
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        if (Input.GetKeyDown(KeyCode.C))
        {
            isSitting = !isSitting;
        }
    }

    void Move()
    {
        
        if (isSitting)
        {
            float sit_smoothness = 5f; // ���� ������ �ε巯�� ���
            sitSpeed = Mathf.Lerp(sitSpeed, 1, Time.deltaTime * sit_smoothness);
            animator.SetFloat("Sit", sitSpeed);
            if (hAxis == 0 && vAxis == 0)
            {
                float smoothness = 5f; // ���� ������ �ε巯�� ���
                sit_walkSpeed = Mathf.Lerp(sit_walkSpeed, 0, Time.deltaTime * smoothness);
                animator.SetFloat("sitSpeed", sit_walkSpeed);
            }
            else
            {
                // ���콺 �Է� �ޱ�
                float mouseX = Input.GetAxis("Mouse X");
                // ī�޶� ȸ��
                transform.Rotate(Vector3.up, mouseX * rotationSpeed);
                // ���� ������ ���� �������� ��ȯ
                Vector3 inputDirection = new Vector3(hAxis, 0.0f, vAxis);
                Vector3 worldDirection = transform.TransformDirection(inputDirection);
                // �̵�
                transform.Translate(worldDirection * sit_walkSpeed * Time.deltaTime, Space.World);

                float smoothness = 5f; // ���� ������ �ε巯�� ���
                sit_walkSpeed = Mathf.Lerp(sit_walkSpeed, 1, Time.deltaTime * smoothness);
                animator.SetFloat("sitSpeed", sit_walkSpeed);

            }
            
        }
        else
        {
            float sit_smoothness = 5f; // ���� ������ �ε巯�� ���
            sitSpeed = Mathf.Lerp(sitSpeed, 0, Time.deltaTime * sit_smoothness);
            animator.SetFloat("Sit", sitSpeed);

            if (hAxis == 0 && vAxis == 0 && isRunning == false)
            {
                float smoothness = 5f; // ���� ������ �ε巯�� ���
                currentSpeed = Mathf.Lerp(currentSpeed, idleSpeed, Time.deltaTime * smoothness);
                animator.SetFloat("moveSpeed", currentSpeed);
            }
            else if (isRunning == false)
            {
                // ���콺 �Է� �ޱ�
                float mouseX = Input.GetAxis("Mouse X");

                // ī�޶� ȸ��
                transform.Rotate(Vector3.up, mouseX * rotationSpeed);
                // ���� ������ ���� �������� ��ȯ
                Vector3 inputDirection = new Vector3(hAxis, 0.0f, vAxis);
                Vector3 worldDirection = transform.TransformDirection(inputDirection);
                // �̵�
                transform.Translate(worldDirection * currentSpeed * Time.deltaTime, Space.World);

                float smoothness = 2f; // ���� ������ �ε巯�� ���
                currentSpeed = Mathf.Lerp(currentSpeed, walkSpeed, Time.deltaTime * smoothness);
                animator.SetFloat("moveSpeed", currentSpeed);


            }
            else
            {
                // ���콺 �Է� �ޱ�
                float mouseX = Input.GetAxis("Mouse X");
                // ī�޶� ȸ��
                transform.Rotate(Vector3.up, mouseX * rotationSpeed);
                // ���� ������ ���� �������� ��ȯ
                Vector3 inputDirection = new Vector3(hAxis, 0.0f, vAxis);
                Vector3 worldDirection = transform.TransformDirection(inputDirection);
                // �̵�
                transform.Translate(worldDirection * currentSpeed * Time.deltaTime, Space.World);

                float smoothness = 4f; // ���� ������ �ε巯�� ���
                currentSpeed = Mathf.Lerp(currentSpeed, runSpeed, Time.deltaTime * smoothness);
                animator.SetFloat("moveSpeed", currentSpeed);

            }
        }
        

    }
    
}
