using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacdotController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "map")
        {
            //Debug.Log("aaa");
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            gameObject.SetActive(false);
        }
    }
}
