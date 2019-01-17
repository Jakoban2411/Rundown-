using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointProperties : MonoBehaviour {
    [SerializeField] AIMoveDecision WaypointManager;
	// Use this for initialization
	void Start () {
        WaypointManager.Waypoints.Add(this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
       
    }
    // Update is called once per frame
    void Update () {
		
	}
}
