using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMoveDecision : MonoBehaviour {
    public List<WaypointProperties> Waypoints;
    [SerializeField] WaveConfig[] Waves;
    [SerializeField] float SecBetWave;
    public GameObject[] Players;
    public float lanepos,seconds;
    int WaveIndex;
    bool Spawned;
    [SerializeField] Spawner pubicspawner;
    // Use this for initialization
    void Start () {
        WaveIndex = 0;
        Spawned = false;
        seconds = pubicspawner.Seconds;
        Players = GameObject.FindGameObjectsWithTag("Player");
    }
    
	// Update is called once per frame
	void Update () {
        if(Spawned==false)
        {
            StartCoroutine(StartWaves());
        }
	}
    private IEnumerator StartWaves()
    {
        Spawned = true;
        yield return StartCoroutine(Waves[WaveIndex].SpawnEnemy(gameObject.transform));
        yield return new WaitForSeconds(SecBetWave);
        Spawned = false;
        WaveIndex++;
        if (WaveIndex==Waves.Length)
        {
            WaveIndex = 0;
        }
    }
}
