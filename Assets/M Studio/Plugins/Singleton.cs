using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;//这个脚本是单例

    public static T Instance//单例脚本的属性返回单例这里是不是可以省略？
    {
        get { return instance; }
    }

    protected virtual void Awake()
    {
        if (instance != null)//有的时候删除
            Destroy(gameObject);
        else
            instance = (T)this;//没有的时候创建单例，泛型单例的类型可能不同，所以这里需要做强制类型转换
    }

    public static bool IsInitialized //判断是否确定生成了单例
    {
        get { return instance != null; }
    }

    protected virtual void OnDestroy() //单例销毁
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
