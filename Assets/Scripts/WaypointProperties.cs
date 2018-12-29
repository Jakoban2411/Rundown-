using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointProperties : MonoBehaviour {
    //bool Occupied;
    [SerializeField] AIMoveDecision WaypointManager;
	// Use this for initialization
	void Start () {
        WaypointManager.Waypoints.Add(this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Enemy")
        {
            //Occupied = true;
           WaypointManager.Waypoints.Remove(this);
           }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
       WaypointManager.Waypoints.Add(this);
        Debug.Log(" List Count:" + WaypointManager.Waypoints.Count);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
