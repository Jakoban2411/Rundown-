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
    Vector3 TurretForward;
    Quaternion LookRotation,ShootRotation, TurretRotation;
    public float turretspeed;
    GameObject ObjBullet;
    Vector3 AudioPosition;
    Vector3 NormalizedShootDirection, ZPlayerPos,ZTurretPos;

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
            if (Turret && Turret.transform.rotation.eulerAngles.z!=LookRotation.eulerAngles.z)
            {
                Quaternion startrotation =Turret.transform.rotation;
                Turret.transform.rotation = startrotation*Quaternion.AngleAxis((LookRotation.eulerAngles.z-startrotation.eulerAngles.z)*Time.deltaTime, Vector3.forward);
            }
        }
    }
    IEnumerator ShootAtPlayer()
    {
        shot = true;
        AudioSource.PlayClipAtPoint(Fire,AudioPosition);
        if(PlayerToShoot)
            NormalizedShootDirection = (PlayerToShoot.transform.position - gameObject.transform.position).normalized;
        LookRotation = Quaternion.FromToRotation(Vector3.up, NormalizedShootDirection);
        ShootRotation = Quaternion.AngleAxis(LookRotation.eulerAngles.z, Vector3.forward);
        ObjBullet = Instantiate<GameObject>(Bullet, BulletSpawnLocation.transform.position, ShootRotation);
        Physics2D.IgnoreCollision(ObjBullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        if (AttachedTurret)
            ParentMovement.Mybody.AddForce(-NormalizedShootDirection * ParentMovement.MovementSpeed);
        if (Time.timeScale == 1)
            ObjBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(NormalizedShootDirection.x * BulletSpeed, NormalizedShootDirection.y * BulletSpeed);
        else
            ObjBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(NormalizedShootDirection.x * BulletSpeed, NormalizedShootDirection.y )*Time.deltaTime / BulletSpeed;
        yield return new WaitForSeconds(ShootTime);
        shot = false;
    }
}
