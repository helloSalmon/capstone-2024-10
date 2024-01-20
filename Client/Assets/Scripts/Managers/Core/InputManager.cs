using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ���� ���Ǵ� ������ ��ǲ�� �̺�Ʈ �ڵ鷯 ������� ȣ���ϰ� ���� �Ŵ���
/// </summary>
public class InputManager
{
    /// <summary>
    /// Ű���忡 �̺�Ʈ�� ������ ��, � ������ ������ �Լ��� ����Ѵ�.
    /// </summary>
    public Action KeyAction = null;
    /// <summary>
    /// ���콺�� �̺�Ʈ�� ������ ��, � ������ ������ �Լ��� ����Ѵ�.
    /// </summary>
    public Action<Define.MouseEvent> MouseAction = null;

    /// <summary>
    /// ���ο��� ���¸� �����ϱ� ���ؼ� ���� ���� �ܺο��� �����ϸ� �� �ȴ�.
    /// </summary>
    bool _pressed = false;
    /// <summary>
    /// ���ο��� ���¸� �����ϱ� ���ؼ� ���� ���� �ܺο��� �����ϸ� �� �ȴ�.
    /// </summary>
    float _pressedTime = 0;
    
    /// <summary>
    /// ������ ������ ��ȸ�ϸ鼭 ������ �̺�Ʈ�� �˸��� �Լ����� �θ���.
    /// �Ŵ����� ����ϴ� �Լ��� �Ϲ� ����� ��ũ��Ʈ���� ����ϸ� �� �ȴ�.
    /// </summary>
    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.anyKey && KeyAction != null)
            KeyAction.Invoke();

        if (MouseAction != null)
        {
            if (Input.GetMouseButton(0))
            {
                if (!_pressed)
                {
                    MouseAction.Invoke(Define.MouseEvent.PointerDown);
                    _pressedTime = Time.time;
                }
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            else
            {
                if (_pressed)
                {
                    if (Time.time < _pressedTime * 0.2f)
                        MouseAction.Invoke(Define.MouseEvent.Click);
                    MouseAction.Invoke(Define.MouseEvent.PointerUp);
                }
                _pressed = false;
                _pressedTime = 0;
            }
        }
    }

    /// <summary>
    /// ȭ���� �ʱ�ȭ�� �Ͼ �� �����͸� �ʱ�ȭ�ϴ� �Լ�
    /// </summary>
    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;
    }
}
