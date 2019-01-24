using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BoomBoom : MonoBehaviour {
    [SerializeField] GameObject Explosion;
    GameObject InstantiatedExplosion;
    bool isQuitting;
    [SerializeField] AudioClip ExplosionSFX;
	// Use this for initialization
	void Start () {
        isQuitting = false;
        SceneManager.activeSceneChanged += ChangeOnDestroy;
	}

    private void OnDestroy()
    {
        Debug.Log("Quit:" + isQuitting.ToString());
        if (!isQuitting)
        {
            Debug.Log("NO BOOM?!");
            InstantiatedExplosion = Instantiate<GameObject>(Explosion, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(ExplosionSFX, Camera.main.transform.position);
            SceneManager.activeSceneChanged -= ChangeOnDestroy;
            Destroy(InstantiatedExplosion, 2);
        }
    }
    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
    private void ChangeOnDestroy(Scene current,Scene next)
    {
        if(next.ToString()=="GameOver")
        {
            isQuitting = true;
        }
    }
    // Update is called once per frame
    void Update () {
        
	}
}
