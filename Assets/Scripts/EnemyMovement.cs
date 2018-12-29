using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    AIMoveDecision WaypointManager;
    public float MovementSpeed=2;
    float movementthisframe;
    bool moving;
    int index0;
    Vector2 MoveToPosition;
    // Use this for initialization
    void Start () {
        WaypointManager = FindObjectOfType<AIMoveDecision>();
        moving = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(moving==false)
        {
            index0 = UnityEngine.Random.Range(0,WaypointManager.Waypoints.Count-1);
            StartCoroutine(RandomMove());
             MoveToPosition = WaypointManager.Waypoints[index0].transform.position;
        }
        if(moving==true)
        {
            movementthisframe = MovementSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, MoveToPosition, movementthisframe);
        }
	}
    IEnumerator RandomMove()
    {
        moving = true;
        yield return new WaitForSeconds(2);
        moving = false;
    }
    private void OnCollisionStay(Collision collision)
    {
        index0 = UnityEngine.Random.Range(0, WaypointManager.Waypoints.Count - 1);
        MoveToPosition = WaypointManager.Waypoints[index0].transform.position;
        moving = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        index0 = UnityEngine.Random.Range(0, WaypointManager.Waypoints.Count - 1);
        MoveToPosition = WaypointManager.Waypoints[index0].transform.position;
        moving = true;
    }
}
