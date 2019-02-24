using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    [SerializeField] float Healthtoadd;
    public float DropProbability;
    float destroyinsec;
    GameObject ObjBullet;
    [SerializeField] bool isHealth,isTime,isExplode,isSpike,isAirdrop;
    [SerializeField] AudioClip Unstoppable,AirDrop,BombExplode;
    [SerializeField] GameObject LeftPlane, RightPlane,StartLeft,StartRight;
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
            if (isTime)
            {
                    destroyinsec = 30;//UnityEngine.Random.Range(5,10);
                    StartCoroutine(TimeChange(5));
            }
            if (isSpike)
            {
                destroyinsec = 8;
                StartCoroutine(Spikes(collision.gameObject.transform.GetChild(0).gameObject));
            }
            if(isExplode)
            {
                destroyinsec = 8;
                StartCoroutine(OmniDirection(collision.gameObject.GetComponent<PlayerMovement>(), destroyinsec));
            }
            if(isAirdrop)
            {

            }
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject,destroyinsec);
        }
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    IEnumerator Spikes(GameObject spike)
    {
        spike.SetActive(true);
        yield return new WaitForSeconds(7);
        spike.SetActive(false);
    }
    IEnumerator AirSupport()
    {
        GameObject Left = Instantiate<GameObject>(LeftPlane, StartLeft.transform.position, LeftPlane.transform.rotation);
        GameObject Right = Instantiate<GameObject>(RightPlane, StartRight.transform.position, RightPlane.transform.rotation);
        yield return new WaitForSeconds(0.5f);
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        AudioSource.PlayClipAtPoint(AirDrop, Camera.main.transform.position);
        yield return new WaitForSeconds(7.5f);
        foreach (GameObject enemy in Enemies)
        {
            AudioSource.PlayClipAtPoint(BombExplode, new Vector3(enemy.transform.position.x, enemy.transform.position.y, Camera.main.transform.position.z));
            Destroy(enemy);
            yield return new WaitForSeconds(.5f);
        }
        yield return null; 
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
