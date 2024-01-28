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
    
    public BaseRoom inChargeRoom;
    public BaseDoor inChargeDoor;
}
