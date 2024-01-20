using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� ���� PopupUI �����Ϸ��� �ش� Ŭ������ ����ϸ� �ȴ�.
/// </summary>
public class UI_Popup : UI_Base
{
    /// <summary>
    /// �ڽĿ��� Init�� �����Ϸ��� �� ��, �ش� Init�� ȣ������� �Ѵ�.
    /// </summary>
    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        Managers.UI.SetCanvas(gameObject, true);
        return true;
    }

    /// <summary>
    /// �ش� PopupUi�� �ݴ´�.
    /// </summary>
    public virtual void ClosePopupUI()
    {
        Managers.UI.ClosePopupUI(this);
    }
}
