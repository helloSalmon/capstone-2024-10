using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public IEnumerator Interact();
}

/// <summary>
/// ���ٵ���ó�� ���� �ð� �̻� �ð��� �ҿ��ؾ� �ϴ� �۾��� MonoBehaviour��� �� Ŭ������ ��ӹ޾� ���
/// </summary>
public class BaseWorking : MonoBehaviour
{
    public float requiredTime = 5.0f;
    protected float workingTime;

    protected float workingSpeed = 1.0f;

    protected bool isComplete = false;
    protected bool isContinuable = false;

    private Coroutine coroutine;

    private void Start()
    {
        workingTime = 0;
    }

    protected void CheckWorking()
    {
        if(coroutine == null)
        {
            coroutine = StartCoroutine(Working());
        }
        else
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    /// <summary>
    /// ���ٵ��� �۾� �ý��� �Լ� (�ʿ��� ���� ȣ���)
    /// </summary>
    /// <returns></returns>
    protected IEnumerator Working()
    {
        isComplete = false;
        
        if(!isContinuable) { workingTime = 0; }

        while (workingTime < requiredTime)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                StopAllCoroutines();
            }
            
            workingTime += Time.deltaTime * workingSpeed;
            yield return null;
        }

        isComplete = true;
           
    }
}
