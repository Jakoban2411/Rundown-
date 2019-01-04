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
        /* if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag=="Player")
         {
             EnemyOnTrigger++;
             if (EnemyOnTrigger==1)
             {
                 WaypointManager.Waypoints.Remove(this);
               }
         }*/
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
       /* if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            EnemyOnTrigger--;
            if (EnemyOnTrigger==0)
            {
                WaypointManager.Waypoints.Add(this);
             }
        }*/
    }
    // Update is called once per frame
    void Update () {
		
	}
}
