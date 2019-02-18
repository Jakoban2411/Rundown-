using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystrm : MonoBehaviour {
    public float MaxHealth,HealthDropProb;
    [HideInInspector] public float Health;
    public GameObject Pickup;
    GameObject Healthsprite;
	// Use this for initialization
	void Start () {
        Health = MaxHealth;
        if (gameObject.tag == "Player")
        {
            Healthsprite = gameObject.GetComponent<PlayerMovement>().Health;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Damage(float HitPoints)
    {
        Health -= HitPoints;
        if (Health<=0)
        {
            if(!Healthsprite)
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
            if(Healthsprite)
            {
                Healthsprite.transform.localScale = Healthsprite.transform.localScale* (Health/MaxHealth);
            }
        }
    }
}
