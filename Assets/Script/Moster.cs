using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moster : MonoBehaviour {
    StaticMachine<Moster> machine = new StaticMachine<Moster>();
    public List<Vector2> LiveHomePath;
    public float speed = 4f;
    class WayPointSetect:SureStatic<Moster>//一种状态
    {
        private List<Vector2> path;
        private int index;//当前走的路径点个数
         public  WayPointSetect(List<Vector2> path)
        {
            this.path = path;
            this.index = 0;
        }
        public override void Update(Moster c)
        {
            Vector2 a = Vector2.MoveTowards(c.transform.position,path[index],c.speed*Time.fixedDeltaTime);
            c.GetComponent<Rigidbody2D>().MovePosition(a);
            if ((Vector2)c.transform.position==a)
            {
                index++;
                if (index>=path.Count)//.count属性判断元素真实个数
                {
                    c.machine.Init(c,new XunluoPoint());
                    Debug.Log("出发路点完成");
                }else 
                {
                    Vector2 b = path[index] - path[index - 1];
                    c.GetComponent<Animator>().SetFloat("MirX",b.x);
                    c.GetComponent<Animator>().SetFloat("MirY",b.y);
                }
            }
        }
    }
    class XunluoPoint:SureStatic<Moster>
    {
        private Vector2 dir;
        private Vector2 Edir;
        private Vector2[] EdirTo = new Vector2[] { Vector2.left, Vector2.up,Vector2.right,Vector2.down };
        public override void Enter(Moster b)
        {
            dir = b.transform.position;
            
        }
        public override void Update(Moster c)
        {
            Edir = c.transform.position;
            Vector2 b = Vector2.MoveTowards(c.transform.position,dir,c.speed*Time.fixedDeltaTime);
            c.gameObject.GetComponent<Rigidbody2D>().MovePosition(b);
            if ((Vector2)c.transform.position==dir)
            {
                List<Vector2> Averation = new List<Vector2>();
                for (int i=0;i<EdirTo.Length;i++)
                {
                    Vector2 d = EdirTo[i] + (Vector2)c.gameObject.transform.position;
                    Edir = Edir + EdirTo[i];
                    if (d.x==(Edir.x-=1))
                    {
                        Debug.Log("重复，跳出路径");
                        continue;
                    }
                    if (d.y==(Edir.y-=1))
                    {
                        continue;
                    }
                    if (c.CanGo(EdirTo[i]) == false)
                    {
                        Averation.Add(EdirTo[i]);
                    }    
                    
                }
                int a = Random.Range(0,Averation.Count);
                dir += Averation[a];
            }
        }
    }
    private bool CanGo(Vector2 dir)
    {
        RaycastHit2D a = Physics2D.Linecast(this.transform.position,(Vector2)this.transform.position+dir,1<<LayerMask.NameToLayer("map"));
        return a;
    }
	void Start ()
    {
        machine.Init(this, new WayPointSetect(LiveHomePath));
	}
     void FixedUpdate()
     {
        machine.Update();
     }
}
