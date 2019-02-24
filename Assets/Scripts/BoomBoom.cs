using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BoomBoom : MonoBehaviour {
    [SerializeField] GameObject Explosion;
    GameObject InstantiatedExplosion;
    bool isQuitting;
    Vector3 AudPos;
    [SerializeField] AudioClip ExplosionSFX;
    GameObject Owner;
	// Use this for initialization
	void Start () {
        isQuitting = false;
        AudPos = Camera.main.transform.position;
        SceneManager.activeSceneChanged += ChangeOnDestroy;
        if (gameObject.transform.parent)
        {
           Owner=gameObject.transform.parent.gameObject;
        }
        else
            Owner=gameObject;
        DontDestroyOnLoad(Owner);
    }
    private void OnDestroy()
    {
       // Debug.Log("Desroe");
          if (!isQuitting)
        {
            if (gameObject && InstantiatedExplosion==null)
            {
                InstantiatedExplosion = Instantiate<GameObject>(Explosion, transform.position, Quaternion.identity);
                AudioSource.PlayClipAtPoint(ExplosionSFX,AudPos);
                Destroy(InstantiatedExplosion, 2);
            }
        }
        SceneManager.activeSceneChanged -= ChangeOnDestroy;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Destroyer")
        {
            Debug.Log(collision.gameObject.name);
            isQuitting = true;
        }
    }
    private void OnApplicationQuit()
    {
        isQuitting = true;
        Destroy(gameObject);
    }
    private void ChangeOnDestroy(Scene current,Scene next)
    {
        isQuitting = true;
        Destroy(Owner);
    }
  
    // Update is called once per frame
    void Update () {
        
	}
}
