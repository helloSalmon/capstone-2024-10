using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeShip : MonoBehaviour, IInteractable
{
    //������ ��� ������ �������Ѿ� �Ѵ�
    public bool isFuel;
    
    public IEnumerator Interact()
    {
        yield return null;

        //������ ��� ������ �������Ѿ��Ѵ�.
        if(isFuel)
        {
            SuccessEscape();
        }
    }

    private void SuccessEscape()
    {

    }
}
