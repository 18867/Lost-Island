using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : Interactive
{

    public BallName matchBall;
    private Ball currentBall;
    public HashSet<Holder> linkHolders = new HashSet<Holder>();//û���ظ������ݼ�
    public bool isEmpty;


    //�ж��ǲ�����ȷ����
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


    public override void EmptyClicked()//�յ��״̬
    {
        Debug.Log("��ʼ����յ�״̬");

        foreach (var holder in linkHolders)
        {  
            if (holder.isEmpty)
            {
                //�ƶ���
                currentBall.transform.position = holder.transform.position;
                currentBall.transform.SetParent(holder.transform);//���ø�����
                Debug.Log("�������ƶ�");

                //������
                holder.CheckBall(currentBall);//����ƹ�ȥ�����ǲ�����ȷ����
                this.currentBall = null;//���ڵ�holder��������û����

                //�ı�״̬
                this.isEmpty = true;//�Լ���״̬Ϊ��
                holder.isEmpty = false;

                EventHandler.CallCheckGameStateEvent();//�����¼�
            }
        }
    }

}
