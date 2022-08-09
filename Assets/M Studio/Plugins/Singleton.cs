using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;//����ű��ǵ���

    public static T Instance//�����ű������Է��ص��������ǲ��ǿ���ʡ�ԣ�
    {
        get { return instance; }
    }

    protected virtual void Awake()
    {
        if (instance != null)//�е�ʱ��ɾ��
            Destroy(gameObject);
        else
            instance = (T)this;//û�е�ʱ�򴴽����������͵��������Ϳ��ܲ�ͬ������������Ҫ��ǿ������ת��
    }

    public static bool IsInitialized //�ж��Ƿ�ȷ�������˵���
    {
        get { return instance != null; }
    }

    protected virtual void OnDestroy() //��������
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
