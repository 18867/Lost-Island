using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public ItemName requireItem;//需要的物体
    public bool isDone;//是否已经完成

    public void CheckItem(ItemName itemName)//检查是否是需要的物体
    {
        if (itemName == requireItem && !isDone)//如果是需要的物体，并且还没有完成
        {
            isDone = true;//状态结束
            //使用并移除
            OnClickedAction();//点击之后的具体操作，由子类完成
            EventHandler.CallItemUsedEvent(itemName);//触发事件
        }
    }

    //正确物品的时候执行
    protected virtual void OnClickedAction()//多态，具体的实现由子类进行定义
    {

    }

    public virtual void EmptyClicked()
    {
        Debug.Log("空点");
    }
}
