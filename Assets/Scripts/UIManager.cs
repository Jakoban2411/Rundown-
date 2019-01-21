using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField] Button Resume;
    [SerializeField] Button StartB;
    [SerializeField] Button Quit;
    [SerializeField] AIMoveDecision Manager;
    [SerializeField] PlayerMovement PlayerScript;
    [SerializeField] GameObject MoveToObject;
    bool Started;
    GameObject Player;
    AudioSource BGMSource;
    // Use this for initialization
    void Start () {
        Resume.enabled = false;
        Manager.enabled = false;
        PlayerScript.enabled = false;
        BGMSource = GetComponent<AudioSource>();
        Player=PlayerScript.gameObject;
        BGMSource.Stop();
        Started = false;
	}
	public void StartPress()
    {
        Manager.enabled = true;
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
       
    }
    void PauseControl(bool ctrl)
    {
        Manager.enabled = !ctrl;
        Player.GetComponent<Renderer>().enabled = !ctrl;
        Quit.gameObject.SetActive(ctrl);
        Resume.gameObject.SetActive(ctrl);
        
    }
    IEnumerator PlayerMove()
    {
        while (Player.transform.position != MoveToObject.transform.position)
        {
            Player.transform.position = Vector2.MoveTowards(Player.transform.position, MoveToObject.transform.position, PlayerScript.MovementSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            if (Player.transform.position == MoveToObject.transform.position)
            {
                PlayerScript.enabled = true;
                BGMSource.Play();
                Resume.enabled = true;
                Started = true;
                StopAllCoroutines();
            }
        }
        
    }
}
