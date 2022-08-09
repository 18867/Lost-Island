using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Teleport�ű��ǹ��ص���Ӧ�������ϵģ����ǳ���ת�����߼���Ӧ�÷�������
//�����ǵ�����һ������ģʽ��TransitionManager�еķ����������������

public class Teleport : MonoBehaviour
{
    [SceneName] public string sceneFrom;
    [SceneName] public string sceneTo;

    public void TeleportToScene()
    {
        TransitionManager.Instance.Transition(sceneFrom, sceneTo);
    }

}
