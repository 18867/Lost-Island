using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Button leftButton, rightButton;
    public SlotUI slotUI;

    public int currentIndex;//当前的物品序号

    private void OnEnable()
    {
        EventHandler.UpdateUIEvent += OnUpdateUIEvent;//事件的订阅
    }

    private void OnDisable()
    {
        EventHandler.UpdateUIEvent -= OnUpdateUIEvent;
    }

    private void OnUpdateUIEvent(ItemDetails itemDetails, int index)//事件处理器
    {
        if (itemDetails == null)
        {
            slotUI.SetEmpty();
            currentIndex = -1;
            leftButton.interactable = false;
            rightButton.interactable = false;
        }
        else
        {
            currentIndex = index;
            slotUI.SetItem(itemDetails);

            if (index > 0)
                leftButton.interactable = true;
            if(index == -1)
            {
                leftButton.interactable = false;
                rightButton.interactable = false;
            }
        }
    }

    public void SwitchItem(int amount)
    {
        var index = currentIndex + amount;

        if (index < currentIndex)
        {
            leftButton.interactable = false;
            rightButton.interactable = true;
        }

        else if(index > currentIndex)
        {
            leftButton.interactable = true;
            rightButton.interactable = false;
        }
        else//多个两个物体的情况
        {
            leftButton.interactable = true;
            rightButton.interactable = true;
        }

        EventHandler.CallChangeItemEvent(index);
    }


    public void ShowLog()
    {
        Debug.Log("这个item holder被点到啦");
    }
}
