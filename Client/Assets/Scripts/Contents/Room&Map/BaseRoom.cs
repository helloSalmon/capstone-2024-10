using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class BaseRoom : MonoBehaviour
{
    //���� ���� ������Ʈ���� ������ ������ �ν����Ϳ��� ���� ����
    public GameObject itsLight;

    private RoomLight roomLight;

    //���� ���� 
    [HideInInspector]
    public bool isLightPower;

    private void Start()
    {
        roomLight = itsLight.GetComponent<RoomLight>();

        SetRoom();
    }

    private void SetRoom()
    {

    }

    public void SwitchLight()
    {
        if(isLightPower)
        {
            if (!roomLight.light.enabled) { roomLight.light.enabled = true; }
            else if (roomLight.light.enabled) { roomLight.light.enabled = false; }
        }
    }
}
