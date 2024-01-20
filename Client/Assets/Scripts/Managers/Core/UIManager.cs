using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>
/// UI �Ŵ����� UI�� ��� ���������� ���� ��å�� ������ �ִ� �Ŵ���
/// </summary>
public class UIManager
{
    /// <summary>
    /// UI Canvas�� ��ġ�Ǵ� ����
    /// </summary>
    int _order = 10;

    /// <summary>
    /// UI_Popup�� �����ϴ� ����
    /// </summary>
    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    /// <summary>
    /// �� UI�� ��� �ִ� ������Ƽ, ���� ���� �Ұ����ϴ�.
    /// </summary>
    public UI_Scene SceneUI { get; private set; }

    /// <summary>
    /// ��� UI�� �θ� �Ǵ� ���� ������Ʈ
    /// </summary>
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    /// <summary>
    /// UI �����տ� ĵ������ �޾��ְ� �ʱ� ������ ���ش�. 
    /// </summary>
    /// <param name="go">UI ������</param>
    /// <param name="sort">���� ������ ������ �޴��� ����</param>
    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    /// <summary>
    /// WorldSpace ������ �� UI�� ȣ���ϴµ� ����ϴ� �Լ�
    /// WorldSpace�� ���忡 �μӵ� UI�� �ǹ��Ѵ�.
    /// </summary>
    /// <typeparam name="T">UI_Base�� ����� ����� ���� UI</typeparam>
    /// <param name="parent">��� UI�� ������</param>
    /// <param name="name">�̸��� �������ϸ� Ŭ���� �̸����� ������ �ҷ��´�.</param>
    public T MakeWorldSpaceUI<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/WorldSpace/{name}");

        if (parent != null)
            go.transform.SetParent(parent);

        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        return Util.GetOrAddComponent<T>(go);
    }

    /// <summary>
    /// SubItem ������ �� UI�� ȣ���ϴµ� ����ϴ� �Լ�
    /// SubItem�� �κ��丮�� ���� �������� �ǹ��Ѵ�.
    /// </summary>
    /// <typeparam name="T">UI_Base�� ����� ����� ���� UI</typeparam>
    /// <param name="parent">��� UI�� ������</param>
    /// <param name="name">�̸��� �������ϸ� Ŭ���� �̸����� ������ �ҷ��´�.</param>
    public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/SubItem/{name}");

        if (parent != null)
            go.transform.SetParent(parent);

        return Util.GetOrAddComponent<T>(go);
    }

    /// <summary>
    /// SceneUI�� ȣ���ϴµ� ����ϴ� �Լ�
    /// SceneUI�� ������ �����ڸ��� ���̴� ����ȭ���̴�.
    /// </summary>
    /// <typeparam name="T">UI_Scene�� ����� ����� ���� UI</typeparam>
    /// <param name="name">�̸��� �������ϸ� Ŭ���� �̸����� ������ �ҷ��´�.</param>
    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");
        T sceneUI = Util.GetOrAddComponent<T>(go);
        SceneUI = sceneUI;

        go.transform.SetParent(Root.transform);

        return sceneUI;
    }

    /// <summary>
    /// PopupUI�� ȣ���ϴµ� ����ϴ� �Լ�
    /// PopupUI�� UI ���� ���������� ���̴� ������ UI�� �ǹ��Ѵ�.
    /// parent�� ������� PopupUI�� �ϳ��� �������� ���� ���� �����Ѵ�.
    /// </summary>
    /// <typeparam name="T">UI_Popup�� ����� ����� ���� UI</typeparam>
    /// <param name="parent">��� UI�� ������</param>
    /// <param name="name">�̸��� �������ϸ� Ŭ���� �̸����� ������ �ҷ��´�.</param>
    public T ShowPopupUI<T>(string name = null, Transform parent = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject prefab = Managers.Resource.Load<GameObject>($"Prefabs/UI/Popup/{name}");
        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");
        T popup = Util.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);

        if (parent != null)
            go.transform.SetParent(parent);
        else if (SceneUI != null)
            go.transform.SetParent(SceneUI.transform);
        else
            go.transform.SetParent(Root.transform);

        go.transform.localScale = Vector3.one;
        go.transform.localPosition = prefab.transform.position;

        return popup;
    }

    /// <summary>
    /// UI_Popup�� ����� PopupUI�� ���� ���� �ִ� �ͺ��� ���ʴ�� �˻��ؼ� ��ġ�ϴ� ���� ã�´�.
    /// </summary>
    /// <typeparam name="T">ã�� ����� ���� PopupUI Ÿ��</typeparam>
    public T FindPopup<T>() where T : UI_Popup
    {
        return _popupStack.Where(x => x.GetType() == typeof(T)).FirstOrDefault() as T;
    }

    /// <summary>
    /// PopupUI ������ ���� ���� ���ϴ� PopupUI�� �ִ����� Ȯ���غ���.
    /// ���ϴ� PopupUI�� �ƴϸ� null�� ��ȯ�Ѵ�.
    /// </summary>
    /// <typeparam name="T">���ϴ� PopupUI</typeparam>
    public T PeekPopupUI<T>() where T : UI_Popup
    {
        if (_popupStack.Count == 0)
            return null;

        return _popupStack.Peek() as T;
    }

    /// <summary>
    /// PopupUI ������ ���� ���� ������ PopupUI�� ������ �� �˾��� �ݴ´�.
    /// </summary>
    /// <param name="popup">���ϴ� PopupUI</param>
    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if (_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed");
            return;
        }

        ClosePopupUI();
    }

    /// <summary>
    /// ���� ���� �ִ� �˾� UI�� �ݴ´�.
    /// </summary>
    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        UI_Popup popup = _popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;
        _order--;
    }

    /// <summary>
    /// ��� �˾� UI�� �ݴ´�.
    /// </summary>
    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }

    /// <summary>
    /// ��� �˾� UI�� �� Ui�� �ݴ� �ʱ�ȭ�� �����Ѵ�.
    /// </summary>
    public void Clear()
    {
        CloseAllPopupUI();
        SceneUI = null;
    }
}
