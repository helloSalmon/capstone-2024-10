using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����Ϳ��� ���� �ҼӵǾ��ִ� �θ������Ʈ�� ���� ���ü����� ���� ���̴�.
[SelectionBase]
public class BaseWall : MonoBehaviour
{
    //�μ��� �� �ִ� ���� �ٸ�����Ʈ�� �ٽ� ���� ���� ����
    public bool isBreakable;
    public bool isOpenable;
    
    // Start is called before the first frame update
    void Start()
    {
        if(isBreakable)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.AddComponent<BreakWall>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
