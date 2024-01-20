using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// UI_Base���� �̺�Ʈ�� ����� �� ���Ǵ� Ŭ����
/// �̺�Ʈ�� �����Ͽ� �̺�Ʈ�� �߻��� ������ ��ϵ� �Լ��� ȣ���Ѵ�.
/// </summary>
public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    /// <summary>
    /// �巡�� �̺�Ʈ ����
    /// </summary>
    public Action<PointerEventData> OnDragHandler = null;
    /// <summary>
    /// Ŭ�� �̺�Ʈ ����
    /// </summary>
    public Action<PointerEventData> OnClickHandler = null;
    /// <summary>
    /// ���� �̺�Ʈ ����
    /// </summary>
    public Action<PointerEventData> OnDownHandler = null;
    /// <summary>
    /// ���� �̺�Ʈ ����
    /// </summary>
    public Action<PointerEventData> OnUpHandler = null;

    /// <summary>
    /// UI ��Ұ� �巡�׵� ������ �� �Լ��� ȣ��ȴ�.
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        // Debug.Log("OnDrag");
        if (OnDragHandler != null)
            OnDragHandler.Invoke(eventData);

    }

    /// <summary>
    /// UI ��Ұ� Ŭ���� ������ �� �Լ��� ȣ��ȴ�.
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        // Debug.Log("OnPointerClick");
        if (OnClickHandler != null)
        {
            OnClickHandler.Invoke(eventData);
        }
    }

    /// <summary>
    /// UI ��Ҹ� ������ �� �� �Լ��� ȣ��ȴ�.
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        // Debug.Log("OnPointerDown");
        if (OnDownHandler != null)
            OnDownHandler.Invoke(eventData);
    }

    /// <summary>
    /// UI ��ҿ��� ���� �� �� �Լ��� ȣ��ȴ�.
    /// </summary>
    public void OnPointerUp(PointerEventData eventData)
    {
        // Debug.Log("OnPointerUp");
        if (OnUpHandler != null)
            OnUpHandler.Invoke(eventData);
    }
}
