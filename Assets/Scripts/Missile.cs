using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {
    GameObject Player;
    Vector3 NormalizedShootDirection;
    Quaternion LookRotation;
    [SerializeField] float MovementSpeed;
	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        NormalizedShootDirection = (Player.transform.position - gameObject.transform.position).normalized;
        LookRotation = Quaternion.FromToRotation(Vector3.right, NormalizedShootDirection);
        Quaternion startrotation = gameObject.transform.rotation;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,Player.transform.position, MovementSpeed * Time.deltaTime);
        gameObject.transform.rotation = startrotation * Quaternion.AngleAxis((LookRotation.eulerAngles.z - startrotation.eulerAngles.z) * Time.deltaTime, Vector3.forward);
    }
   
}
