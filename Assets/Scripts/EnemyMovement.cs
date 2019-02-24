using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    public AIMoveDecision WaypointManager;
    public float MovementSpeed=2;
    float movementthisframe;
    [SerializeField]float sec;
    bool changed;
    public bool running;
    [SerializeField]int index0;
    public float Left, Right;
    Vector2 LookForward;
    RaycastHit2D hit;
    bool Blocked,Avoided;
    Vector2 MoveToPosition,Myposition,normalisedforce;
    WaypointProperties Waypoint,altWaypoint;
    public Rigidbody2D Mybody;
    public IEnumerator coroutine;
    float XDirection,YDirection;
    // Use this for initialization
    void Start () {
        WaypointManager = FindObjectOfType<AIMoveDecision>();
        running = false;
        index0 = UnityEngine.Random.Range(0, WaypointManager.Waypoints.Count - 1);
        Left = WaypointManager.Left.transform.position.x;
        Right = WaypointManager.Right.transform.position.x;
        Mybody = GetComponent<Rigidbody2D>();
        altWaypoint = WaypointManager.Waypoints[index0];
        LookForward = new Vector2(altWaypoint.transform.position.x, altWaypoint.transform.position.y+0.2f);
        if (!gameObject.CompareTag("Tank"))
        {
            AIMoveDecision.BlockInitialised += Move;
            AIMoveDecision.UnBlockInitialised += Return;
        }
        Blocked = false;
        Avoided = true;
        }

    private void Return()
    {
        if(Avoided==true)
            if(coroutine!=null)
                StopCoroutine(coroutine);
        Blocked = false;
     }
    private void OnDestroy()
    {
        if (!gameObject.CompareTag("Tank"))
        {
            AIMoveDecision.BlockInitialised -= Move;
            AIMoveDecision.UnBlockInitialised -= Return;
        }
    }
    private void Move()
    {
       
            StopCoroutine(RaycastAndMove());
            Blocked = true;
            if (Mathf.Abs(gameObject.transform.position.x) >= Mathf.Abs(Left) && Mathf.Abs(gameObject.transform.position.x) <= Mathf.Abs(Right))
            {
                if (Mathf.Abs(gameObject.transform.position.x) - Mathf.Abs(Left) >= Mathf.Abs(Right) - Mathf.Abs(gameObject.transform.position.x))
                {
                    Debug.Log("Right for " + gameObject.name);
                    coroutine = SideSwipe(Right);
                    StartCoroutine(SideSwipe(Right));
                }
                else
                {
                    Debug.Log("Left for " + gameObject.name);
                    coroutine = SideSwipe(Left);
                    StartCoroutine(SideSwipe(Left));
                }
            }
    }
    IEnumerator SideSwipe(float Side)
    {
        Avoided = false;
        Debug.Log(gameObject.name+" going "+Side.ToString());
        if (Side <= gameObject.transform.position.x)
        {
            while (Mathf.Abs(gameObject.transform.position.x) > Mathf.Abs(Side))
            {
                if (Time.timeScale == 1)
                    Mybody.AddForce(new Vector2(-MovementSpeed * Side * Time.deltaTime / Mathf.Abs(Side), 0));
                else
                    Mybody.AddForce(new Vector2(-MovementSpeed * Side * Time.deltaTime / (Mathf.Abs(Side)*10000) , 0));
                yield return null;
            }
            yield return new WaitForSeconds(3);
        }
        else
        {
            while (Mathf.Abs(gameObject.transform.position.x) < Mathf.Abs(Side))
            {
                if (Time.timeScale == 1)
                    Mybody.AddForce(new Vector2(MovementSpeed * Side * Time.deltaTime / Mathf.Abs(Side), 0));
                else
                    Mybody.AddForce(new Vector2(MovementSpeed * Side  * Time.deltaTime / (Mathf.Abs(Side)*10000) , 0));
                yield return null;
            }
            yield return new WaitForSeconds(3);
        }
        Avoided = true;
        yield return null;
    }
    // Update is called once per frame
    void Update () {
        //Debug.Log("Running " + running.ToString()+" for "+gameObject.name);
        if(WaypointManager==null)
        {
            WaypointManager = FindObjectOfType<AIMoveDecision>();
        }
        if (Blocked != true)
        {
                if (running == true)
                {
                    Myposition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
                    if (Myposition != MoveToPosition)
                {
                     if (Time.timeScale == 1)
                    Mybody.AddForce((MoveToPosition - Myposition).normalized * MovementSpeed * Time.deltaTime);
                    else
                        {
                            Mybody.AddForce((MoveToPosition - Myposition).normalized * Time.deltaTime*20 / MovementSpeed);
                        }
                    }
                }
                else
                {
                     StartCoroutine(RaycastAndMove());
                }
        }
	}
    IEnumerator RaycastAndMove()
    {
        running = true;
        hit = Physics2D.Raycast(altWaypoint.transform.position, LookForward);
        if (hit.transform.gameObject != null && hit.transform.gameObject.CompareTag("Waypoint")==false)
        {
            index0 = UnityEngine.Random.Range(0, WaypointManager.Waypoints.Count);
            altWaypoint = WaypointManager.Waypoints[index0];
            LookForward = new Vector2(altWaypoint.transform.position.x, altWaypoint.transform.position.y + 0.5f);
            hit = Physics2D.Raycast(altWaypoint.transform.position, LookForward);
            running = false;
            yield return null;
        }
        else
        {
            Waypoint = WaypointManager.Waypoints[index0];
            MoveToPosition = Waypoint.transform.position;
            yield return new WaitForSeconds(sec);
            running = false;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!gameObject.CompareTag("Tank"))
        {
            if (!Blocked)
            {
                if (running == true)
                {
                    StopCoroutine(RaycastAndMove());
                    running = false;
                    float x = -(collision.transform.position.x - gameObject.transform.position.x) * MovementSpeed / 3;//I think the division is always giving -1. Try subtracting the position of gameobject and collision and then using them in the x of the vector
                    Mybody.AddForce(new Vector2(x, 0));
                }

            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!gameObject.CompareTag("Tank"))
        {
            if (!Blocked)
            {
                if (running == false)
                {
                    StartCoroutine(RaycastAndMove());
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!gameObject.CompareTag("Tank"))
        {
            if (!Blocked)
            {
                /*   if (Mathf.Abs(collision.gameObject.transform.position.y) - Mathf.Abs(gameObject.transform.position.y) > 0)
                   {
                       YDirection = MovementSpeed;
                   }
                   else
                       YDirection = -MovementSpeed;
                   if (Mathf.Abs(collision.gameObject.transform.position.x) > Mathf.Abs(gameObject.transform.position.x))
                       XDirection = Left;
                   else
                       XDirection = Right;
                   Mybody.AddForce(new Vector2(XDirection * MovementSpeed * 10, YDirection * 100));*/
                if (Time.timeScale == 1)
                {
                    if (running == true)
                    {
                        StopCoroutine(RaycastAndMove());
                        float x = -(collision.transform.position.x - gameObject.transform.position.x) * MovementSpeed / 3;//I think the division is always giving -1. Try subtracting the position of gameobject and collision and then using them in the x of the vector
                        Mybody.AddForce(new Vector2(x, 0));
                    }
                    StartCoroutine(RaycastAndMove());
                }
            }
        }
    }
}
