using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]GameObject[] SpawnPoints;
    [SerializeField]GameObject[] PubicCars;
    int Carindex, LaneIndex;
    public float Seconds;
    bool Spawned;
	// Use this for initialization
	void Start ()
    {
        Spawned = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
	if(!Spawned)
        {
            StartCoroutine(Spawn());
        }
        
	}
    IEnumerator Spawn()
    {
     
        Carindex = UnityEngine.Random.Range(0, PubicCars.Length - 1);
        LaneIndex = UnityEngine.Random.Range(0,SpawnPoints.Length - 1);
       Instantiate<GameObject>(PubicCars[Carindex], SpawnPoints[LaneIndex].transform.position, Quaternion.identity);
        Spawned = true;
        yield return new WaitForSeconds(Seconds);
        Spawned = false;
      
    }
}
