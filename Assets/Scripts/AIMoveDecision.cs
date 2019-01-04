using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMoveDecision : MonoBehaviour {
    public List<WaypointProperties> Waypoints;
    [SerializeField] WaveConfig[] Waves;
    public float lanepos,seconds;
    [SerializeField] Spawner pubicspawner;
    // Use this for initialization
    void Start () {
        seconds = pubicspawner.Seconds;
    }
    
	// Update is called once per frame
	void Update () {
        Debug.Log("No. Waypoints: " + Waypoints.Count);
	}
    private IEnumerable StartWaves()
    {
        for (int WaveIndex = 0; WaveIndex < Waves.Length; WaveIndex++)
        {
            yield return StartCoroutine(Waves[WaveIndex].SpawnEnemy(this.transform));
        }
    }
}
