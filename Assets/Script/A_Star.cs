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
    List<pageFour> OpenList = new List<pageFour>();
    List<pageFour> CloseList = new List<pageFour>();
    List<List<pageFour>> pagefours = new List<List<pageFour>>();//储存格子列表
    public A_Star(Vector2 lU, Vector2 RU, Vector2 LD, int WangGeMiDu)//二维使用方法
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
        for (int i = 0; i <= nitMiDu / 2 + 1; i += 2) //设置角
        {
            for (int x = 0; i <= nitMiDu / 2 + 1; i += 2)
            {
                int h = 0;
                pagefours[w][h] = new pageFour(vector2s[i, x], vector2s[i, x + 1], vector2s[i + 1, x], vector2s[i + 1, x + 1]);//计算每个格子的中心点
                OpenList.Add(pagefours[i][x]);//将计算号的坐标添加进数组
                h++;
            }
            w++;
        }
        //开始计算每个网格的中心点
    }

    List<Vector2> A_star(Vector2 StarTransForm, Vector2 EndTransForm) //F=G+H
    {
        List<Vector2> returnVector2s = new List<Vector2>();
        bool isin = false;//判断是否在网格内
        bool isStart = true;//判断【是否是初始节点
        //首先判断StartTransForm是否在openlist列表里面
        for (int i = 0; i <= OpenList.Count; i++)
        {
            if (StarTransForm == OpenList[i].CountrePoint || EndTransForm == OpenList[i].CountrePoint)
            {
                isin = true;
            }
        }
        if (isin == true)
        {
            Vector2 LinShi_CloseList;//找到链表的末尾
            CloseList.Reverse();
            LinShi_CloseList = CloseList[0].CountrePoint;
            CloseList.Reverse();
            if (LinShi_CloseList == EndTransForm) //如果链表等于终点，返回
            {
                Debug.Log("中止算法");
                goto led;
            }
            //  LinShi_CloseList 
            //首先确定该点在矩阵当中的位置
            List<pageFour> list = new List<pageFour>();
            for (int x = 0; x <= pagefours.Count; x++)
            {
                for (int y = 0; y <= pagefours[0].Count; y++)
                {
                    if (pagefours[x][y].CountrePoint == LinShi_CloseList)
                    {
                        try
                        {
                            list.Add(pagefours[x + 1][y]);
                            list.Add(pagefours[x + 1][y - 1]);
                            list.Add(pagefours[x - 1][y]);
                            list.Add(pagefours[x - 1][y - 1]);
                            list.Add(pagefours[x][y - 1]);
                            list.Add(pagefours[x - 1][y + 1]);
                            list.Add(pagefours[x][y + 1]);
                        }
                        catch
                        {
                            //判断是ClosPoint否在边界
                            if (x <= 0 || y <= 0)
                            {
                                list.Add(pagefours[x + 1][y]);
                                list.Add(pagefours[x + 1][y - 1]);
                                list.Add(pagefours[x][y + 1]);
                            } else if (y <= 0)
                            {
                                list.Add(pagefours[x + 1][y]);
                                list.Add(pagefours[x - 1][y]);
                                list.Add(pagefours[x - 1][y + 1]);
                                list.Add(pagefours[x][y + 1]);
                            } else if (x <= 0)
                            {
                                list.Add(pagefours[x + 1][y]);
                                list.Add(pagefours[x + 1][y - 1]);
                                //list.Add(pagefours[x - 1][y]);
                                //list.Add(pagefours[x - 1][y - 1]);
                                list.Add(pagefours[x][y - 1]);
                                //list.Add(pagefours[x - 1][y + 1]);
                                list.Add(pagefours[x][y + 1]);
                            } else if (x > pagefours[0].Count || y > pagefours[0].Count)
                            {
                                //list.Add(pagefours[x + 1][y]);
                                // list.Add(pagefours[x + 1][y - 1]);
                                list.Add(pagefours[x - 1][y]);
                                list.Add(pagefours[x - 1][y - 1]);
                                list.Add(pagefours[x][y - 1]);
                                //list.Add(pagefours[x - 1][y + 1]);
                                //list.Add(pagefours[x][y + 1]);
                            } else if (y > pagefours.Count)
                            {
                                list.Add(pagefours[x + 1][y]);
                                list.Add(pagefours[x + 1][y - 1]);
                                list.Add(pagefours[x - 1][y]);
                                list.Add(pagefours[x - 1][y - 1]);
                                list.Add(pagefours[x][y - 1]);

                            }
                            else if (x > pagefours[0].Count)
                            {

                                list.Add(pagefours[x - 1][y]);
                                list.Add(pagefours[x - 1][y - 1]);
                                list.Add(pagefours[x][y - 1]);
                                list.Add(pagefours[x - 1][y + 1]);
                                list.Add(pagefours[x][y + 1]);
                            }
                        }
                        //寻找到
                        foreach (pageFour page in CloseList)//查看是否与封闭表格重复 
                        {
                            int index = 0;
                            if (list[index] == page)
                            {
                                Debug.Log(list[index]);
                                list.Remove(list[index]);
                            }
                            index++;
                        }
                    }
                }
            }
        }
        else
        {
            Debug.Log("起点或终点不在范围内");
        }
    //如果在的话要考录边界问题，在我设计的这套东西里面需要和矩阵完成对比
    //这里暂时想不到计算h值的方法，打算直接计算直线距离但愿保证不会出错

    led://遍历closelist
        foreach (pageFour vector2 in CloseList)
        {
            returnVector2s.Add(vector2.CountrePoint);
        }
        return returnVector2s;
    }
    void AddCloseList(List<Vector2> vectors)//初始化的时候使用
    {
        //将
    }
    void Paixu(List<pageFour> pageFours)//将格子排序的方法并加入关闭列表里面
    {
        float num_change = 0;
        float num_chaneg2 = 0;
        for (int num=0;num< pageFours.Count;num++)
        {
            for (int num2=1;num2<pageFours.Count-1-num;num2++)
            {
                num_change = pageFours[num2].H + pageFours[num].G;
                num_chaneg2 = pageFours[num2+1].G + pageFours[num2+1].H;
                if(num_change<num_chaneg2)//将最小值往前冒泡
                {
                    pageFour LinShi= pageFours[num];
                    pageFours[num] = pageFours[num2];
                    pageFours[num2] = LinShi;
                }
            }
        }
        Debug.Log(pageFours[0].CountrePoint);
        CloseList.Add(pageFours[0]);//加入封闭表格
    }
    public class pageFour 
    {
        Vector2 LeftUp;
        Vector2 RightUp;
        Vector2 LeftDown;
        Vector2 RightDown;
        Vector2 countre;
        public int G;
        public float H;
        public Vector2 CountrePoint { get => countre; set => countre = value; }
        public Vector2 LeftUpPoint { get => LeftUp; set => LeftUp = value; }
        public Vector2 RightUpPoint { get => RightUp; set => RightUp = value; }
        public Vector2 LeftDownPoint { get => LeftDown; set => LeftDown = value; }
        public Vector2 RightDownPoint { get => RightDown; set => RightDown = value; }

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
