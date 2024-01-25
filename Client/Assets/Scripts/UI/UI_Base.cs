using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ��� UI�� ���̽��� �Ǵ� Ŭ����
/// �� UI, �˾� UI�� �ش� UI�� ����ؼ� �����Ǿ� �ִ�.
/// UI ��Ҹ� ������ �����ϴ� ������Ʈ ���ε� ��ɰ� 
/// UI ��Ҹ� �Է� �̺�Ʈ�� �����ϴ� �Լ� ���ε� ����� �����ϰ� �ִ�.
/// </summary>
public abstract class UI_Base : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    protected bool _init = false;

    public virtual bool Init()
    {
        if (_init)
            return false;

        return _init = true;
    }

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// enum Ÿ���� �̸����� ��Ʈ������ ��ȯ�� UI ��ҿ� �����Ѵ�.
    /// </summary>
    /// <typeparam name="T">UI ���</typeparam>
    /// <param name="type">enum Ÿ��</param>
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.FindChild(gameObject, names[i], true);
            else
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);

            if (objects[i] == null)
                Debug.Log($"Failed to bind({names[i]})");
        }
    }

    /// <summary>
    /// ��ϵ� enum Ÿ�� �߿��� �ش��ϴ� �ε����� ���� UI ��Ҹ� �����´�.
    /// �ε����� enum Ÿ�� �տ� (int)�� ���̸� ���� �� �ִ�.
    /// GetObject, GetButton, GetText, GetImage �ø��� �Լ��� ����ϴ� �� ��õ�Ѵ�.
    /// </summary>
    /// <typeparam name="T">UI ���</typeparam>
    /// <param name="idx">enum Ÿ�� �ε���</param>
    /// <returns></returns>
    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;

        return objects[idx] as T;
    }

    /// <summary>
    /// Get<GameObject> �Լ��� ������ �Լ�
    /// </summary>
    /// <param name="idx">enum Ÿ�� �ε���</param>
    /// <returns></returns>
    protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }
    /// <summary>
    /// Get<TMP_Text> �Լ��� ������ �Լ�
    /// </summary>
    /// <param name="idx">enum Ÿ�� �ε���</param>
    /// <returns></returns>
    protected TMP_Text GetText(int idx) { return Get<TMP_Text>(idx); }
    /// <summary>
    /// Get<Button> �Լ��� ������ �Լ�
    /// </summary>
    /// <param name="idx">enum Ÿ�� �ε���</param>
    /// <returns></returns>
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    /// <summary>
    /// Get<Image> �Լ��� ������ �Լ�
    /// </summary>
    /// <param name="idx">enum Ÿ�� �ε���</param>
    /// <returns></returns>
    protected Image GetImage(int idx) { return Get<Image>(idx); }

    /// <summary>
    /// UI ��ҿ� UI_EventHanlder�� ������ Event�� �����ϰ� ����� �Ķ���ͷ� �־��� �̺�Ʈ�� ����Ѵ�.
    /// </summary>
    /// <param name="go">UI ���</param>
    /// <param name="action">�̺�Ʈ �Լ�</param>
    /// <param name="type">������ �̺�Ʈ ����</param>
    public static void BindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

        switch (type)
        {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            case Define.UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
            case Define.UIEvent.Down:
                evt.OnDownHandler -= action;
                evt.OnDownHandler += action;
                break;
            case Define.UIEvent.Up:
                evt.OnUpHandler -= action;
                evt.OnUpHandler += action;
                break;
        }
    }
}
