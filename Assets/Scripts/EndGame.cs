using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EndGame : MonoBehaviour {
    [SerializeField]string scene;
    [SerializeField] AudioClip Crash;
    AIMoveDecision Manager;
    Text HighScore;
    // Use this for initialization
    void Start () {
        AudioSource.PlayClipAtPoint(Crash,Camera.main.transform.position);
        Manager = FindObjectOfType<AIMoveDecision>();
        HighScore = transform.Find("HighScoreHolder").transform.GetChild(0).GetComponent<Text>();
        HighScore.text = Manager.HighScore.ToString("F2");
        
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
        Manager.HighScore = 0;
        SceneManager.LoadScene(scene);
    }
}
