using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pewdiepie : MonoBehaviour {
    public float MovementSpeed;
    Vector3 movethisframe;
    public bool Sideways;
    AudioSource source;
    public float Health;
	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        if(Sideways)
            StartCoroutine(FadeOut(source, source.clip.length / 2));
        else
            StartCoroutine(FadeOut(source, source.clip.length ));

    }
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
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
    void Update () {
        if(Sideways)
        movethisframe = new Vector3(gameObject.transform.position.x - MovementSpeed * Time.deltaTime, gameObject.transform.position.y , gameObject.transform.position.z);
        gameObject.transform.position = movethisframe;
        if (source.volume == 0)
            Destroy(gameObject);
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
