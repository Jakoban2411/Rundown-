using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBoom : MonoBehaviour {
    [SerializeField] GameObject Explosion;
    GameObject InstantiatedExplosion;
    bool isQuitting;
    [SerializeField] AudioClip ExplosionSFX;
	// Use this for initialization
	void Start () {
        isQuitting = false;
	}
    private void OnDestroy()
    {
        if (!isQuitting)
        {
            InstantiatedExplosion = Instantiate<GameObject>(Explosion, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(ExplosionSFX, Camera.main.transform.position);
            Destroy(InstantiatedExplosion, 2);
        }
    }
    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
