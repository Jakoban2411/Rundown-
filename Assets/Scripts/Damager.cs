using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour {
    [SerializeField] float Damage;
    DamageSystrm ObjectDamage;
    bool check,checkstop;
    // Use this for initialization
    void Start()
    {
        if (Time.timeScale != 1)
        {
            check = true;
            checkstop = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(check==true && checkstop==false)
        {
            if(Time.timeScale==1)
            {
                checkstop = true;
                gameObject.GetComponent<Rigidbody2D>().velocity *= 500;
            }
        }
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
