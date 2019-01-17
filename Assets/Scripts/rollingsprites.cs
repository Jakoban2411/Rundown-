using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rollingsprites : MonoBehaviour {
    public float speed;
    Material myMaterial;
    Vector2 Offset;
	// Use this for initialization
	void Start () {
        myMaterial = GetComponent<Renderer>().material;
        Offset = new Vector2(0, speed);
    }
	
	// Update is called once per frame
	void Update () {
        myMaterial.mainTextureOffset += Offset * Time.deltaTime;
    }
}
