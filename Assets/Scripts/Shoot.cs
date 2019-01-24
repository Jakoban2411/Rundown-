using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
    EnemyMovement ParentMovement;
    bool multiplayer = false;
    bool shot = false;
    public int ShootTime;
    [SerializeField] GameObject Bullet;
    [SerializeField] AudioClip Fire;
    [SerializeField] bool AttachedTurret;
    [SerializeField] GameObject Turret;
    [SerializeField] GameObject BulletSpawnLocation;
    [SerializeField] float BulletSpeed;
    Quaternion Rotation, TurretRotation;
    public float turretspeed;
    GameObject ObjBullet;
    Vector3 AudioPosition;
    Vector3 NormalizedShootDirection,NormalizedTurretPosition, ZPlayerPos,ZTurretPos;

    public GameObject[] Players;
    GameObject PlayerToShoot;
    // Use this for initialization
    void Start () {
        ParentMovement = GetComponent<EnemyMovement>();
        Players = ParentMovement.WaypointManager.Players;
        AudioPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 10);
        if (Players.Length > 1)
        {
            multiplayer = true;
        }
        else
            multiplayer = false;
    }
    
	// Update is called once per frame
	void Update () {
	    if(multiplayer==false)
        {
            PlayerToShoot = Players[0];
        }
        else
        {
            PlayerToShoot = ((Players[0].transform.position - gameObject.transform.position).magnitude > (Players[1].transform.position - gameObject.transform.position).magnitude) ? Players[1] : Players[0];
        }
        
        if(shot==false)
        {
            StartCoroutine(ShootAtPlayer());
        }
        if (AttachedTurret)
        {
            if (Turret)
            {/*
                ZPlayerPos = new Vector3(PlayerToShoot.transform.position.x, PlayerToShoot.transform.position.y, 0);
                ZTurretPos = new Vector3(Turret.transform.position.x, Turret.transform.position.y, 0);
                NormalizedTurretPosition=(ZPlayerPos-ZTurretPos).normalized;
                TurretRotation = Quaternion.FromToRotation(ZTurretPos,NormalizedTurretPosition);
                TurretRotation=Quaternion.FromToRotation(Turret.transform.position.normalized,NormalizedTurretPosition);*/
                //Turret.transform.rotation = Quaternion.RotateTowards(Turret.transform.rotation, Rotation, turretspeed * Time.deltaTime);
                Turret.transform.rotation = Quaternion.Slerp(Turret.transform.rotation, Rotation, turretspeed * Time.deltaTime);
            }
        }
    }
    IEnumerator ShootAtPlayer()
    {
        shot = true;
        AudioSource.PlayClipAtPoint(Fire,AudioPosition);
        NormalizedShootDirection = (PlayerToShoot.transform.position - gameObject.transform.position).normalized;
        Rotation = Quaternion.FromToRotation(Vector3.up, NormalizedShootDirection);
        ObjBullet = Instantiate<GameObject>(Bullet, BulletSpawnLocation.transform.position, Rotation);
        Physics2D.IgnoreCollision(ObjBullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        ObjBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(NormalizedShootDirection.x*BulletSpeed,NormalizedShootDirection.y*BulletSpeed);
        yield return new WaitForSeconds(ShootTime);
        shot = false;
    }
}
