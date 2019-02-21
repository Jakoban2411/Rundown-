using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    [SerializeField] float Healthtoadd;
    public float DropProbability;
    float destroyinsec;
    GameObject ObjBullet;
    [SerializeField] bool isHealth,isTime,isExplode;
    [SerializeField] AudioClip Unstoppable;
    // Use this for initialization
    void Start()
    {
        DropProbability = Mathf.Clamp(DropProbability,0, 1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isHealth)
            {

                collision.gameObject.GetComponent<DamageSystrm>().Health += Healthtoadd;
                destroyinsec = 0;
            }
            else
            {
                if (isTime)
                {
                    destroyinsec = 30;//UnityEngine.Random.Range(5,10);
                    StartCoroutine(TimeChange(5));
                }
                else
                {
                    destroyinsec =8;
                    StartCoroutine(OmniDirection(collision.gameObject.GetComponent<PlayerMovement>(), destroyinsec));
                }
            }
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject,destroyinsec);
        }
    }
    IEnumerator OmniDirection(PlayerMovement playerscript,float duration)
    {
        Quaternion startrotation = playerscript.gameObject.transform.rotation;
        float t = 0;
        AudioSource.PlayClipAtPoint(Unstoppable, Camera.main.transform.position);
        while (t < duration)
        {
            t += Time.deltaTime;
            Quaternion Rotation = startrotation*Quaternion.AngleAxis(t/duration * 720, Vector3.forward);
            playerscript.Fire(Rotation);
            playerscript.Fire(Quaternion.Inverse(Rotation));
            yield return null;
        }

    }
    IEnumerator TimeChange(float TimesFaster)
    {
        Time.timeScale = TimesFaster;
        yield return new WaitForSeconds(28);
        Time.timeScale = 1;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
