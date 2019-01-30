using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBack : MonoBehaviour {
    [SerializeField] float MoveSpeed;
    Vector3 movethisframe;
    Rigidbody2D MyBody;
    // Use this for initialization
    void Start () {
        if(GetComponent<Rigidbody2D>())
        {
            MyBody = GetComponent<Rigidbody2D>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (MyBody)
        {
            movethisframe = new Vector3(0, -MoveSpeed , 0);
            MyBody.AddForce(movethisframe);
        }
        else
        {
            movethisframe = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - MoveSpeed * Time.deltaTime, gameObject.transform.position.z);
            gameObject.transform.position = movethisframe;
        }
    }
}
