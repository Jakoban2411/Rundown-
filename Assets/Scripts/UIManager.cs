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
    GameObject Player;
    // Use this for initialization
    void Start () {
        Resume.enabled = false;
        Manager.enabled = false;
        PlayerScript.enabled = false;
        Player=PlayerScript.gameObject;
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
        Manager.enabled = true;
    }
    // Update is called once per frame
    void Update () {
		
	}


    IEnumerator PlayerMove()
    {
        while (Player.transform.position != MoveToObject.transform.position)
        {
            Player.transform.position = Vector2.MoveTowards(Player.transform.position, MoveToObject.transform.position, PlayerScript.MovementSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            if (Player.transform.position == MoveToObject.transform.position)
            {
                Debug.Log("Som0ething");
                PlayerScript.enabled = true;
            }
            else
            {
                Debug.Log("Player: "+Player.transform.position.ToString()+" MoveObject: " + MoveToObject.transform.position.ToString());
            }
        }
        
    }
}
