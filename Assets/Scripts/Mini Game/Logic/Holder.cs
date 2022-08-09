using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : Interactive
{

    public BallName matchBall;
    private Ball currentBall;
    public HashSet<Holder> linkHolders = new HashSet<Holder>();//没有重复的数据集
    public bool isEmpty;


    //判断是不是正确的球
    public void CheckBall(Ball ball)
    {
        currentBall = ball;
        if(ball.ballDetails.ballName == matchBall)
        {
           
                currentBall.isMatch = true;
                currentBall.SetRight();
            }
            else
            {
                currentBall.isMatch = false;
                currentBall.SetWrong();
            }
        



    }


    public override void EmptyClicked()//空点的状态
    {
        Debug.Log("开始进入空点状态");

        foreach (var holder in linkHolders)
        {  
            if (holder.isEmpty)
            {
                //移动球
                currentBall.transform.position = holder.transform.position;
                currentBall.transform.SetParent(holder.transform);//设置父物体
                Debug.Log("完成球的移动");

                //交换球
                holder.CheckBall(currentBall);//检查移过去的球是不是正确的球
                this.currentBall = null;//现在的holder里面的球就没有了

                //改变状态
                this.isEmpty = true;//自己的状态为空
                holder.isEmpty = false;

                EventHandler.CallCheckGameStateEvent();//触发事件
            }
        }
    }

}
