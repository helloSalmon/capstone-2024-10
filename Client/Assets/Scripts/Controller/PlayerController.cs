using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Fusion;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    float currentSpeed = 0;    //���� �ӵ�

    float sitSpeed = 0;    //�ɴ� �ӵ�
    float sit_walkSpeed = 0;    //�ɾƼ� �ȴ� �ӵ�

    float rotationSpeed = 5.0f;  //�� ȸ�� �ӵ�
    public float currentHealth = 100;  //���� ü��

    float hAxis;
    float vAxis;

    private bool isRunning = false;
    private bool isSitting = false;
    private bool isDeath = false;
    //private bool isCasting = false;
    //private bool isPicking = false;


    private Animator animator;
    private CharacterController _controller;
    public Camera Camera;

    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            Camera = Camera.main;
            Camera.GetComponent<CameraFollow>().player = transform;
            animator = GetComponent<Animator>();
            _controller = GetComponent<CharacterController>();
        }
    }
    void Update()
    {
        GetInput();
    }

    public override void FixedUpdateNetwork()
    {
        Network_Move();
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
    
    void Network_Move()
    {
        if (HasStateAuthority == false)
        {
            return;
        }

        if (currentHealth == 100)
        {
            animator.SetFloat("Health", currentHealth);
            if (isSitting)
            {
                float sit_smoothness = 5f; // ���� ������ �ε巯�� ���
                sitSpeed = Mathf.Lerp(sitSpeed, 1, Runner.DeltaTime * sit_smoothness);
                animator.SetFloat("Sit", sitSpeed);
                if (hAxis == 0 && vAxis == 0)
                {
                    float smoothness = 5f; // ���� ������ �ε巯�� ���
                    sit_walkSpeed = Mathf.Lerp(sit_walkSpeed, 0, Runner.DeltaTime * smoothness);
                    animator.SetFloat("sitSpeed", sit_walkSpeed);
                }
                else
                {
                    Quaternion cameraRotationY = Quaternion.Euler(0, Camera.transform.rotation.eulerAngles.y, 0);
                    Vector3 move = cameraRotationY * new Vector3(hAxis, 0, vAxis) * Runner.DeltaTime * sit_walkSpeed;

                    _controller.Move(move);

                    if (move != Vector3.zero)
                    {
                        gameObject.transform.forward = move;
                    }
                    float smoothness = 5f; // ���� ������ �ε巯�� ���
                    sit_walkSpeed = Mathf.Lerp(sit_walkSpeed, 1, Runner.DeltaTime * smoothness);
                    animator.SetFloat("sitSpeed", sit_walkSpeed);
                }
            }
            else
            {
                float sit_smoothness = 5f; // ���� ������ �ε巯�� ���
                sitSpeed = Mathf.Lerp(sitSpeed, 0, Runner.DeltaTime * sit_smoothness);
                animator.SetFloat("Sit", sitSpeed);
                if (hAxis == 0 && vAxis == 0 && isRunning == false)
                {
                    float smoothness = 5f; // ���� ������ �ε巯�� ���
                    currentSpeed = Mathf.Lerp(currentSpeed, 0, Runner.DeltaTime * smoothness);
                    animator.SetFloat("moveSpeed", currentSpeed);
                }
                else if (isRunning == false)
                {
                    Quaternion cameraRotationY = Quaternion.Euler(0, Camera.transform.rotation.eulerAngles.y, 0);
                    Vector3 move = cameraRotationY * new Vector3(hAxis, 0, vAxis) * Runner.DeltaTime * currentSpeed;

                    _controller.Move(move);

                    if (move != Vector3.zero)
                    {
                        gameObject.transform.forward = move;
                    }
                    float smoothness = 2f; // ���� ������ �ε巯�� ���
                    currentSpeed = Mathf.Lerp(currentSpeed, 2, Runner.DeltaTime * smoothness);
                    animator.SetFloat("moveSpeed", currentSpeed);
                }
                else
                {
                    Quaternion cameraRotationY = Quaternion.Euler(0, Camera.transform.rotation.eulerAngles.y, 0);
                    Vector3 move = cameraRotationY * new Vector3(hAxis, 0, vAxis) * Runner.DeltaTime * currentSpeed;

                    _controller.Move(move);

                    if (move != Vector3.zero)
                    {
                        gameObject.transform.forward = move;
                    }
                    float smoothness = 4f; // ���� ������ �ε巯�� ���
                    currentSpeed = Mathf.Lerp(currentSpeed, 3.5f, Runner.DeltaTime * smoothness);
                    animator.SetFloat("moveSpeed", currentSpeed);
                }
            }
        }
        else if (currentHealth == 50)
        {
            animator.SetFloat("Health", currentHealth);
            if (isSitting)
            {
                float sit_smoothness = 5f; // ���� ������ �ε巯�� ���
                sitSpeed = Mathf.Lerp(sitSpeed, 1, Runner.DeltaTime * sit_smoothness);
                animator.SetFloat("Sit", sitSpeed);
                if (hAxis == 0 && vAxis == 0)
                {
                    float smoothness = 5f; // ���� ������ �ε巯�� ���
                    sit_walkSpeed = Mathf.Lerp(sit_walkSpeed, 0, Runner.DeltaTime * smoothness);
                    animator.SetFloat("sitSpeed", sit_walkSpeed);
                }
                else
                {
                    Quaternion cameraRotationY = Quaternion.Euler(0, Camera.transform.rotation.eulerAngles.y, 0);
                    Vector3 move = cameraRotationY * new Vector3(hAxis, 0, vAxis) * Runner.DeltaTime * sit_walkSpeed;

                    _controller.Move(move);

                    if (move != Vector3.zero)
                    {
                        gameObject.transform.forward = move;
                    }
                    float smoothness = 5f; // ���� ������ �ε巯�� ���
                    sit_walkSpeed = Mathf.Lerp(sit_walkSpeed, 1, Runner.DeltaTime * smoothness);
                    animator.SetFloat("sitSpeed", sit_walkSpeed);
                }
            }
            else
            {
                float sit_smoothness = 5f; // ���� ������ �ε巯�� ���
                sitSpeed = Mathf.Lerp(sitSpeed, 0, Runner.DeltaTime * sit_smoothness);
                animator.SetFloat("Sit", sitSpeed);
                if (hAxis == 0 && vAxis == 0 && isRunning == false)
                {
                    float smoothness = 5f; // ���� ������ �ε巯�� ���
                    currentSpeed = Mathf.Lerp(currentSpeed, 0, Runner.DeltaTime * smoothness);
                    animator.SetFloat("moveSpeed", currentSpeed);
                }
                else if (isRunning == false)
                {
                    Quaternion cameraRotationY = Quaternion.Euler(0, Camera.transform.rotation.eulerAngles.y, 0);
                    Vector3 move = cameraRotationY * new Vector3(hAxis, 0, vAxis) * Runner.DeltaTime * currentSpeed;

                    _controller.Move(move);

                    if (move != Vector3.zero)
                    {
                        gameObject.transform.forward = move;
                    }
                    float smoothness = 2f; // ���� ������ �ε巯�� ���
                    currentSpeed = Mathf.Lerp(currentSpeed, 1.5f, Runner.DeltaTime * smoothness);
                    animator.SetFloat("moveSpeed", currentSpeed);
                }
                else
                {
                    Quaternion cameraRotationY = Quaternion.Euler(0, Camera.transform.rotation.eulerAngles.y, 0);
                    Vector3 move = cameraRotationY * new Vector3(hAxis, 0, vAxis) * Runner.DeltaTime * currentSpeed;

                    _controller.Move(move);

                    if (move != Vector3.zero)
                    {
                        gameObject.transform.forward = move;
                    }
                    float smoothness = 4f; // ���� ������ �ε巯�� ���
                    currentSpeed = Mathf.Lerp(currentSpeed, 2.5f, Runner.DeltaTime * smoothness);
                    animator.SetFloat("moveSpeed", currentSpeed);
                }
            }
        }
        else
        {
            isDeath = true;
            animator.SetBool("IsDeath", true);
        }
    }

}
