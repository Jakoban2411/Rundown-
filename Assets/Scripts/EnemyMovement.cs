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
    [SerializeField] float CollisionDamgage;
    public IEnumerator coroutine;
    float XDirection,YDirection;
    // Use this for initialization
    void Start () {
        WaypointManager = FindObjectOfType<AIMoveDecision>();
        running = false;
        index0 = UnityEngine.Random.Range(0, WaypointManager.Waypoints.Count - 1);
        Left = WaypointManager.Players[0].GetComponent<PlayerMovement>().LeftBorder;
        Right = WaypointManager.Players[0].GetComponent<PlayerMovement>().RightBorder;
        mid = WaypointManager.Players[0].GetComponent<PlayerMovement>().Mid;
        Mybody = GetComponent<Rigidbody2D>();
        // Debug.Log("Index0:"+ index0);
        // Debug.Log("Count:" + WaypointManager.Waypoints.Count );
        altWaypoint = WaypointManager.Waypoints[index0];
       LookForward = new Vector2(altWaypoint.transform.position.x, altWaypoint.transform.position.y+0.2f);
        AIMoveDecision.BlockInitialised += Move;
        AIMoveDecision.UnBlockInitialised += Return;
        Blocked = false;
    }

    private void Return()
    {
        if(coroutine!=null)
        StopCoroutine(coroutine);
        Blocked = false;
        Debug.Log("I DID RUN!Running: " + running.ToString());
    }
    private void OnDestroy()
    {
        AIMoveDecision.BlockInitialised -= Move;
        AIMoveDecision.UnBlockInitialised -= Return; 
    }
    private void Move()
    {
        StopCoroutine(RaycastAndMove());
        Blocked = true;
        if (gameObject.transform.position.x > mid)
        {
            coroutine = SideSwipe(Right);
            StartCoroutine(coroutine);
        }
        else
        {
            coroutine = SideSwipe(Left);
            Debug.Log("Coroutine with Left: " + Left.ToString());
            StartCoroutine(coroutine);
        }
    }
    IEnumerator SideSwipe(float Side)
    {
        Debug.Log("Side: " + Side.ToString() + " My: " + gameObject.transform.position.x);
        while(Mathf.Abs(gameObject.transform.position.x) < Mathf.Abs(Side))
        {
           // Debug.Log("Adding");
            Mybody.AddForce(new Vector2(MovementSpeed*Side*10f/Mathf.Abs(Side),-MovementSpeed));
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
        if (Blocked != true)
        {
            if (WaypointManager.Waypoints.Count != 1)
            {
                if (running == true)
                {
                    Myposition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
                    if (Myposition != MoveToPosition)
                    {
                      //  Debug.Log("Added: " + ((MoveToPosition - Myposition).normalized * MovementSpeed*5).ToString());
                        Mybody.AddForce((MoveToPosition - Myposition).normalized * MovementSpeed*5);
                        //transform.position = Vector2.MoveTowards(transform.position, MoveToPosition, movementthisframe);
                    }
                }
                else
                {
                    StartCoroutine(RaycastAndMove());
                }
            }
        }
	}
    IEnumerator RaycastAndMove()
    {
        //Debug.Log("ALT WAYPOINT " + altWaypoint.ToString()+" Index " + index0.ToString()+" COUNT: "+WaypointManager.Waypoints.Count);
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
                    MoveToPosition = Waypoint.transform.position;
                    yield return new WaitForSeconds(sec);
                    running = false;
                }
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!Blocked)
        {
            if (running == true)
            {
                StopCoroutine(RaycastAndMove());
               
            }
            StartCoroutine(RaycastAndMove());
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            collision.gameObject.GetComponent<DamageSystrm>().Damage(CollisionDamgage);
        }
        if (!Blocked)
        {
            if (Mathf.Abs(collision.gameObject.transform.position.y) - Mathf.Abs(gameObject.transform.position.y) > 0)
            {
                YDirection = MovementSpeed;
            }
            else
                YDirection = -MovementSpeed;
            if (Mathf.Abs(collision.gameObject.transform.position.x) > Mathf.Abs(gameObject.transform.position.x))
                XDirection = Left;
            else
                XDirection = Right;
            Mybody.AddForce(new Vector2(XDirection * MovementSpeed * 10, YDirection * 100));
            if (running == true)
            {
                StopCoroutine(RaycastAndMove());
            }
            StartCoroutine(RaycastAndMove());
        }
    }
}
