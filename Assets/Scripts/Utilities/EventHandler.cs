using System;
using UnityEngine;


//�������¼��������ĺϼƣ��о�Ҳû��Ҫ���¼�����������ͬһ���ű�
public static class EventHandler//�ĳ�һ����̬�ķ������Ա�����ʱ��ؿ��Ե���
{
    public static event Action<ItemDetails, int> UpdateUIEvent; //������������һ���¼����������¼��ļ�������ʽ������¼����¼��������д�����Ҫ����

    public static void CallUpdateUIEvent(ItemDetails itemDetails, int index)//�����¼�
    {
        UpdateUIEvent?.Invoke(itemDetails, index);
    }


    //����ж��֮ǰ
    public static event Action BeforeSceneUnloadEvent;

    public static void CallBeforeSceneUnloadEvent()
    {
        BeforeSceneUnloadEvent?.Invoke();
    }

    //��������֮��
    public static event Action AfterSceneLoadEvent;

    public static void CallAfterSceneLoadEvent()
    {
        AfterSceneLoadEvent?.Invoke();
    }

    public static event Action<ItemDetails, bool> ItemSelectedEvent;
    public static void CallItemSelectedEvent(ItemDetails itemDetails, bool isSelected)
    {
        ItemSelectedEvent?.Invoke(itemDetails, isSelected);
    }

    public static event Action<ItemName> ItemUsedEvent;//
    public static void CallItemUsedEvent(ItemName itemName)//�¼�
    {
        ItemUsedEvent?.Invoke(itemName);
    }

    public static event Action<int> ChangeItemEvent;
    public static void CallChangeItemEvent(int index)
    {
        ChangeItemEvent?.Invoke(index);
    }

    public static event Action<string> ShowDialogueEvent;
    public static void CallShowDialogueEvent(string dialogue)
    {
        ShowDialogueEvent?.Invoke(dialogue);
    }


    public static event Action<GameState> GameStateChangeEvent;

    public static void CallGameStateChangeEvent(GameState gameState)
    {
        GameStateChangeEvent?.Invoke(gameState);
    }

    public static event Action CheckGameStateEvent;

    public static void CallCheckGameStateEvent()
    {
        CheckGameStateEvent?.Invoke();
    }

    //������Ϸ������״̬
    public static event Action<string> GamePassEvent;

    public static void CallGamePassEvent(string gameName)
    {
        GamePassEvent?.Invoke(gameName);
    }


    //��ʼһ������Ϸ��������������������Ŀ
    public static event Action<int> StartNewGameEvent;

    public static void CallStartNewGameEvent(int gameweek)
    {
        StartNewGameEvent?.Invoke(gameweek);
    }


}
