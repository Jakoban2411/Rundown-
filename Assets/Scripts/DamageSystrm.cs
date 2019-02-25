using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystrm : MonoBehaviour
{
    public float CollisionDamage;
    public float MaxHealth,Health;
    public GameObject[] Pickup;
    float DropProb;
    GameObject Healthsprite;
    int index;
    Vector3 Maxscale;
    bool isPlayer;
    GameObject PickupSprite;
    public bool isTank;
    [SerializeField]GameObject DamageEffect;
    Animator Effect;
    // Use this for initialization
    void Start()
    {
        Health = MaxHealth;
        if (gameObject.tag == "Player")
        {
            isPlayer = true;
            Healthsprite = gameObject.GetComponent<PlayerMovement>().Health;
            Maxscale = Healthsprite.transform.localScale;
            Effect=DamageEffect.GetComponent<Animator>();
        }
        index = UnityEngine.Random.Range(0, Pickup.Length);
        if (isTank)
            DropProb = 1;
        else
            DropProb = Random.value;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Damage(float HitPoints)
    {
        Health -= HitPoints;

        if (Health <= 0)
        {

            if (isPlayer == false &&  DropProb>= Pickup[index].GetComponent<Pickup>().DropProbability)
            {
                Instantiate<GameObject>(Pickup[index], transform.position, Pickup[index].transform.rotation);
            }

            Destroy(gameObject);
        }
        else
        {
            if (isPlayer==true)
            {
                if(DamageEffect.activeSelf==false)
                {
                    DamageEffect.SetActive(true);
                }
                Healthsprite.transform.localScale = Maxscale * (Health / MaxHealth);
                Effect.Play("DamageEffect");
                
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<DamageSystrm>())
        {
            collision.gameObject.GetComponent<DamageSystrm>().Damage(CollisionDamage);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<DamageSystrm>())
        {
            collision.gameObject.GetComponent<DamageSystrm>().Damage(CollisionDamage);
        }
    }
}
