using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>, ISavable
{
    public ItemDataList_SO itemData;

    [SerializeField] private List<ItemName> itemList = new List<ItemName>();

    private void OnEnable()//ÿ�νű������ʱ��ͽ��е���
    {
        EventHandler.ItemUsedEvent += OnItemUsedEvent;//�¼������ġ��¼�������
        EventHandler.ChangeItemEvent += OnChangeItemEvent;
        EventHandler.AfterSceneLoadEvent += OnAfterSceneLoadEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
    }

    private void OnDisable()
    {
        EventHandler.ItemUsedEvent -= OnItemUsedEvent;
        EventHandler.ChangeItemEvent -= OnChangeItemEvent;
        EventHandler.AfterSceneLoadEvent -= OnAfterSceneLoadEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
    }


    private void Start()
    {
        ISavable savable = this;
        savable.SavableRegister();
    }

    private void OnStartNewGameEvent(int obj)
    {
        itemList.Clear();
    }


    //����item�����ǵı���ϵͳ��
    private void OnAfterSceneLoadEvent()//�¼�������
    {
        if (itemList.Count == 0)
            EventHandler.CallUpdateUIEvent(null, -1);
        else
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(itemList[i]), i);
            }
        }
    }

    private void OnChangeItemEvent(int index)
    {
        if (index>=0 && index<itemList.Count)
        {
            ItemDetails item = itemData.GetItemDetails(itemList[index]);
            EventHandler.CallUpdateUIEvent(item, index);
        }
    }

    private void OnItemUsedEvent(ItemName itemName)
    {
        var index = GetItemIndex(itemName);
        itemList.RemoveAt(index);//��һ��Ʒ��ʹ�����

        if (itemList.Count == 0)
            EventHandler.CallUpdateUIEvent(null, -1);
    }

    public void AddItem(ItemName itemName)
    {
        if (!itemList.Contains(itemName))
        {
            itemList.Add(itemName);
            EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(itemName), itemList.Count - 1);//�����¼�
            //��ʾ��Ӧ��UI
        }
    }


    private int GetItemIndex(ItemName itemName)//����Ϊʲô��Ҫ��ȡ
    {
        for(int i=0; i<itemList.Count; i++)
        {
            if (itemList[i] == itemName)
                return i;
        }

        return -1;
    }

    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.itemList = this.itemList;
        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        this.itemList = saveData.itemList;
    }
}
