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
        AIMoveDecision.BlockInitialised += Stop;
        AIMoveDecision.UnBlockInitialised += SpawnTrue;
        Spawned = false;
	}

    private void SpawnTrue()
    {
        Spawned = false;
        Debug.Log("SpawnedStart: " + Spawned.ToString());
        StartCoroutine(Spawn());
    }

    private void Stop()
    {
        StopAllCoroutines();
        Spawned = true;
        Debug.Log("SpawnedStop: " + Spawned.ToString());
    }
    private void OnDestroy()
    {
        AIMoveDecision.BlockInitialised -= Stop;
        AIMoveDecision.UnBlockInitialised -= SpawnTrue;
    }
    // Update is called once per frame
    void Update ()
    {
         if(!Spawned)
        {
            Debug.Log("SpawnedUpdate: " + Spawned.ToString());
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
