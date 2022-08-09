using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemDataList_SO", menuName = "Inventory/ItemDataList_SO")]
public class ItemDataList_SO : ScriptableObject
{
    public List<ItemDetails> itemDetailsList;

    public ItemDetails GetItemDetails(ItemName itemName)//参数是枚举类型的ItemName，返回值是下面定义的ItemDetails类
    {
        return itemDetailsList.Find(i => i.itemName == itemName);//设置了临时变量i，i的itemName=itemName，如果itemName相等，那么就找到这个itemName的details信息

    }


}

[System.Serializable]
public class ItemDetails//游戏中需要的物品字段
{
    public ItemName itemName;
    public Sprite itemSprite;

}
