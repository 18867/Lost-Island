using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//����ű�������ʾ���ǵĳ�����ʵ�ʳ��ֵ���Ʒ��״̬


public class ObjectManager : MonoBehaviour, ISavable
{
    private Dictionary<ItemName, bool> itemAvailableDict = new Dictionary<ItemName, bool>();//�ֵ�
    private Dictionary<string, bool> interactiveStateDict = new Dictionary<string, bool>();

    private void OnEnable()//ע���¼�
    {
        EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadEvent += OnAfterSceneLoadEvent;
        EventHandler.UpdateUIEvent += OnUpdateUIEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
    }

   

    private void OnDisable()
    {
        EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadEvent -= OnAfterSceneLoadEvent;
        EventHandler.UpdateUIEvent -= OnUpdateUIEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;

    }

    private void Start()
    {
        ISavable savable = this;
        savable.SavableRegister();
    }


    private void OnStartNewGameEvent(int obj)
    {
        //�������ֵ�����ݶ����
        itemAvailableDict.Clear();
        interactiveStateDict.Clear();
    }

    private void OnBeforeSceneUnloadEvent()
    {
        foreach(var item in FindObjectsOfType<Item>())
        {
            if (!itemAvailableDict.ContainsKey(item.itemName))
                itemAvailableDict.Add(item.itemName, true);
       
        }

        foreach (var item in FindObjectsOfType<Interactive>())
        {
            if (interactiveStateDict.ContainsKey(item.name))
                interactiveStateDict[item.name] = item.isDone;
            else
                interactiveStateDict.Add(item.name, item.isDone);
        }
    }

    private void OnAfterSceneLoadEvent()
    {
        foreach(var item in FindObjectsOfType<Item>())
        {
            if (!itemAvailableDict.ContainsKey(item.itemName))
                itemAvailableDict.Add(item.itemName, true);
            else
                item.gameObject.SetActive(itemAvailableDict[item.itemName]);//����ֵ����Ѿ����ˣ��͸�����ʾ״̬
        }
        foreach (var item in FindObjectsOfType<Interactive>())
        {
            if (interactiveStateDict.ContainsKey(item.name))
                item.isDone = interactiveStateDict[item.name];
            else
                interactiveStateDict.Add(item.name, item.isDone);
        }
    }


    private void OnUpdateUIEvent(ItemDetails itemDetails, int arg2)
    {
        if (itemDetails != null)//�жϲ�Ϊ��
        {
            itemAvailableDict[itemDetails.itemName] = false;//ʰȡ��Ʒ֮�����س����е���Ʒ�������Ǵӱ�ĳ�������
        }
    }

    public GameSaveData GenerateSaveData()
    {
        GameSaveData savaData = new GameSaveData();
        savaData.itemAvailableDict = this.itemAvailableDict;
        savaData.interactiveStateDict = this.interactiveStateDict;

        return savaData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        this.itemAvailableDict = saveData.itemAvailableDict;
        this.interactiveStateDict = saveData.interactiveStateDict; 
    }
}
