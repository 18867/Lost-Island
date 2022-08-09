using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//调用几个接口
public class SlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image itemImage;
    public ItemToolTip toolTip;
    private ItemDetails currentItem;

    private bool isSelected;

    public void SetItem(ItemDetails itemDetails)
    {
        currentItem = itemDetails;
        this.gameObject.SetActive(true);
        itemImage.sprite = itemDetails.itemSprite;
        itemImage.SetNativeSize();
    }

    public void SetEmpty()
    {
        this.gameObject.SetActive(false);
        isSelected = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick");
        isSelected = !isSelected;//需要将这个数据传给cursormanager
        EventHandler.CallItemSelectedEvent(currentItem, isSelected);//触发事件,但是没有找到事件处理器
    
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       // if (this.gameObject.activeInHierarchy)
        //{
            toolTip.gameObject.SetActive(true);
            toolTip.UpdateItemName(currentItem.itemName);
       // }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTip.gameObject.SetActive(false);
    }
}
