using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemDataList_SO", menuName = "Inventory/ItemDataList_SO")]
public class ItemDataList_SO : ScriptableObject
{
    public List<ItemDetails> itemDetailsList;

    public ItemDetails GetItemDetails(ItemName itemName)//������ö�����͵�ItemName������ֵ�����涨���ItemDetails��
    {
        return itemDetailsList.Find(i => i.itemName == itemName);//��������ʱ����i��i��itemName=itemName�����itemName��ȣ���ô���ҵ����itemName��details��Ϣ

    }


}

[System.Serializable]
public class ItemDetails//��Ϸ����Ҫ����Ʒ�ֶ�
{
    public ItemName itemName;
    public Sprite itemSprite;

}
