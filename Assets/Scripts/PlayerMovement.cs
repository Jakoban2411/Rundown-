using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] GameObject ReloadText;
    Vector3 displacement,newpos;
    public float MovementSpeed;
    public float LeftBorder, RightBorder,TopBorder, BottomBorder,Mid,clipLength;
    [SerializeField] GameObject Bullet;
    public float BulletSpeed;
    GameObject ObjBullet;
    int BulletsFired;
    public int Magazine;
    bool Reloading,Firing,AudioPlaying;
    Animation FadeAnim;
    public AudioClip LMGFireaudio;
    public AudioClip LMGReloadaudio;
    public AudioClip LMGStopFireAudio;
    public AudioClip LMGStartAudio;
    AudioSource AudioComponent;
    Rigidbody2D PlayerBody;
    public GameObject Health;
    GameObject Spikes;
    public GameObject Left, Right;
    Vector3 velocity;
    public Vector3 PlaneLeft, PlaneRight;
    // Use this for initialization
    void Start () {
        float Zdis = Camera.main.transform.position.z - transform.position.z;
        LeftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0,Zdis)).x;
        RightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Zdis)).x;
        TopBorder= Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Zdis)).y;
        BottomBorder=Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Zdis)).y;
        Mid = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0, Zdis)).x;
        BulletsFired = 0;
        Reloading = false;
        PlaneLeft = Left.transform.position;
        PlaneRight = Right.transform.position;
        Destroy(Left);
        Destroy(Right);
        PlayerBody = GetComponent<Rigidbody2D>();
        FadeAnim=ReloadText.GetComponent<Animation>();
        FadeAnim.Stop();
        AudioComponent = GetComponent<AudioSource>();
        AudioComponent.clip = LMGFireaudio;
        Spikes = transform.GetChild(0).gameObject;
        Spikes.SetActive(false);
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetAxis("Horizontal")!=0 || Input.GetAxis("Vertical") != 0)
        {
            Move( Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        if(Input.GetButton("Fire1"))
        {
            Fire();
        }
        if(Input.GetButtonUp("Fire1"))
        {
            AudioComponent.Stop();
            StartCoroutine(PlayAudio(LMGStopFireAudio));
        }
}

    private void Move(float displacementx, float displacementy)
    {
        displacement = new Vector3(displacementx, displacementy,0);
        PlayerBody.AddForce(displacement*MovementSpeed*Time.unscaledDeltaTime);
    }

    private void Fire()
    {
        if (BulletsFired < Magazine)
        {
            if (Firing==true)
        {
            if (AudioPlaying == false)
            {
                StartCoroutine(PlayAudio(LMGFireaudio));
            }
        }
        if (Firing == false)
        {
            if (AudioPlaying == false)
            {
                StartCoroutine(PlayAudio(LMGStartAudio));
                Firing = true;
            }
        }
            ObjBullet = Instantiate<GameObject>(Bullet, transform.position, Bullet.transform.rotation);
            Physics2D.IgnoreCollision(ObjBullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            ObjBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, BulletSpeed);
            BulletsFired++;
            Destroy(ObjBullet,2);
           
        }
        else
        {
            if (Reloading == false)
            {
                StartCoroutine(DisplayReload());
                StartCoroutine(PlayAudio(LMGReloadaudio));
            }
        }
    }
    public void Fire(Quaternion Rotation)
    {
        ObjBullet = Instantiate<GameObject>(Bullet, transform.position, Rotation);
        if(Time.timeScale==1)
            velocity = Rotation * new Vector3(0, BulletSpeed/Time.timeScale, 0);
        else
            velocity = Rotation * new Vector3(0, BulletSpeed/ Time.timeScale, 0);
        Physics2D.IgnoreCollision(ObjBullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        ObjBullet.GetComponent<Rigidbody2D>().velocity = velocity;
        if(Time.timeScale==1)
        Destroy(ObjBullet, 2);
        else
            Destroy(ObjBullet, 2*Time.timeScale);

    }
    IEnumerator DisplayReload()
    {
        AudioComponent.clip = LMGReloadaudio;
        Reloading = true;
        FadeAnim.Play();
        AudioComponent.Play();
        yield return new WaitForSeconds(LMGReloadaudio.length);
        BulletsFired = 0;
        Reloading = false;
        FadeAnim.Stop();
    }
    IEnumerator PlayAudio(AudioClip clip)
    {
        AudioPlaying = true;
        AudioComponent.clip = clip;
        AudioComponent.Play();
        yield return new WaitForSeconds(clip.length);
        AudioPlaying = false;
    }
}
