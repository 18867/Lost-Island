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
        EventHandler.ItemSelectedEvent += OnItemSelectedEvent;//�¼����ġ��¼�������
        EventHandler.ItemUsedEvent += OnItemUsedEvent;
    }

    private void OnDisable()
    {
        EventHandler.ItemSelectedEvent -= OnItemSelectedEvent;
        EventHandler.ItemUsedEvent -= OnItemUsedEvent;
    }

    

    //�ƶ��Ļ��ŵ�update����ִ��
    private void Update()
    {
        canClick = ObjectAtMousePosition();//�������ÿһ֡����ִ��,����һ������ֵ����ֵ��canClick
        if (hand.gameObject.activeInHierarchy)
        {
            hand.gameObject.transform.position = Input.mousePosition;//ʵ�ָ���Ч��
        }

        if (InteractWithUI()) return;

        if (canClick && Input.GetMouseButtonDown(0))
        {
            Debug.Log("�㵽��");
            ClickAction(ObjectAtMousePosition().gameObject);
        }

    }
    private void OnItemUsedEvent(ItemName obj)
    {
        currentItem = ItemName.None;
        holdItem = false;
        hand.gameObject.SetActive(false);
    }


    //ѡ��item��ʱ����ʾ�ֵ�ͼƬ
    private void OnItemSelectedEvent(ItemDetails itemDetails, bool isSelected)
    {
        Debug.Log("�¼���Ӧ����ʼ����");
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
                item?.ItemClicked();//���item��Ϊ�գ���ôִ��itemclicked����
                break;
            case "Interactive":
                var interactive = clickObject.GetComponent<Interactive>();//�ж��Ƿ�����
                if (holdItem)
                { Debug.Log("���Ŷ�����"); interactive?.CheckItem(currentItem);  }
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

    private bool InteractWithUI()//�ж��Ƿ��UI�����˽���
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return true;
        
        else return false;
    }
}
