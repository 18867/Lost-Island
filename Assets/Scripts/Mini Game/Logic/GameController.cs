using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : Singleton<GameController>
{
    public UnityEvent OnFinish;

    [Header("��Ϸ����")]
    public GameDataH2A_SO gameData;

    public GameDataH2A_SO[] gameDataArray;//����Ŀ

    public GameObject lineParent;

    public LineRenderer linePrefab;

    public Ball ballPrefab;

    public Transform[] holderTransforms;


    private void OnEnable()
    {
        EventHandler.CheckGameStateEvent += OnCheckGameStateEvent;//ע������Ϸ״̬�ķ���

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
   

    //�ж��Ƿ�ƥ�䵽����ȷ��С��
    private void OnCheckGameStateEvent()
    {
        foreach (var ball in FindObjectsOfType<Ball>())
        {
            if (!ball.isMatch) return;
        }

        Debug.Log("��Ϸ����");
        //����ײ��ص�
        foreach (var holder in holderTransforms)
        {
            holder.GetComponent<Collider2D>().enabled = false;
        }

        EventHandler.CallGamePassEvent(gameData.gameName);//֪ͨgamemanager
        OnFinish?.Invoke();

    }

    public void ResetGame()
    {
        //�Ƚ�������ɾ��
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


            //��from��to�е�holder������һ��mΪ����ɸѡ��һ��������Щ����������һ�����׼��
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
