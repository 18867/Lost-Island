using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Teleport脚本是挂载到相应的物体上的，但是场景转换的逻辑不应该放在这里
//所以是调用了一个单例模式的TransitionManager中的方法，来做到低耦合

public class Teleport : MonoBehaviour
{
    [SceneName] public string sceneFrom;
    [SceneName] public string sceneTo;

    public void TeleportToScene()
    {
        TransitionManager.Instance.Transition(sceneFrom, sceneTo);
    }

}
