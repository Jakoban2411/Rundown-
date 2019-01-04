using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    AIMoveDecision WaypointManager;
    public float MovementSpeed=2;
    float movementthisframe,sec;
    bool running,changed;
    int index0;
    Vector2 LookForward;
    RaycastHit2D hit;
    Vector2 MoveToPosition;
    WaypointProperties Waypoint,altWaypoint;
    // Use this for initialization
    void Start () {
        WaypointManager = FindObjectOfType<AIMoveDecision>();
        running = false;
        sec = WaypointManager.seconds;
        index0 = UnityEngine.Random.Range(0, WaypointManager.Waypoints.Count - 1);
        altWaypoint = WaypointManager.Waypoints[index0];
        LookForward = new Vector2(altWaypoint.transform.position.x, altWaypoint.transform.position.y+0.5f);
    }


    // Update is called once per frame
    void Update () {
        /*  if (moving == false)
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
          {*/
        if (running == true)
        {
            movementthisframe = MovementSpeed * Time.deltaTime;
           transform.position = Vector2.MoveTowards(transform.position, MoveToPosition, movementthisframe);
        }
        else
        {
            StartCoroutine(RaycastAndMove());
        }
       // }
	}
    IEnumerator RaycastAndMove()
    {
        running = true;
        hit = Physics2D.Raycast(altWaypoint.transform.position, LookForward);
        if (hit.collider != null && hit.collider.gameObject.tag == "Pubic")
        {
                index0 = UnityEngine.Random.Range(0, WaypointManager.Waypoints.Count - 1);
                altWaypoint = WaypointManager.Waypoints[index0];
                LookForward = new Vector2(altWaypoint.transform.position.x, altWaypoint.transform.position.y + 0.5f);
                running = false;
                changed = true;
                yield return null;
        }
        else
        {
            Waypoint = WaypointManager.Waypoints[index0];
            WaypointManager.Waypoints.RemoveAt(index0);
            MoveToPosition = Waypoint.transform.position;
            yield return new WaitForSeconds(2);
            WaypointManager.Waypoints.Add(Waypoint);
            running = false;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        transform.position = transform.position;
        if (running == true)
        {
            StopAllCoroutines();                                                                                           //Will lead to reloading text problem which also works on coroutines
            WaypointManager.Waypoints.Add(Waypoint);
        }
        running = false;
        
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.position = transform.position;
        if (running == true)
        {
            StopAllCoroutines();                                                                                           //Will lead to reloading text problem which also works on coroutines
            WaypointManager.Waypoints.Add(Waypoint);
        }
        StartCoroutine(RaycastAndMove());
    }
}
