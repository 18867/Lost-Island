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
        Debug.Log("������Ϸ����");
        SaveLoadManager.Instance.Load();//������Ϸ����
    }

    public void GoBackToMenu()
    {
        Debug.Log("�ص����˵�ҳ");
        var currentScene = SceneManager.GetActiveScene().name;
        TransitionManager.Instance.Transition(currentScene, "Menu");

        //������Ϸ����
        //SaveLoadManager.Instance.Save();
    }

    public void StartGameWeek(int gameweek)
    {
        //��Ҫ��֪gamemanager�����Һܶ���Ϸ���ݶ���Ҫ���ã�����Object Manager�е������ֵ�
        EventHandler.CallStartNewGameEvent(gameweek);//������Ϸ��ʼ�¼�

    }
}
