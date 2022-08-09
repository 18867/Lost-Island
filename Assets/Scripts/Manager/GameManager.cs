using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISavable
{
    //这个字典包含了所有的小程序
    private Dictionary<string, bool> miniGameStateDict = new Dictionary<string, bool>();//记录游戏状态，考虑到以后可能会有多个minigame

    private GameController currentGame;

    private int gameWeek;

    private void OnEnable()
    {
        EventHandler.AfterSceneLoadEvent += OnAfterSceneLoadEvent;
        EventHandler.GamePassEvent += OnGamePassEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
    }

    private void OnDisable()
    {
        EventHandler.AfterSceneLoadEvent -= OnAfterSceneLoadEvent;
        EventHandler.GamePassEvent -= OnGamePassEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
    }

    private void OnStartNewGameEvent(int gameWeek)
    {
        this.gameWeek = gameWeek;
        miniGameStateDict.Clear();
    }

    void Start()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        EventHandler.CallGameStateChangeEvent(GameState.GamePlay);

        ISavable savable = this;
        savable.SavableRegister();
    }

  

    private void OnAfterSceneLoadEvent()
    {
        foreach (var miniGame in FindObjectsOfType<MiniGame>())
        {
            if (miniGameStateDict.TryGetValue(miniGame.gameName, out bool isPass))
            {
                miniGame.isPass = isPass;
                miniGame.UpdateMiniGameState();//true的话会执行我们创建的unityevent
            }

        }

        currentGame = FindObjectOfType<GameController>();//一个小游戏一定只有一个gamecontroller

        currentGame?.SetGameWeekData(gameWeek);
    }


    private void OnGamePassEvent(string gameName)//事件处理器
    {
        miniGameStateDict[gameName] = true;
    }

    public GameSaveData GenerateSaveData() //生成存储数据
    {
        GameSaveData saveData = new GameSaveData();
        //saveData.gameWeek = this.gameWeek;
        saveData.miniGamaStateDict = this.miniGameStateDict;//对应的数据存储对应的变量
        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData) //加载数据
    {
        //this.gameWeek = saveData.gameWeek;
        this.miniGameStateDict = saveData.miniGamaStateDict;
    }
}
