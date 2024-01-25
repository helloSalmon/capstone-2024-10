using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float walkSpeed = 1f;   // �ȱ� �ӵ�
    float runSpeed = 3f;   // �޸��� �ӵ�
    float idleSpeed = 0f;   // �޸��� �ӵ�
    float currentSpeed = 0;    //���� �ӵ�
    public float rotationSpeed = 2.0f;  //�� ȸ�� �ӵ�

    public Camera mainCamera;

    float hAxis;
    float vAxis;
    Vector3 moveVec;
    Quaternion targetRotation;

    private bool isRunning = false;


    private Animator animator;
    private Rigidbody rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
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
    }

    void Move()
    {
        if (hAxis == 0 && vAxis == 0 && isRunning == false)
        {
            float smoothness = 5f; // ���� ������ �ε巯�� ���
            currentSpeed = Mathf.Lerp(currentSpeed, idleSpeed, Time.deltaTime * smoothness);
            animator.SetFloat("moveSpeed", currentSpeed);
        }
        else if (isRunning == false)
        {
            moveVec = new Vector3(hAxis, 0, vAxis).normalized;
            transform.position += moveVec * currentSpeed * Time.deltaTime;
            Turn();
            float smoothness = 2f; // ���� ������ �ε巯�� ���
            currentSpeed = Mathf.Lerp(currentSpeed, walkSpeed, Time.deltaTime * smoothness);
            animator.SetFloat("moveSpeed", currentSpeed);
        }
        else
        {
            moveVec = new Vector3(hAxis, 0, vAxis).normalized;
            transform.position += moveVec * currentSpeed * Time.deltaTime;
            Turn();
            float smoothness = 4f; // ���� ������ �ε巯�� ���
            currentSpeed = Mathf.Lerp(currentSpeed, runSpeed, Time.deltaTime * smoothness);
            animator.SetFloat("moveSpeed", currentSpeed);
        }

    }
    void Turn()
    {
        targetRotation = Quaternion.LookRotation(moveVec);
        // �ε巯�� ȸ��
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

    }
}
