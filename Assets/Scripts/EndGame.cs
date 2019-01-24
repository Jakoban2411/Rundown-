using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndGame : MonoBehaviour {
    [SerializeField]string scene;
    [SerializeField] AudioClip Crash;
    AIMoveDecision Manager;
    // Use this for initialization
    void Start () {
        AudioSource.PlayClipAtPoint(Crash,Camera.main.transform.position);
     }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void GameQuit()
    {
        Application.Quit();
    }
    public void GameRestart()
    {
        SceneManager.LoadScene(scene);
    }
}
