using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour {
    [SerializeField] float Damage;
    DamageSystrm ObjectDamage;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObjectDamage = collision.gameObject.GetComponent<DamageSystrm>();
        if (ObjectDamage != null)
        {
            ObjectDamage.Damage(Damage);
            Destroy(gameObject);
        }
    }
}
