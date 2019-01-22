using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
	public GameObject pauseMenu;
	public Animator anim;

	private bool clicked;
	private bool isOpen;

    public GameObject map;
    public GameObject controls;
  
	private PlayerController playerController;
	// Use this for initialization
	void Start () {
        //	anim = GetComponent<Animator> ();
        Debug.Log(this.gameObject.GetComponent<GameObject>().name);
        map.SetActive(false);
        controls.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Start"))
        {//change to start button
         //clicked = true;
            if (!isOpen)
            {
                controls.SetActive(false);
                map.SetActive(true);
                pauseMenu.SetActive(true);

                anim.SetBool("isPaused", true);
                isOpen = true;
                Time.timeScale = 0f;
                Debug.Log("Menu Open");



            }

            else
            {
                pauseMenu.SetActive(false);
                anim.SetBool("isPaused", false);
                isOpen = false;
                //playerController.enabled = true;
                Time.timeScale = 1f;
                Debug.Log("Menu Close");
            }
            //			

        }
        else if (Input.GetButtonDown("Home"))
        {//change to start button
         //clicked = true;
            if (!isOpen)
            {
                map.SetActive(false);
                controls.SetActive(true);
                pauseMenu.SetActive(true);

                anim.SetBool("isPaused", true);
                isOpen = true;
                Time.timeScale = 0f;
                Debug.Log("Menu Open");

            }
            else
            {
                pauseMenu.SetActive(false);
                anim.SetBool("isPaused", false);
                isOpen = false;
                //playerController.enabled = true;
                Time.timeScale = 1f;
                Debug.Log("Menu Close");
            }
        }
    }
}
