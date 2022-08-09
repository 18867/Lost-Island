using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemName itemName;//枚举中定义的itemname

    public void ItemClicked()
    {
        
        this.gameObject.SetActive(false);//将当前放置的物品隐藏，后续的ObjectManager中有相同功能的语句，但是两者有不同点，这里是控制点击之后同场景
        InventoryManager.Instance.AddItem(itemName);//将物品添加到背包当中
    }
}
