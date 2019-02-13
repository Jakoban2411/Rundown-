using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystrm : MonoBehaviour {
    public float Health,HealthDropProb;
    public GameObject Pickup;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Damage(float HitPoints)
    {
        Health -= HitPoints;
        if (Health<=0)
        {
            if(gameObject.tag!="Player")
            {
                if(Random.value>HealthDropProb)
                {
                    Instantiate<GameObject>(Pickup,transform.position,Pickup.transform.rotation);
                }

            }
            Destroy(gameObject);
        }
        else
        {

        }
    }
}
