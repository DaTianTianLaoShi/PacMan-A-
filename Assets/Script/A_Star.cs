using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    List<List<pageFour>> pagefours = new List<List<pageFour>>();//储存格子列表
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
        Vector2[,] vector2s = new Vector2[nitMiDu + 1, nitMiDu + 1];
        int a = 0;
        for (float i = leftUpPoint.x; i <= RightUpPoint.x; i += pane_with)
        {
            a++;
            int b = 0;
            for (float y = LeftDownPoint.y; y <= leftUpPoint.y; y += pane_hight)
            {
                // vector2s[i][y];
                Vector2 page = new Vector2(i, y);
                b++;
                vector2s[a, b] = page;
                Debug.Log(vector2s[a, b]);
            }
        }
        //声明四个格子
        //获取每个格子的四个角
        int w = 0;
        for (int i=0;i<=nitMiDu/2+1;i+=2) //设置角
        {
            for (int x=0;i<=nitMiDu/2+1;i+=2) 
            {
              int h = 0;
              pagefours[w][h]=new pageFour(vector2s[i,x], vector2s[i,x + 1], vector2s[i + 1,x], vector2s[i+1,x+1]);//计算每个格子的中心点
              OpenList.Add(pagefours[i][x].Countre);//将计算号的坐标添加进数组
              h++;
            }
            w++;
        }
        //开始计算每个网格的中心点
    }
    void A_star(Vector2 StarTransForm,Vector2 EndTransForm) //F=G+H
    {
        bool isin=false;//判断是否在网格内
        bool isStart = true;//判断【是否是初始节点
        //首先判断StartTransForm是否在openlist列表里面
        for(int i = 0;i<=OpenList.Count;i++) 
        {
            if (StarTransForm == OpenList[i] || EndTransForm == OpenList[i]) 
            {
                isin = true;
            }
        }
        if (isin == true)
        {   
            Vector2 LinShi_CloseList;
            CloseList.Reverse();
            LinShi_CloseList = CloseList[0];
            CloseList.Reverse();
            if (LinShi_CloseList==EndTransForm) 
            {

            }
        }
        else 
        {
            Debug.Log("起点或终点不在范围内");
        }   
        //如果在的话要考录边界问题，在我设计的这套东西里面需要和矩阵完成对比
        //这里暂时想不到计算h值的方法，打算直接计算直线距离但愿保证不会出错
        //遍历closelist 
        
    }
    public class pageFour 
    {
        Vector2 LeftUp;
        Vector2 RightUp;
        Vector2 LeftDown;
        Vector2 RightDown;
        Vector2 countre;
        int G;
        int H;
        public Vector2 Countre { get => countre; set => countre = value; }
        
        public pageFour(Vector2 LeftUp,Vector2 LeftDown,Vector2 RightUp,Vector2 RightDown) 
        {
            this.LeftUp = LeftUp;
            this.LeftDown = LeftDown;
            this.RightUp = RightUp;
            this.RightDown = RightDown;
            countre.x= (RightUp.x - LeftUp.x)/2;
            countre.y = (LeftDown.y - LeftUp.y )/ 2;
            Debug.Log(this.countre);
        }
        public pageFour() 
        {

        }

    }
}
