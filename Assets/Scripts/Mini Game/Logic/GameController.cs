using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : Singleton<GameController>
{
    public UnityEvent OnFinish;

    [Header("游戏数据")]
    public GameDataH2A_SO gameData;

    public GameDataH2A_SO[] gameDataArray;//多周目

    public GameObject lineParent;

    public LineRenderer linePrefab;

    public Ball ballPrefab;

    public Transform[] holderTransforms;


    private void OnEnable()
    {
        EventHandler.CheckGameStateEvent += OnCheckGameStateEvent;//注册检查游戏状态的方法

    }

    private void OnDisable()
    {
        EventHandler.CheckGameStateEvent -= OnCheckGameStateEvent;
    }


    /*private void Start()
    {
        DrawLine();
        CreateBall();
    }*/
   

    //判断是否匹配到了正确的小球
    private void OnCheckGameStateEvent()
    {
        foreach (var ball in FindObjectsOfType<Ball>())
        {
            if (!ball.isMatch) return;
        }

        Debug.Log("游戏结束");
        //将碰撞体关掉
        foreach (var holder in holderTransforms)
        {
            holder.GetComponent<Collider2D>().enabled = false;
        }

        EventHandler.CallGamePassEvent(gameData.gameName);//通知gamemanager
        OnFinish?.Invoke();

    }

    public void ResetGame()
    {
        //先将子物体删除
        foreach (var holder in holderTransforms)
        {
            if (holder.childCount > 0)
                Destroy(holder.GetChild(0).gameObject);
        }

        DrawLine();
        CreateBall();
    }

    public void DrawLine()
    {
        foreach(var connections in gameData.lineConnections)
        {
            var line = Instantiate(linePrefab, lineParent.transform);
            line.SetPosition(0, holderTransforms[connections.from].position);
            line.SetPosition(1, holderTransforms[connections.to].position);


            //将from和to中的holder连接在一起m为后来筛选出一个球有哪些球是链接在一起的做准备
            holderTransforms[connections.from].GetComponent<Holder>().linkHolders.Add(holderTransforms[connections.to].GetComponent<Holder>());
            holderTransforms[connections.to].GetComponent<Holder>().linkHolders.Add(holderTransforms[connections.from].GetComponent<Holder>());
        }
    }

    public void CreateBall()
    {
        for(int i=0; i<gameData.startBallOrder.Count; i++)
        {
            if(gameData.startBallOrder[i] == BallName.None)
            {
                holderTransforms[i].GetComponent<Holder>().isEmpty = true;
                continue;
            }
            Ball ball = Instantiate(ballPrefab,holderTransforms[i]);
            holderTransforms[i].GetComponent<Holder>().CheckBall(ball);
            holderTransforms[i].GetComponent<Holder>().isEmpty = false;
            ball.SetupBall(gameData.GetBallDetails(gameData.startBallOrder[i]));

        }
    }

    public void SetGameWeekData(int week)
    {
        gameData = gameDataArray[week];
        DrawLine();
        CreateBall();
    }


}
