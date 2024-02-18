using UnityEngine;
using UnityEngine.AI;

public class PlayerMoveEx : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    int RandomWanderRange = 50;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        WanderRandomly();
    }

    void Update()
    {
        // �������� �����ϸ� ���ο� ���� ��ġ�� �̵�
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            WanderRandomly();
        }
    }

    void WanderRandomly()
    {
        // ������ ��ġ�� �����ϰ� �� ��ġ�� �̵�
        Vector3 randomPosition = GetRandomPosition();
        navMeshAgent.SetDestination(randomPosition);
    }

    Vector3 GetRandomPosition()
    {
        // NavMesh�� �������� ������ ��ġ�� ����
        NavMeshHit hit;
        Vector3 randomPosition = transform.position + Random.insideUnitSphere * RandomWanderRange;
        NavMesh.SamplePosition(randomPosition, out hit, RandomWanderRange, NavMesh.AllAreas);

        return hit.position;
    }
}