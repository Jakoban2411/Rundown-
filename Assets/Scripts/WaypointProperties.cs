using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointProperties : MonoBehaviour {
    //bool Occupied;
    [SerializeField] AIMoveDecision WaypointManager;
    int EnemyOnTrigger;
	// Use this for initialization
	void Start () {
        WaypointManager.Waypoints.Add(this);
        EnemyOnTrigger = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag=="Player")
        {
            EnemyOnTrigger++;
            if (EnemyOnTrigger==1)
            {
                WaypointManager.Waypoints.Remove(this);
               // Debug.Log(" List Count On removal:" + WaypointManager.Waypoints.Count + ".Waypoint Removed: " + gameObject.name + " due to " + collision.gameObject.name);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            EnemyOnTrigger--;
            if (EnemyOnTrigger==0)
            {
                WaypointManager.Waypoints.Add(this);
              //  Debug.Log(" List Count:" + WaypointManager.Waypoints.Count + " due to " + collision.gameObject.name + "with: " + gameObject.name);
            }
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
