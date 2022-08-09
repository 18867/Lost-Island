using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public ItemName requireItem;//��Ҫ������
    public bool isDone;//�Ƿ��Ѿ����

    public void CheckItem(ItemName itemName)//����Ƿ�����Ҫ������
    {
        if (itemName == requireItem && !isDone)//�������Ҫ�����壬���һ�û�����
        {
            isDone = true;//״̬����
            //ʹ�ò��Ƴ�
            OnClickedAction();//���֮��ľ�����������������
            EventHandler.CallItemUsedEvent(itemName);//�����¼�
        }
    }

    //��ȷ��Ʒ��ʱ��ִ��
    protected virtual void OnClickedAction()//��̬�������ʵ����������ж���
    {

    }

    public virtual void EmptyClicked()
    {
        Debug.Log("�յ�");
    }
}
