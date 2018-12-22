using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Vector3 deltapos;
    Vector3 displacement,newpos;
    public float MovementSpeed;
    float LeftBorder, RightBorder,TopBorder,BottomBorder;
    [SerializeField] GameObject Bullet;
    public float BulletSpeed;
    GameObject ObjBullet;
    Quaternion Rot;
    // Use this for initialization
    void Start () {
        float Zdis = Camera.main.transform.position.z - transform.position.z;
        LeftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0,Zdis)).x;
        RightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Zdis)).x;
        TopBorder= Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Zdis)).y;
        BottomBorder=Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Zdis)).y;
        Rot = Quaternion.LookRotation(new Vector3(0, 0, 0));
        Rot *= Quaternion.Euler(0, 0, 90);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetAxis("Horizontal")!=0 || Input.GetAxis("Vertical") != 0)
        {
            Move( Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        if(Input.GetButton("Fire1"))
        {
            Fire();
        }
}

    private void Move(float displacementx, float displacementy)
    {
        displacement = new Vector3(displacementx, displacementy,0);
        deltapos = displacement * Time.deltaTime*MovementSpeed;
        newpos = transform.position + deltapos;
        newpos.x=Mathf.Clamp(newpos.x, LeftBorder+.8f, RightBorder-.8f);
        newpos.y=Mathf.Clamp(newpos.y, BottomBorder+.8f, TopBorder-.8f);
        transform.position = newpos;
    }

    private void Fire()
    {
        
        ObjBullet = Instantiate<GameObject>(Bullet,transform.position,Rot);
        ObjBullet.GetComponent<Rigidbody2D>().velocity=new Vector2(0,BulletSpeed);
        Destroy(ObjBullet, 2);
    }
}
