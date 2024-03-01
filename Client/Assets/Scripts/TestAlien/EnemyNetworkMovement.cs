using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNetworkMovement : NetworkBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator anim;
    int IdleOne;
    int IdleAlert;
    int Sleeps;
    int AngryReaction;
    int Hit;
    int AnkleBite;
    int CrochBite;
    int Dies;
    int HushLittleBaby;
    int Run;

    int RandomWanderRange = 50;
    float detectionRange = 25f; // �÷��̾� ���� ����

    public override void Spawned()
    {
        anim = GetComponent<Animator>();
        IdleOne = Animator.StringToHash("IdleOne");
        IdleAlert = Animator.StringToHash("IdleAlert");
        Sleeps = Animator.StringToHash("Sleeps");
        AngryReaction = Animator.StringToHash("AngryReaction");
        Hit = Animator.StringToHash("Hit");
        AnkleBite = Animator.StringToHash("AnkleBite");
        CrochBite = Animator.StringToHash("CrochBite");
        Dies = Animator.StringToHash("Dies");
        HushLittleBaby = Animator.StringToHash("HushLittleBaby");
        Hit = Animator.StringToHash("Hit");
        Run = Animator.StringToHash("Run");

        navMeshAgent = GetComponent<NavMeshAgent>();
        WanderRandomly();
    }

    public override void FixedUpdateNetwork()
    {
        if (HasStateAuthority == false)
        {
            return;
        }

        base.FixedUpdateNetwork();
        // �÷��̾� ���� �� ���� ���� �߰�
        DetectAndChasePlayer();

        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ�� ����
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Ŭ���� ��ġ�� NavMeshAgent �̵� ������ ����
                navMeshAgent.SetDestination(hit.point);
            }
        }

        // �������� �����ϸ� ���ο� ���� ��ġ�� �̵�
        else if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            WanderRandomly();
        }

        // ���� ��ȸ �߿� Run �ִϸ��̼� ����
        if (navMeshAgent.velocity.magnitude > 0.1f)
        {
            anim.SetBool(Run, true);
        }
        else
        {
            anim.SetBool(Run, false);
        }
    }

    void WanderRandomly()
    {
        // ������ ��ġ�� �����ϰ� �� ��ġ�� �̵�
        Vector3 randomPosition = GetRandomPosition();
        navMeshAgent.SetDestination(randomPosition);
    }

    void DetectAndChasePlayer()
    {
        // �ֺ��� �÷��̾ �ִ��� ����
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                // �÷��̾ �����Ǹ� ���� ����
                ChasePlayer(hitCollider.transform.position);
                return;
            }
        }
    }

    void ChasePlayer(Vector3 playerPosition)
    {
        // �÷��̾ �����ϴ� �Լ� ȣ��
        navMeshAgent.SetDestination(playerPosition);
    }

    Vector3 GetRandomPosition()
    {
        // NavMesh�� �������� ������ ��ġ�� ����
        NavMeshHit hit;
        Vector3 randomPosition = transform.position + Random.insideUnitSphere * RandomWanderRange;//Ž�� ������
        NavMesh.SamplePosition(randomPosition, out hit, RandomWanderRange, NavMesh.AllAreas);//�ִ�õ�Ƚ��

        return hit.position;
    }
}
