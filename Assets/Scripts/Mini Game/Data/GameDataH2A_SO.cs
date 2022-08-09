using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameDataH2A_SO", menuName = "Mini Game Data/GamaDataH2A_SO")]
public class GameDataH2A_SO : ScriptableObject
{
    [SceneName] public string gameName;
    
    [Header("球的名字和对应图片")]
    public List<BallDetails> ballDataList;


    [Header("游戏逻辑数据")]
    //生成连线
    public List<Connections> lineConnections;
    //初始的放置位置
    public List<BallName> startBallOrder;


    public BallDetails GetBallDetails(BallName ballName)
    {
        return ballDataList.Find(b => b.ballName == ballName);
    }
}


[System.Serializable]
public class BallDetails
{
    public BallName ballName;
    public Sprite wrongSprite;
    public Sprite rightSprite;
}

[System.Serializable]
public class Connections
{
    public int from;
    public int to;
}
