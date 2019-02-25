using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pewdiepie : MonoBehaviour {
    public float MovementSpeed;
    Vector3 movethisframe;
    public bool Sideways;
    AudioSource source;
    GameObject BulletRight, BulletLeft;
    public bool isTracker, isDouble, isSingle;
    public float Health,BulletSpeed;
    [SerializeField] GameObject Left,Right,Center,Round;
    [SerializeField] float RateOfFire;
    public AudioClip Fire;
    bool shot,played;
	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        if(Sideways)
            StartCoroutine(FadeOut(source, source.clip.length / 2));
        else
            StartCoroutine(FadeOut(source, source.clip.length ));
        shot = false;
        played = false;
    }
    IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;
        yield return new WaitForSeconds(audioSource.clip.length / 5);
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
         audioSource.Stop();
        audioSource.volume = startVolume;
    }
    // Update is called once per frame
    void Update()
    {
        if (Sideways)
            movethisframe = new Vector3(gameObject.transform.position.x - MovementSpeed * Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z);
        else
            movethisframe = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - MovementSpeed * Time.deltaTime, gameObject.transform.position.z);
        gameObject.transform.position = movethisframe;
        if (source.volume == 0)
            Destroy(gameObject);
        if (shot == false)
        {
            StartCoroutine(ShootAtPlayer());
        }
    }
    IEnumerator ShootAtPlayer()
    {
        shot = true;
        if(isTracker)
         {  
            //yield return new WaitForSeconds(1.5f);
            AudioSource.PlayClipAtPoint(Fire, Camera.main.transform.position);
            BulletLeft = Instantiate<GameObject>(Round,Center.transform.position, Round.transform.rotation);
            Physics2D.IgnoreCollision(BulletLeft.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            yield return new WaitForSeconds(RateOfFire);
         }
        if(isDouble)
        {  
            if(played==false)
            {
                AudioSource.PlayClipAtPoint(Fire, Camera.main.transform.position);
                played=true;
            }
            BulletLeft = Instantiate<GameObject>(Round, Left.transform.position, Left.transform.rotation);
            Physics2D.IgnoreCollision(BulletLeft.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            BulletRight = Instantiate<GameObject>(Round, Right.transform.position, Right.transform.rotation);
            Physics2D.IgnoreCollision(BulletRight.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            BulletLeft.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -BulletSpeed);
            BulletRight.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -BulletSpeed);
            yield return new WaitForSeconds(RateOfFire);
            shot = false;
        }
        if(isSingle)
        {
            if(played==false)
            {
                AudioSource.PlayClipAtPoint(Fire, Camera.main.transform.position);
                played=true;
            }
            BulletLeft = Instantiate<GameObject>(Round,Center.transform.position, Center.transform.rotation);
            Physics2D.IgnoreCollision(BulletLeft.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            BulletLeft.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -BulletSpeed);
            yield return new WaitForSeconds(RateOfFire);
            shot = false;
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ammo"))
        {
            Health -= collision.gameObject.GetComponent<Damager>().Damage;
            if (Health <= 0)
                Destroy(gameObject);
        }
    }
}
