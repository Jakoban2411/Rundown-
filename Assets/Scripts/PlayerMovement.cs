using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] GameObject ReloadText;
    Vector3 deltapos,displacement,newpos;
    public float MovementSpeed;
    float LeftBorder, RightBorder,TopBorder, BottomBorder,clipLength;
    [SerializeField] GameObject Bullet;
    public float BulletSpeed;
    GameObject ObjBullet;
    Quaternion Rot;
    int BulletsFired;
    public int Magazine;
    bool Reloading,Firing,AudioPlaying;
    Animation FadeAnim;
    public AudioClip LMGFireaudio;
    public AudioClip LMGReloadaudio;
    public AudioClip LMGStopFireAudio;
    public AudioClip LMGStartAudio;
    AudioSource AudioComponent;
    // Use this for initialization
    void Start () {
        float Zdis = Camera.main.transform.position.z - transform.position.z;
        LeftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0,Zdis)).x;
        RightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Zdis)).x;
        TopBorder= Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Zdis)).y;
        BottomBorder=Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Zdis)).y;
        Rot = Quaternion.LookRotation(new Vector3(0, 0, 0));
        Rot *= Quaternion.Euler(0, 0, 90);
        BulletsFired = 0;
        Reloading = false;
        FadeAnim=ReloadText.GetComponent<Animation>();
        FadeAnim.Stop();
        AudioComponent = GetComponent<AudioSource>();
        AudioComponent.clip = LMGFireaudio;
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
        deltapos = displacement * Time.deltaTime*MovementSpeed;
        newpos = transform.position + deltapos;
        newpos.x=Mathf.Clamp(newpos.x, LeftBorder+.8f, RightBorder-.8f);
        newpos.y=Mathf.Clamp(newpos.y, BottomBorder+.8f, TopBorder-.8f);
        transform.position = newpos;

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
        
        
            ObjBullet = Instantiate<GameObject>(Bullet, transform.position, Rot);
            ObjBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, BulletSpeed);
            BulletsFired++;
           Destroy(ObjBullet,2);
           
        }
        else
        {
            if (Reloading == false)
            {
                StartCoroutine(PlayAudio(LMGReloadaudio));
                StartCoroutine(DisplayReload());
            }
        }
    }
    IEnumerator DisplayReload()
    {
        AudioComponent.clip = LMGReloadaudio;
        Reloading = true;
        FadeAnim.Play();
        AudioComponent.Play();
        yield return new WaitForSeconds(FadeAnim.GetClip("Flash").length);
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
