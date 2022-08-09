using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManager: MonoBehaviour
{
    public GameObject hand;
    private Vector3 MouseWorldPos => Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    private ItemName currentItem;
    private bool canClick;
    private bool holdItem;

    private void OnEnable()
    {
        EventHandler.ItemSelectedEvent += OnItemSelectedEvent;//事件订阅、事件处理器
        EventHandler.ItemUsedEvent += OnItemUsedEvent;
    }

    private void OnDisable()
    {
        EventHandler.ItemSelectedEvent -= OnItemSelectedEvent;
        EventHandler.ItemUsedEvent -= OnItemUsedEvent;
    }

    

    //移动的话放到update中来执行
    private void Update()
    {
        canClick = ObjectAtMousePosition();//这个方法每一帧都会执行,返回一个布尔值，赋值给canClick
        if (hand.gameObject.activeInHierarchy)
        {
            hand.gameObject.transform.position = Input.mousePosition;//实现跟随效果
        }

        if (InteractWithUI()) return;

        if (canClick && Input.GetMouseButtonDown(0))
        {
            Debug.Log("点到啦");
            ClickAction(ObjectAtMousePosition().gameObject);
        }

    }
    private void OnItemUsedEvent(ItemName obj)
    {
        currentItem = ItemName.None;
        holdItem = false;
        hand.gameObject.SetActive(false);
    }


    //选中item的时候显示手的图片
    private void OnItemSelectedEvent(ItemDetails itemDetails, bool isSelected)
    {
        Debug.Log("事件响应器开始运行");
        holdItem = isSelected;
        if (isSelected)
        {
            currentItem = itemDetails.itemName; 
        }
        hand.gameObject.SetActive(true);
    }


    private void ClickAction(GameObject clickObject)
    {
        switch(clickObject.tag)
        {
            case "Teleport":
                var teleport = clickObject.GetComponent<Teleport>();
                teleport?.TeleportToScene();
                break;
            case "Item":
                var item = clickObject.GetComponent<Item>();
                item?.ItemClicked();//如果item不为空，那么执行itemclicked方法
                break;
            case "Interactive":
                var interactive = clickObject.GetComponent<Interactive>();//判断是否点击了
                if (holdItem)
                { Debug.Log("带着东西呢"); interactive?.CheckItem(currentItem);  }
                else
                { interactive?.EmptyClicked();
                   
                }
                break;
        }    
    }

    private Collider2D ObjectAtMousePosition()
    {
        
        return Physics2D.OverlapPoint(MouseWorldPos);
    }

    private bool InteractWithUI()//判断是否和UI进行了交互
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return true;
        
        else return false;
    }
}
