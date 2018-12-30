using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEnemy : MonoBehaviour {
    [SerializeField] WaveConfig Wave1;
	// Use this for initialization
	void Start () {
        StartCoroutine(Wave1.SpawnEnemy(this.transform));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
