using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Wave Config Script")]
public class WaveConfig : ScriptableObject {
    [SerializeField] GameObject EnemyPrefab;
    public float TimeBetweenSpawns = 1;
    public float SpawnRandomFactor = 0.3f;
    public int NumberOfEnemies = 5;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public IEnumerator SpawnEnemy(Transform SpawnPoint)
    {
        if (Time.timeScale == 1)
        {
            for (int enemycount = 0; enemycount < NumberOfEnemies; enemycount++)
            {
                Instantiate<GameObject>(EnemyPrefab, SpawnPoint.transform.position, EnemyPrefab.transform.rotation);
                yield return new WaitForSeconds(TimeBetweenSpawns);
            }
        }
        else
            yield return new WaitForSeconds(TimeBetweenSpawns);
    }
}
