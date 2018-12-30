using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    AIMoveDecision WaypointManager;
    public float MovementSpeed=2;
    float movementthisframe;
    bool moving,start;
    int index0;
    Vector2 MoveToPosition;
    WaypointProperties Waypoint;
    // Use this for initialization
    void Start () {
        WaypointManager = FindObjectOfType<AIMoveDecision>();
        moving = false;
        start = true;
    }
    
	
	// Update is called once per frame
	void Update () {
        if (moving == false)
        {
            index0 = UnityEngine.Random.Range(0, WaypointManager.Waypoints.Count - 1);
            if (!start)
            {
                start = false;
                WaypointManager.Waypoints.Add(Waypoint);
            }
            Waypoint = WaypointManager.Waypoints[index0];
            WaypointManager.Waypoints.RemoveAt(index0);
            StartCoroutine(RandomMove());
            MoveToPosition = Waypoint.transform.position;
            start = false;
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
    private void OnCollisionStay2D(Collision2D collision)
    {
        transform.position = transform.position;
        StopAllCoroutines();                                                                                            //Will lead to reloading text problem which also works on coroutines
        moving = false;
        Debug.Log("Colliding because of Index:"+index0+ "LIST COUNT:"+WaypointManager.Waypoints.Count);
        /* index0 = UnityEngine.Random.Range(0, WaypointManager.Waypoints.Count - 1);
         MoveToPosition = WaypointManager.Waypoints[index0].transform.position;
         moving = true;*/
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.position = transform.position;
        //Debug.Log("Colliding because of Index:" + index0);
        /*  index0 = UnityEngine.Random.Range(0, WaypointManager.Waypoints.Count - 1);
          MoveToPosition = WaypointManager.Waypoints[index0].transform.position;*/
        StopAllCoroutines();                                                                                           //Will lead to reloading text problem which also works on coroutines
        StartCoroutine(RandomMove());
    }
}
