﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMoveDecision : MonoBehaviour {
    public List<WaypointProperties> Waypoints;
    [SerializeField] WaveConfig[] Waves;
    [SerializeField] float SecBetWave,TimeInterval;
    public float[] Stages;
    public GameObject[] Players;
    [SerializeField] GameObject[] RoadBlocks;
    //WaveConfig[] InstantiatedWaves;
    public float lanepos,seconds;
    int WaveIndex;
    float LastTime;
    bool Spawned;
    int sizeblock;
    GameObject Blocker;
    public bool blocked;
    [SerializeField] Spawner pubicspawner;
    public delegate void Block();
    public static event Block BlockInitialised;
    public delegate void UnBlock();
    public static event UnBlock UnBlockInitialised;
    // Use this for initialization
    private void Start()
    {
       if (FindObjectsOfType<AIMoveDecision>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
            DontDestroyOnLoad(gameObject);          //Check if it works. Its getting deleted and a new AIMove is getting instantiaed on pressing start button
    }
    void OnEnable () {
        sizeblock = RoadBlocks.Length;
        blocked = false;
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
        if (Time.timeSinceLevelLoad - LastTime > TimeInterval)
            StartCoroutine(RoadBlock());
	}
    IEnumerator RoadBlock()
    {
        BlockInitialised();
        blocked = true;
        int Rand = UnityEngine.Random.Range(0, sizeblock);
        StopAllCoroutines();
        Blocker = Instantiate<GameObject>(RoadBlocks[Rand], gameObject.transform.position, RoadBlocks[Rand].transform.rotation);
        LastTime = Time.timeSinceLevelLoad;
        foreach (int stage in Stages)
        {
            if (LastTime > stage)
                TimeInterval -= 10;
        }
        yield return new WaitForSeconds(3);
        Debug.Log("Something");
        UnBlockInitialised();
        blocked = false;
        yield return StartCoroutine(StartWaves());
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
