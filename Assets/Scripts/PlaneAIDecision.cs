using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneAIDecision : MonoBehaviour {
    public GameObject LeftSpawnPosition, RightSpawnPosition, BannerSpawnPosition;
    [SerializeField] GameObject[] Planes;
    [SerializeField] GameObject Pewdiepie;
    [SerializeField] float[] SpawnIntervals;
    bool SpawnedPlanes;
    int index;
    int count;
    // Use this for initialization
	void Start () {
        StartCoroutine(PlaneSpawn());
        count = SpawnIntervals.Length;
	}
	
	// Update is called once per frame
	void Update () {
		if(SpawnedPlanes==false)
        {
            StartCoroutine(PlaneSpawn());
        }
	}
    IEnumerator PlaneSpawn()
    {
        SpawnedPlanes = true;
        index = UnityEngine.Random.Range(0, count);
        yield return new WaitForSeconds(SpawnIntervals[index] * 1000);
        Instantiate<GameObject>(Planes[index],LeftSpawnPosition.transform.position,Planes[index].transform.rotation);
        SpawnedPlanes = false;
        Instantiate<GameObject>(Planes[index], RightSpawnPosition.transform.position, Planes[index].transform.rotation);
        yield return new WaitForSeconds(SpawnIntervals[index] * 100);
        Instantiate<GameObject>(Pewdiepie, BannerSpawnPosition.transform.position, Pewdiepie.transform.rotation);
    }
}
