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
    public float workingTime;

    protected float workingSpeed = 1.0f;

    public bool isComplete = false;
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
    public IEnumerator Working()
    {
        Managers.UI.ShowPopupUI<UI_WorkingBar>("ShowLongWork");
        UI_WorkingBar ui = Managers.UI.PeekPopupUI<UI_WorkingBar>();
        yield return new WaitUntil(() => ui.Init());
        
        isComplete = false;

        MapManager.baseSystem.isInteracting = true;
        
        if(!isContinuable) { workingTime = 0; }

        while (workingTime < requiredTime)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                MapManager.baseSystem.isInteracting = false;
                Managers.UI.ClosePopupUI();
                StopAllCoroutines();
            }
            
            workingTime += Time.deltaTime * workingSpeed;
            ui.CalculateBar(this);
            yield return null;
        }

        isComplete = true;

        MapManager.baseSystem.isInteracting = false;
        Managers.UI.ClosePopupUI();           
    }
}
