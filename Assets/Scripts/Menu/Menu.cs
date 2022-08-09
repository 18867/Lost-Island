using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{


    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("You have quited from the game");
    }

    public void ContinueGame()
    {
        Debug.Log("加载游戏进度");
        SaveLoadManager.Instance.Load();//加载游戏进度
    }

    public void GoBackToMenu()
    {
        Debug.Log("回到主菜单页");
        var currentScene = SceneManager.GetActiveScene().name;
        TransitionManager.Instance.Transition(currentScene, "Menu");

        //保存游戏进度
        //SaveLoadManager.Instance.Save();
    }

    public void StartGameWeek(int gameweek)
    {
        //需要告知gamemanager，并且很多游戏数据都需要重置，比如Object Manager中的两个字典
        EventHandler.CallStartNewGameEvent(gameweek);//触发游戏开始事件

    }
}
