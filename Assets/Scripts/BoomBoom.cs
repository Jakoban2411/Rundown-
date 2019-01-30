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
	// Use this for initialization
	void Start () {
        isQuitting = false;
        AudPos = Camera.main.transform.position;
        SceneManager.activeSceneChanged += ChangeOnDestroy;
        if (gameObject.transform.parent)
        {
            DontDestroyOnLoad(gameObject.transform.parent);
        }
        else
            DontDestroyOnLoad(gameObject);
    }
    private void OnDestroy()
    {
       // Debug.Log("Desroe");
          if (!isQuitting)
        {
            if (gameObject && InstantiatedExplosion==null)
            {
               // Debug.Log("NO BOOM?!");
                InstantiatedExplosion = Instantiate<GameObject>(Explosion, transform.position, Quaternion.identity);
                AudioSource.PlayClipAtPoint(ExplosionSFX,AudPos);
                Destroy(InstantiatedExplosion, 2);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("collided");
        if (collision.gameObject.tag == "Destroyer")
        {
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
        Debug.Log("uasfiu");
        isQuitting = true;
        Destroy(gameObject);
        SceneManager.activeSceneChanged -= ChangeOnDestroy;
    }
  
    // Update is called once per frame
    void Update () {
        
	}
}
