using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �����ǿ� ���̴� Ŭ������
/// </summary>
public class ElectricSystem : BaseWorking, IInteractable
{    
    public BaseSystem baseSystem;

    private void Start()
    {
        
    }

    public IEnumerator Interact()
    {
        yield return null;

        //���½ý��� ���� �ڵ�
        Managers.UI.ShowPopupUI<UI_ElectricPanel>("Electric_Control");
        Debug.Log("dddd");
    }
}
