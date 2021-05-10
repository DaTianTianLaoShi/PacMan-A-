using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Star 
{
    //决定使用对象类了
    //首先要确定地图的三个点
    Vector2 leftUpPoint;
    Vector2 RightUpPoint;
    Vector2 LeftDownPoint;
    int nitMiDu;
    List<Vector2> OpenList = new List<Vector2>();
    List<Vector2> CloseList = new List<Vector2>();
    public A_Star(Vector2 lU,Vector2 RU,Vector2 LD,int WangGeMiDu)//二维使用方法
    {
        leftUpPoint = lU;
        RightUpPoint = RU;
        LeftDownPoint = LD;
        nitMiDu = WangGeMiDu;
    }
    //首先要根据场景来计算，每个方格的大小，所以要开始画放格子的方法
    void drawGeZi() //根据场景画格子的方法
    {
        float pane_with;
        float pane_hight;
        //计算横向宽度
        pane_with = (leftUpPoint.x - RightUpPoint.x) / nitMiDu;
        pane_hight = (leftUpPoint.y - LeftDownPoint.y) / nitMiDu;
        //首先画好竖线
        //之后画号横线
        //计算二维坐标以二维数组的方式储存
        Vector2[,] vector2s = new Vector2[nitMiDu+1,nitMiDu+1];
        int a = 0;
        int b = 0;
        for (float i=leftUpPoint.x ;i<=RightUpPoint.x;i+=pane_with ) 
        {
            a++;
            for (float y = LeftDownPoint.y; y <= leftUpPoint.y; y += pane_hight)
            {
                // vector2s[i][y];
                Vector2 page = new Vector2(i, y);
                b++;
                vector2s[a,b]=page;
                Debug.Log(vector2s[a, b]);
            }
        }
        //声明四个格子
        List<pageFour> pagefours = new List<pageFour>();
        //获取每个格子的四个角
        for (int i=0;i<=nitMiDu/2+1;i+=2) //设置角
        {
            for (int x=0;i<=nitMiDu/2+1;i+=2) 
            {

            }
        }
        //开始计算每个网格的中心点
    }
    public class pageFour 
    {
        Vector2 LeftUp;
        Vector2 RightUp;
        Vector2 LeftDown;
        Vector2 RightDown;
        Vector2 countre;
        public pageFour(Vector2 leftUp,Vector2 LeftDown,Vector2 RightUp,Vector2 RightDown) 
        {
            this.LeftUp = leftUp;
            this.LeftDown = LeftDown;
            this.RightUp = RightUp;
            this.RightDown = RightDown;
            countre.x= (RightUp.x - leftUp.x)/2;
            countre.y = (LeftDown.y - leftUp.y )/ 2;
            Debug.Log(this.countre);
        }
    }
}
