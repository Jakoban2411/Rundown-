using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    [SerializeField] float Healthtoadd;
    public float DropProbability;
    [SerializeField] bool isHealth,isTime,isExplode;
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

            }
            else
            {
                if (isTime)
                {
                    StartCoroutine(TimeChange(UnityEngine.Random.Range(1, 5)));
                }
                else
                {
                    StartCoroutine(OmniDirection(collision.gameObject.GetComponent<PlayerMovement>()));
                }
            }
            Destroy(gameObject);
        }
    }
    IEnumerator OmniDirection(PlayerMovement playerscript)
    {
           
        yield return null;
    }
    IEnumerator TimeChange(float TimesFaster)
    {
        Time.timeScale = TimesFaster;
        yield return new WaitForSeconds(3);
        Time.timeScale = 1;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
