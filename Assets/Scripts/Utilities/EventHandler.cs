using System;
using UnityEngine;


//这里是事件处理器的合计，感觉也没必要把事件处理器放在同一个脚本
public static class EventHandler//改成一个静态的方法，以便于随时随地可以调用
{
    public static event Action<ItemDetails, int> UpdateUIEvent; //这里是声明了一个事件，并且是事件的简单声明方式，这个事件的事件处理器中代码需要满足

    public static void CallUpdateUIEvent(ItemDetails itemDetails, int index)//触发事件
    {
        UpdateUIEvent?.Invoke(itemDetails, index);
    }


    //场景卸载之前
    public static event Action BeforeSceneUnloadEvent;

    public static void CallBeforeSceneUnloadEvent()
    {
        BeforeSceneUnloadEvent?.Invoke();
    }

    //场景加载之后
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
    public static void CallItemUsedEvent(ItemName itemName)//事件
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

    //传递游戏结束的状态
    public static event Action<string> GamePassEvent;

    public static void CallGamePassEvent(string gameName)
    {
        GamePassEvent?.Invoke(gameName);
    }


    //开始一个新游戏，整数参数变量代表周目
    public static event Action<int> StartNewGameEvent;

    public static void CallStartNewGameEvent(int gameweek)
    {
        StartNewGameEvent?.Invoke(gameweek);
    }


}
