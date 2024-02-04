using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricsystemButton : MonoBehaviour
{
    public enum ButtonType
    {
        room,
        door,
    }
    public ButtonType buttonType;  //�ν�����â���� ���� ��������� ��
    
    [HideInInspector]
    public BaseRoom inChargeRoom;
    [HideInInspector]
    public BaseDoor inChargeDoor;

    /// <summary>
    /// �ݵ�� ����Ǿ��ִ� �� ������Ʈ�� �ν����� �� �̸��� ��Ȯ�� ��ġ�ؾ� ��
    /// </summary>
    public string segmentName;

    [HideInInspector]
    public bool isOn
    {
        get
        {
            if (inChargeRoom) { return inChargeRoom.isLightPower; }
            else { return inChargeDoor.isLightPower; }
        }
    }

    public void ChangeIsOn()
    {
        switch(buttonType)
        {
            case ButtonType.room:
                inChargeRoom.isLightPower = !inChargeRoom.isLightPower;
                break;

            case ButtonType.door:
                inChargeDoor.isLightPower = !inChargeDoor.isLightPower;
                break;
        }
    }
}
