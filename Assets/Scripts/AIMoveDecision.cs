using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMoveDecision : MonoBehaviour {
    public List<WaypointProperties> Waypoints;
    [SerializeField] WaveConfig[] Waves;
    [SerializeField] float SecBetWave;
    public GameObject[] Players;
    //WaveConfig[] InstantiatedWaves;
    public float lanepos,seconds;
    int WaveIndex;
    bool Spawned;
    [SerializeField] Spawner pubicspawner;
    // Use this for initialization
    private void Start()
    {
        if (FindObjectsOfType<AIMoveDecision>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
            DontDestroyOnLoad(gameObject);          //Check i it works. Its getting deleted and a new AIMove is getting instantiaed on pressing start button
    }
    void OnEnable () {
        WaveIndex = 0;
        Spawned = false;
        seconds = FindObjectOfType<Spawner>().Seconds; 
        Players = GameObject.FindGameObjectsWithTag("Player");
        StartCoroutine(StartWaves());
        
       /* for (int i = 0; i < Waves.Length; i++)
        {
            InstantiatedWaves[i] = ScriptableObject.CreateInstance<WaveConfig>(Waves[i]);
       }*/
    }
    private void OnDisable()
    {
        StopAllCoroutines();
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
