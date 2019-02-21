using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]GameObject[] SpawnPoints;
    [SerializeField]GameObject[] PubicCars;
    int Carindex, LaneIndex;
    public float Seconds;
    bool Spawned,Blocked;
	// Use this for initialization
	void Start ()
    {
        AIMoveDecision.BlockInitialised += Stop;
        AIMoveDecision.UnBlockInitialised += SpawnTrue;
        Spawned = false;
        Blocked = false;
	}

    private void SpawnTrue()
    {
        Blocked = false;
        StartCoroutine(Spawn());
    }

    private void Stop()
    {
        StopCoroutine(Spawn());
        Blocked= true;
    }
    private void OnDestroy()
    {
        AIMoveDecision.BlockInitialised -= Stop;
        AIMoveDecision.UnBlockInitialised -= SpawnTrue;
    }
    // Update is called once per frame
    void Update ()
    {
         if(!Spawned && !Blocked)
        {
            if(Time.timeScale==1)
            StartCoroutine(Spawn());
        }
        
	}
    IEnumerator Spawn()
    {
        Carindex = UnityEngine.Random.Range(0, PubicCars.Length);
        LaneIndex = UnityEngine.Random.Range(0,SpawnPoints.Length );
        Instantiate<GameObject>(PubicCars[Carindex], SpawnPoints[LaneIndex].transform.position, Quaternion.identity);
        Spawned = true;
        yield return new WaitForSeconds(Seconds);
        Spawned = false;
    }
}
