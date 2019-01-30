using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    public AIMoveDecision WaypointManager;
    public float MovementSpeed=2;
    float movementthisframe;
    [SerializeField]float sec;
    bool running,changed;
    [SerializeField]int index0;
    public float Left, Right,mid;
    Vector2 LookForward;
    RaycastHit2D hit;
    bool Blocked;
    Vector2 MoveToPosition,Myposition,normalisedforce;
    WaypointProperties Waypoint,altWaypoint;
    Rigidbody2D Mybody;
    // Use this for initialization
    void Start () {
        WaypointManager = FindObjectOfType<AIMoveDecision>();
        running = false;
        index0 = UnityEngine.Random.Range(0, WaypointManager.Waypoints.Count - 1);
        Left = WaypointManager.Players[0].GetComponent<PlayerMovement>().LeftBorder;
        Right = WaypointManager.Players[0].GetComponent<PlayerMovement>().RightBorder;
        mid = Right-(Right-Left) / 2;
        Mybody = GetComponent<Rigidbody2D>();
        // Debug.Log("Index0:"+ index0);
        // Debug.Log("Count:" + WaypointManager.Waypoints.Count );
        altWaypoint = WaypointManager.Waypoints[index0];
        Debug.Log("ALT: "+altWaypoint.ToString());
        LookForward = new Vector2(altWaypoint.transform.position.x, altWaypoint.transform.position.y+0.2f);
        AIMoveDecision.BlockInitialised += Move;
        AIMoveDecision.UnBlockInitialised += Return;
        Blocked = false;
    }

    private void Return()
    {
        StopAllCoroutines();
        Blocked = false;
    }
    private void OnDestroy()
    {
        AIMoveDecision.BlockInitialised -= Move;
        AIMoveDecision.UnBlockInitialised -= Return;
    }
    private void Move()
    {
        StopAllCoroutines();
        Blocked = true;
        if (Right - gameObject.transform.position.x > mid)
        {
            StartCoroutine(SideSwipe(Left));
        }
        else
            StartCoroutine(SideSwipe(Right));
    }
    IEnumerator SideSwipe(float Side)
    {
        while(Mathf.Abs(gameObject.transform.position.x) < Side)
        {
            Mybody.AddForce(new Vector2(MovementSpeed*Side,-MovementSpeed));
            yield return null;
        }
       
    }
    // Update is called once per frame
    void Update () {
        //Debug.Log("Position :" + gameObject.transform.position.ToString());
        if(WaypointManager==null)
        {
            WaypointManager = FindObjectOfType<AIMoveDecision>();
        }
        if (WaypointManager.Waypoints.Count != 1)
        {
            if (running == true && Blocked!=true)
            {
                Myposition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
                if (Myposition != MoveToPosition)
                    Mybody.AddForce((MoveToPosition - Myposition).normalized * MovementSpeed);
                //transform.position = Vector2.MoveTowards(transform.position, MoveToPosition, movementthisframe);
            }
            else
            {
                StartCoroutine(RaycastAndMove());
            }
        }
	}
    IEnumerator RaycastAndMove()
    {
        Debug.Log("ALT WAYPOINT " + altWaypoint.ToString()+" Index " + index0.ToString()+" COUNT: "+WaypointManager.Waypoints.Count);
        if (altWaypoint)
        {
            running = true;
            hit = Physics2D.Raycast(altWaypoint.transform.position, LookForward);
            if (hit && hit.collider.gameObject.tag == "Pubic")
            {
                index0 = UnityEngine.Random.Range(0, WaypointManager.Waypoints.Count);
                altWaypoint = WaypointManager.Waypoints[index0];
                LookForward = new Vector2(altWaypoint.transform.position.x, altWaypoint.transform.position.y + 0.5f);
                running = false;
                yield return null;
            }
            else
            {
              //  Debug.Log("Index0: " + index0.ToString() + " ListCount: " + WaypointManager.Waypoints.Count);
                if (WaypointManager.Waypoints[index0] != null)
                {
                    Waypoint = WaypointManager.Waypoints[index0];
                    WaypointManager.Waypoints.RemoveAt(index0);
                    MoveToPosition = Waypoint.transform.position;
                    yield return new WaitForSeconds(sec);
                    WaypointManager.Waypoints.Add(Waypoint);
                    running = false;
                }
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (running == true)
        {
            StopAllCoroutines();                                                                                           //Will lead to reloading text problem which also works on coroutines
            WaypointManager.Waypoints.Add(Waypoint);
        }
        StartCoroutine(RaycastAndMove());

    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (running == true)
        {
            StopAllCoroutines();                                                                                           //Will lead to reloading text problem which also works on coroutines
            WaypointManager.Waypoints.Add(Waypoint);
        }
        StartCoroutine(RaycastAndMove());
    }
}
