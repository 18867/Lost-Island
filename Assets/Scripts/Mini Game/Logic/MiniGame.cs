using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MiniGame : MonoBehaviour
{
    public UnityEvent OnGameFinish;
    [SceneName] public string gameName;

    public bool isPass;

    public void UpdateMiniGameState()
    {
        if (isPass)
        {
            GetComponent<Collider2D>().enabled = false;//¹Ø±ÕÅö×²Ìå
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);//°ëÍ¸Ã÷
            OnGameFinish?.Invoke();
        }
    }
}
