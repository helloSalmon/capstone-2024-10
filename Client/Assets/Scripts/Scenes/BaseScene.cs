using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ��� ���� �ϳ����� �־�� �ϴ� �� ��ũ��Ʈ�� ����� ���� ��� ����.
/// �ش� Ŭ������ ����ؼ� ���� �� ��ũ��Ʈ�� �ݵ�� ������ �ϳ���
/// @Scene ������Ʈ ���Ͽ� �پ� �־�� �Ѵ�.
/// </summary>
public abstract class BaseScene : MonoBehaviour
{
    /// <summary>
    /// enum���� ���ǵ� �� Ÿ���� ������ �� �ִ�.
    /// </summary>
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;

    void Awake()
    {
        init();
    }

    /// <summary>
    /// EventSystem�� ������ @EventSystem���� �����Ѵ�.
    /// ���� ����ϰ� �����ϱ� ���ؼ� ������ EventSystem�� ������ȭ ���״�.
    /// �� ��ũ��Ʈ�� ������ �� ���ο� Init�� �����Ѵٸ� �Լ� ���ο��� ȣ������� �Ѵ�.
    /// </summary>
    protected virtual void init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
    }

    /// <summary>
    /// �� �� ��ũ��Ʈ�� �����ؾ� �� �Լ�
    /// </summary>
    public abstract void Clear();
}
