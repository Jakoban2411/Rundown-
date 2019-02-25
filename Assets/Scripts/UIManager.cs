using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour {
    [SerializeField] Button Resume;
    [SerializeField] Button StartB;
    [SerializeField] Button Quit;
    AIMoveDecision Manager;
    PlaneAIDecision PlaneManager;
    PlayerMovement PlayerScript;
    DamageSystrm PlayerDamage;
    [SerializeField] string endgame;
    GameObject Clampers;
    [SerializeField] GameObject MoveToObject;
    bool Started;
    GameObject Player;
    AudioSource BGMSource;
    Text TimeScore;
    float StartTime;
    // Use this for initialization
    void Start () {
        Manager = FindObjectOfType<AIMoveDecision>();
        PlaneManager = FindObjectOfType<PlaneAIDecision>();
        Resume.enabled = false;
        PlaneManager.enabled = false;
        Manager.enabled = false;
        PlayerScript = FindObjectOfType<PlayerMovement>();
        PlayerScript.enabled = false;
        Clampers = GameObject.FindGameObjectWithTag("Container");
        Player = PlayerScript.gameObject;
        PlayerDamage = Player.GetComponent<DamageSystrm>();
        BGMSource = GetComponent<AudioSource>();
        BGMSource.Stop();       
        Started = false;
        Clampers.SetActive(false);
        PlayerScript.Health.SetActive(false);
        TimeScore = gameObject.transform.Find("HighScore").transform.GetChild(0).GetComponent<Text>();
        DontDestroyOnLoad(TimeScore);
    }
	public void StartPress()
    {
        StartB.gameObject.SetActive(false);
        Quit.gameObject.SetActive(false);
        Resume.gameObject.SetActive(false);
        StartCoroutine(PlayerMove());
    }
    public void ResumePress()
    {
        PauseControl(false);
         Time.timeScale = 1;
    }
    // Update is called once per frame
    void Update () {
        if (Started == true)
        {
            TimeScore.text = (Time.timeSinceLevelLoad - StartTime).ToString("F2");
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                
                if (Time.timeScale == 1)
                {
                    Time.timeScale = 0;
                    PauseControl(true);
                }
                else
                {
                    Time.timeScale = 1;
                    PauseControl(false);
                }
            }
        }
       if(PlayerDamage.Health<=0)
        {
            Manager.enabled = false;
            Manager.HighScore = Time.timeSinceLevelLoad - StartTime;
            SceneManager.LoadScene(endgame);
        }
    }
    void PauseControl(bool ctrl)
    {
        PlayerScript.enabled = !ctrl;
        Manager.enabled = !ctrl;
        Player.GetComponent<Renderer>().enabled = !ctrl;
        Quit.gameObject.SetActive(ctrl);
        Resume.gameObject.SetActive(ctrl);
    }
    public void GameQuit()
    {
        Application.Quit();
    }
    IEnumerator PlayerMove()
    {
        while (Player.transform.position != MoveToObject.transform.position)
        {
            Player.transform.position = Vector2.MoveTowards(Player.transform.position, MoveToObject.transform.position, 4 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            if (Player.transform.position == MoveToObject.transform.position)
            {
                PlayerScript.enabled = true;
                BGMSource.Play();
                Resume.enabled = true;
                Started = true;
                StopCoroutine(PlayerMove());
                Clampers.SetActive(true);
                Manager.enabled = true;
                PlaneManager.enabled = true;
                PlayerScript.Health.SetActive(true);
                StartTime = Time.timeSinceLevelLoad;
            }
        }
    }
}
