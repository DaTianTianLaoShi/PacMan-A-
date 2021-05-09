using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
     float Speed=5f;
    Vector2 playerto;
    void Start()
    {
        playerto = gameObject.transform.position;
    }
    void FixedUpdate()
    {

        Vector2 a = Vector2.MoveTowards(this.gameObject.transform.position, playerto, Speed * Time.fixedDeltaTime);
        this.gameObject.GetComponent<Rigidbody2D>().MovePosition(a);
        if ((Vector2)gameObject.transform.position == playerto)
        {
            Vector2 s=Vector2.zero;
            if (Input.GetKey(KeyCode.A)&&!CanGo(Vector2.left))
            {
                s = Vector2.left;
            }
             else if (Input.GetKey(KeyCode.S)&&!CanGo(Vector2.down))
            {
                s = Vector2.down;
            }
             else if (Input.GetKey(KeyCode.D)&&!CanGo(Vector2.right))
            {
                s = Vector2.right;
            }
             else if (Input.GetKey(KeyCode.W)&&!CanGo(Vector2.up))
            {
                s = Vector2.up;
            }
            playerto += s;
            gameObject.GetComponent<Animator>().SetFloat("DirX",s.x);
            gameObject.GetComponent<Animator>().SetFloat("DirY",s.y);
        }
    }
    void Update()
    {
    }
    bool CanGo(Vector2 ss)
    {
        //Debug.Log("检测到障碍物");
       RaycastHit2D raycastHit2=Physics2D.Linecast(this.transform.position,(Vector2)this.transform.position+ss,1<<LayerMask.NameToLayer("map"));
        if (raycastHit2==true)
        {
            Debug.Log("检测到障碍物");
        }
        return raycastHit2;
    }
}
