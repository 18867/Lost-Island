using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameDataH2A_SO", menuName = "Mini Game Data/GamaDataH2A_SO")]
public class GameDataH2A_SO : ScriptableObject
{
    [SceneName] public string gameName;
    
    [Header("������ֺͶ�ӦͼƬ")]
    public List<BallDetails> ballDataList;


    [Header("��Ϸ�߼�����")]
    //��������
    public List<Connections> lineConnections;
    //��ʼ�ķ���λ��
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
