using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDoor : BaseWorking, IInteractable
{
    //�� ���� ����ġ�� �� ���� �ƴ� �� ���� ���ӵǾ� �ִ�.
    [HideInInspector]
    public List<DoorSwitch> switches;

    private Animator animator;

    [HideInInspector]
    public bool isLightPower;

    public bool isPassable
    {
        get => animator.GetBool("isPassable");
        set => animator.SetBool("isPassable", value);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        for(int i = 2; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).TryGetComponent(out DoorSwitch doorSwitch))
            {
                doorSwitch.door = this;
                switches.Add(doorSwitch);
            }
        }
    }

    public IEnumerator Interact()
    {
        if (isLightPower) { isPassable = !isPassable; }
        else
        {
            //���Ⱑ ������ �� ���� �������� ���� �Լ�
            yield return StartCoroutine(Working());

            if(isComplete) { isPassable = !isPassable; }
        }
    }
}
