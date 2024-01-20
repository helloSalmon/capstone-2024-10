using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� ���� SceneUI �����Ϸ��� �ش� Ŭ������ ����ϸ� �ȴ�.
/// </summary>
public class UI_Scene : UI_Base
{
    /// <summary>
    /// �ڽĿ��� Init�� �����Ϸ��� �� ��, �ش� Init�� ȣ������� �Ѵ�.
    /// </summary>
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Managers.UI.SetCanvas(gameObject, false);
        return true;
    }
}
