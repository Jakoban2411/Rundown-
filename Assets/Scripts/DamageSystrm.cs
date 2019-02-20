using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystrm : MonoBehaviour {
    public float MaxHealth,HealthDropProb;
    [HideInInspector] public float Health;
    public GameObject[] Pickup;
    GameObject Healthsprite;
    int index;
    GameObject PickupSprite;
	// Use this for initialization
	void Start () {
        Health = MaxHealth;
        if (gameObject.tag == "Player")
        {
            Healthsprite = gameObject.GetComponent<PlayerMovement>().Health;
        }
        index = UnityEngine.Random.Range(0, Pickup.Length);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Damage(float HitPoints)
    {
        Health -= HitPoints;
        if (Health<=0)
        {
                if(Random.value>=Pickup[index].GetComponent<Pickup>().DropProbability)
                {
                    Instantiate<GameObject>(Pickup[index],transform.position,Pickup[index].transform.rotation);
                }

            Destroy(gameObject);
        }
        else
        {
            if(Healthsprite)
                Healthsprite.transform.localScale = Healthsprite.transform.localScale* (Health/MaxHealth);
        }
    }
}
