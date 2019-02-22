using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {
    [SerializeField] AudioClip DrawSpikes;
	// Use this for initialization
	void Start () {
		
	}
    private void OnEnable()
    {
        AudioSource.PlayClipAtPoint(DrawSpikes, Camera.main.transform.position);
        GetComponentInParent<DamageSystrm>().CollisionDamage = 500;
        
    }
    private void OnDisable()
    {
        AudioSource.PlayClipAtPoint(DrawSpikes, Camera.main.transform.position);
        GetComponentInParent<DamageSystrm>().CollisionDamage = 0;

    }
    // Update is called once per frame
    void Update () {
		
	}
}
