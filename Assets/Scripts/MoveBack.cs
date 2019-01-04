using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBack : MonoBehaviour {
    [SerializeField] float MoveSpeed;
    Vector3 movethisframe;

    // Use this for initialization
    void Start () {
        MoveSpeed = 2;
	}
	
	// Update is called once per frame
	void Update () {
        movethisframe = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - MoveSpeed * Time.deltaTime, gameObject.transform.position.z);
        gameObject.transform.position = movethisframe;
    }
}
