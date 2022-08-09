using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISavable
{
    //����ֵ���������е�С����
    private Dictionary<string, bool> miniGameStateDict = new Dictionary<string, bool>();//��¼��Ϸ״̬�����ǵ��Ժ���ܻ��ж��minigame

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
                miniGame.UpdateMiniGameState();//true�Ļ���ִ�����Ǵ�����unityevent
            }

        }

        currentGame = FindObjectOfType<GameController>();//һ��С��Ϸһ��ֻ��һ��gamecontroller

        currentGame?.SetGameWeekData(gameWeek);
    }


    private void OnGamePassEvent(string gameName)//�¼�������
    {
        miniGameStateDict[gameName] = true;
    }

    public GameSaveData GenerateSaveData() //���ɴ洢����
    {
        GameSaveData saveData = new GameSaveData();
        //saveData.gameWeek = this.gameWeek;
        saveData.miniGamaStateDict = this.miniGameStateDict;//��Ӧ�����ݴ洢��Ӧ�ı���
        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData) //��������
    {
        //this.gameWeek = saveData.gameWeek;
        this.miniGameStateDict = saveData.miniGamaStateDict;
    }
}
