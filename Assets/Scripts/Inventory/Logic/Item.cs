using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemName itemName;//ö���ж����itemname

    public void ItemClicked()
    {
        
        this.gameObject.SetActive(false);//����ǰ���õ���Ʒ���أ�������ObjectManager������ͬ���ܵ���䣬���������в�ͬ�㣬�����ǿ��Ƶ��֮��ͬ����
        InventoryManager.Instance.AddItem(itemName);//����Ʒ��ӵ���������
    }
}
